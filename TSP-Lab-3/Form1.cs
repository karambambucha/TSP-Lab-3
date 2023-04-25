using System;
using System.Text;
using System.Windows.Forms;

namespace TSP_Lab_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Text = "Узнать прогноз погоды на " + numericUpDown10.Value + " дней(-я)";
        }
        double[,] MatrixMultiplication(double[,] a, double[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            double[,] r = new double[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < b.GetLength(1); j++)
                    for (int k = 0; k < b.GetLength(0); k++)
                        r[i, j] += a[i, k] * b[k, j];
            return r;
        }
        public string Print(double[] vector)
        {
            var tableStr = new StringBuilder();
            
                for (int j = 0; j < vector.GetLength(0); j++)
                {
                    tableStr.Append($"[{string.Format("{0:f10}", vector[j])}]\t");
                }
                tableStr.Append("\n");

            tableStr.Append("\n");
            return tableStr.ToString();
        }
        double[,] VectorMatrixMultiplication(double[,] inputMartix, double[] vector)
        {
            double[,] resultMatrix;

            resultMatrix = new double[1, vector.Length];

            for (var i = 0; i < vector.Length; i++)
            {
                resultMatrix[0, i] = 0;
                for (var k = 0; k < vector.Length; k++)
                {
                    resultMatrix[0, i] += inputMartix[k, i] * vector[k];
                }
            }
            return resultMatrix;
        }
        void MatrixPower(double[,] a, double[] vector, int power)
        {
            double[,] res = VectorMatrixMultiplication(a,vector);
            richTextBox1.Text += $"Прогноз погоды завтра: \n";
            richTextBox1.Text += Print(res);
            for (int i = 1; i < power; i++)
            {
                double[,] resu = a;
                resu = MatrixMultiplication(resu, a);
                double[,] matr = VectorMatrixMultiplication(resu, vector);
                richTextBox1.Text += $"Прогноз погоды через {i+1} дней(-я): \n";
                richTextBox1.Text += Print(matr);
            }
        }
        string Print(double[,] a)
        {
            var tableStr = new StringBuilder();
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    tableStr.Append($"[{a[i, j]}]\t");
                }
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
                else if (nud1.Value < 0 || nud2.Value < 0 || nud3.Value < 0
                 || nud4.Value < 0 || nud5.Value < 0 || nud6.Value < 0
                 || nud7.Value < 0 || nud8.Value < 0 || nud9.Value < 0)
                {
                    throw new Exception("Одна или несколько вероятностей меньше 0!");
                }
                else if (nud11.Value + nud12.Value + nud13.Value != 1)
                {
                    throw new Exception("Сумма вектора изначнальных вероятностей не равна 1!");
                }
                else
                {
                    double[,] probabilityTable = { { (double)nud1.Value, (double)nud2.Value, (double)nud3.Value },
                                                   { (double)nud4.Value, (double)nud5.Value, (double)nud6.Value },
                                                   { (double)nud7.Value, (double)nud8.Value, (double)nud9.Value } };
                    richTextBox1.Text = "";
                    double[] vector = { (double)nud11.Value, (double)nud12.Value, (double)nud13.Value };
                    MatrixPower(probabilityTable, vector, (int)numericUpDown10.Value);
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
