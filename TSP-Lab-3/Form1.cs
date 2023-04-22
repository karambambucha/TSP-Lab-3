using System;
using System.Text;
using System.Windows.Forms;

namespace TSP_Lab_3
{
    public partial class Form1 : Form
    {
        private WeatherTypes preferredWeather;
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Узнать прогноз погоды на " + numericUpDown10.Value + " дней(-я)";
        }
        enum WeatherTypes
        {
            Rainy, Sunny, Snowy
        }
        double[,] Multiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < b.GetLength(1); j++)
                    for (int k = 0; k < b.GetLength(0); k++)
                        r[i, j] += a[i, k] * b[k, j];
            return r;
        }
        double[,] MatrixPower(double[,] a, int power)
        {
            double[,] res = a;
            for (int i = 0; i < power; i++)
            {
                res = Multiplication(res, a);
                richTextBox1.Text += $"Прогноз погоды через {i + 1} дней(-я): \n";
                richTextBox1.Text += Print(res);
            }
            return res;
        }
        string Print(double[,] a)
        {
            var tableStr = new StringBuilder();
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    tableStr.Append($"[{string.Format("{0:f10}", a[i, j])}]\t");
                }
                if (Array.IndexOf(Enum.GetValues(preferredWeather.GetType()), preferredWeather) == i)
                    tableStr.Append("(*)");
                tableStr.Append("\n");
            }

            tableStr.Append("\n");
            return tableStr.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (nud1.Value + nud2.Value + nud3.Value != 1
                 || nud4.Value + nud5.Value + nud6.Value != 1
                 || nud7.Value + nud8.Value + nud9.Value != 1)
                {
                    throw new Exception("Сумма одной или нескольких строк не равна 1!");
                }
                else
                {
                    if (radioButton1.Checked)
                        preferredWeather = WeatherTypes.Rainy;
                    else if (radioButton2.Checked)
                        preferredWeather = WeatherTypes.Sunny;
                    else if (radioButton3.Checked)
                        preferredWeather = WeatherTypes.Snowy;
                    double[,] probabilityTable = { { (double)nud1.Value, (double)nud2.Value, (double)nud3.Value },
                                                   { (double)nud4.Value, (double)nud5.Value, (double)nud6.Value },
                                                   { (double)nud7.Value, (double)nud8.Value, (double)nud9.Value } };
                    richTextBox1.Text = "";
                    MatrixPower(probabilityTable, (int)numericUpDown10.Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {
            button1.Text = "Узнать прогноз погоды на " + numericUpDown10.Value + " дней(-я)";
        }
    }
}
