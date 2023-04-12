using System;
using System.Windows.Forms;

namespace OOP1_Task2
{
    public partial class Form1 : Form
    {
        //Параметры для методов PreparationForIter() и Iteration()
        long itr = 0;
        decimal numDecItr;
        decimal deltaItr;
        decimal guessItr;
        decimal resultItr;
        /// <summary>
        ///Расcчет корня через MathSqrt
        /// </summary>
        /// <param name="numberDouble">Принимает число типа double</param>
        /// <returns>Возвращает квадратный корень этого числа</returns>
        double SqrtRoot(double numberDouble)
        {
            return Math.Sqrt(numberDouble);
        }
        /// <summary>
        /// Нахождение корня через MathSqrt
        /// </summary>
        void MathSqrt()
        {
            //Расчет по Math Sqrt
            double numberDouble; //Тут будет хранится значение из textBox
            double squareRoot; //Тут хранится значение корня, вычисленное при помощи Math.Sqrt
            bool result1 = double.TryParse(textBoxValue.Text, out var number);//Проверям, ввели ли double
            if (result1 == true)
            {

                if (number < 0) //Положителен ли он
                {
                    MessageBox.Show("Введите положительное число"); return;
                }
                else
                {
                    numberDouble = number; //Если да, то находим корень через MathSqrt
                    squareRoot = SqrtRoot(numberDouble);
                    label1.Text = squareRoot.ToString() + " (.NET Framework)";
                    //Выводим это значение на label
                }
            }
            else
            {
                MessageBox.Show("Введите double");
                return;
            }
        }
        /// <summary>
        /// Нахождение корня методом Ньютона
        /// </summary>
        void NewtonRoot()
        {
            //Проверяем ввод
            bool result2 = decimal.TryParse(textBoxValue.Text, out var number2);
            if (result2 == true)
            {
                if (number2 == 0)
                {
                    label2.Text = 0 + " (Метод Ньютона)";
                    return;
                }
                if (number2 < 0) //Положителен ли он
                {
                    MessageBox.Show("Введите положительное число"); return;
                }
                decimal numberDecimal = number2;
                decimal delta = (decimal)Math.Pow(10, -28);
                //Самое маленькое значение поддерживаемое decimal. Нужно для проверки точности результата
                decimal guess = (decimal)Math.Sqrt((double)numberDecimal / 2);
                //В result хранятся значения, производимые на каждой итерации алгоритма
                decimal result = ((numberDecimal / guess) + guess) / 2;//Основанные на предыдущих значениях
                while (Math.Abs(result - guess) > delta)
                {
                    guess = result;
                    result = ((numberDecimal / guess) + guess) / 2;
                }
                label2.Text = result.ToString() + " (Метод Ньютона)";

            }
            else
            {
                MessageBox.Show("Введите decimal"); return;
            }
        }
        /// <summary>
        /// Метод, задающий значения и проверяющий введенное значение на 0 и -
        /// </summary>
        void PreparationForIter()
        {
            if (itr == 0)
            {
                bool resItr = decimal.TryParse(textBoxValue.Text, out var numItr);
                if (resItr == true) //Проверка на ввод не текста
                {
                    if (numItr == 0) //Проверка на 0
                    {
                        label2.Text = numItr + " (Метод Ньютона)";
                        amOfIter.Text = "Итераций: " + numItr;
                        fault.Text = "Погрешность: " + numItr;
                        RootOnIter.Text = "Корень во время итерации: " + numItr;
                        itr = 0;
                        return;
                    }
                    if (numItr < 0) //Положителен ли он
                    {
                        MessageBox.Show("Введите положительное число"); return;
                    }
                    numDecItr = numItr; //То же, что и в обычном методе Ньютона
                    deltaItr = (decimal)Math.Pow(10, -28);
                    guessItr = (decimal)Math.Sqrt((double)numDecItr / 2);
                    resultItr = ((numDecItr / guessItr) + guessItr) / 2;
                }
                else
                {
                    MessageBox.Show("Введите decimal"); return;
                }
            }
        }
        /// <summary>
        /// Одна итерация по нажатию на кнопку
        /// </summary>
        void Iteration()
        {
            //То же, что и в методе Ньютона, но без цикла
            if (Math.Abs(resultItr - guessItr) > deltaItr)
            {
                guessItr = resultItr;
                resultItr = ((numDecItr / guessItr) + guessItr) / 2;
            }
            else
            {
                label2.Text = resultItr.ToString() + " (Метод Ньютона)";
                itr = 0;
                resultItr = 0;
                guessItr = 0;
                return;
            }
            itr++;
            amOfIter.Text = "Итераций: " + itr;
            fault.Text = "Погрешность: " + Math.Abs(resultItr - guessItr);
            RootOnIter.Text = "Корень во время итерации: " + resultItr;
        }
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Нажатие на кнопку Вычислить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Calculate_Click(object sender, EventArgs e)
        {
            MathSqrt();
            NewtonRoot();
        }
        /// <summary>
        /// Нажатие на кнопку Выполнить итерацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void runIteration_Click(object sender, EventArgs e)
        {
            PreparationForIter();
            Iteration();
        }
        /// <summary>
        ///Изменение значения в textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            itr = 0;
            label1.Text = "(.NET Framework)";
            label2.Text = "(Метод Ньютона)";
        }
    }
}
