using System;
using System.IO;
using System.Linq;

namespace Lab07
{
    class Program
    {
        static void Main(string[] args)
        {
            //считать строку с файла
            string input = ReadStringFronFile();
            //выполнить действие
            string output = DoArithmeticOperation(input);
            //записать ответ в файл
            WriteAnswerToFile(output);
        }

        static string ReadStringFronFile()
        {
            string input;
            FileStream file = new FileStream(@"input.txt", FileMode.Open); //создаем файловый поток
            StreamReader reader = new StreamReader(file); // создаем «потоковый читатель» и связываем его с файловым потоком
            input = reader.ReadToEnd(); //считываем все данные с потока
            reader.Close(); //закрываем поток
            return input;
        }

        static string DoArithmeticOperation(string input)
        {
            
            input = RemoveDoubleSpace(input); //удаляем лишние пробелы
            string[] elements;
            double[] nums;
            DivideNumbersAndSigns(input, out nums, out elements);
            char[] signs = AreTheSignsCorrect(elements);
            double result = Calculate(nums, signs);
            string output = result.ToString();
            return output;
        }

        static void WriteAnswerToFile (string output)
        {
            FileStream file = new FileStream(@"output.txt", FileMode.Create); //создаем файловый поток
            StreamWriter writer = new StreamWriter(file); //создаем «потоковый писатель» и связываем его с файловым потоком
            writer.Write(output); //записываем в файл
            writer.Close(); //закрываем поток. Не закрыв поток, в файл ничего не запишется
        }

        static void RemoveAtString(ref string[] elements, int index)
        {
            string[] newelements = new string[elements.Length - 1];

            for (int i = 0; i < index; i++)
                newelements[i] = elements[i];
            for (int i = index + 1; i < elements.Length; i++)
                newelements[i - 1] = elements[i];
            elements = newelements;
        }

        static void RemoveAtChar(ref char[] elements, int index)
        {
            char[] newelements = new char[elements.Length - 1];

            for (int i = 0; i < index; i++)
                newelements[i] = elements[i];
            for (int i = index + 1; i < elements.Length; i++)
                newelements[i - 1] = elements[i];
            elements = newelements;
        }

        static double Calculate(double[] nums, char[] signs)
        {
            double result=0; //попытка составить алгоритм вычисления с учетом порядка действий провалилась, поэтому считаем без godmod`а
            if (signs[0] == '+')
                result = nums[0] + nums[1];
            else if (signs[0] == '-')
                result = nums[0] - nums[1];
            else if (signs[0] == '*')
                result = nums[0] * nums[1];
            else if (signs[0] == '/')
            {
                if (nums[1] != 0)
                    result = nums[0] / nums[1];
                else
                {
                    Console.WriteLine("Недопустимая операция");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            return result;
        }

        static char[] AreTheSignsCorrect(string[] elements)
        {
            char[] signs = { '+', '-', '*', '/' };
            char[] correctSigns = new char[elements.Length];
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].Length == 1)
                {
                    if(signs.Contains(elements[i][0]))
                        correctSigns[i] = elements[i][0];
                    else
                    {
                        Console.WriteLine("Недопустимая операция");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Недопустимая операция");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }

            return correctSigns;
        }

        static void DivideNumbersAndSigns(string input, out double[] nums, out string[] elements)
        {
            elements = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //делим строку на числа и знаки
            nums = new double[elements.Length / 2 + 1];
            int j = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                if (double.TryParse(elements[i], out double x))
                {
                    nums[j] = x;
                    j++;
                    RemoveAtString(ref elements, i);
                }
            }
        }

        static string RemoveDoubleSpace(string input)
        {

            while (input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }
            return input;
        }
    }
}
