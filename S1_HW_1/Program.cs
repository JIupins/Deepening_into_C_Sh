// Написать программу-калькулятор, вычисляющую выражения вида a + b, a - b, a / b, a * b, введенные из командной строки, и выводящую результат выполнения на экран.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using static System.Net.Mime.MediaTypeNames;

namespace S1_HW_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            if (double.TryParse(args[0], out double first))
            {
                Console.WriteLine($"\nДано.\nПервое число: {args[0]}");
            }
            else
            {
                Console.WriteLine("Некорректное значение первого числа!");
                return;
            }

            if (double.TryParse(args[2], out double second))
            {
                Console.WriteLine($"Второе число: {args[2]}");
            }
            else
            {
                Console.WriteLine("Некорректное значение второго числа!");
                return;
            }

            if (char.TryParse(args[1], out char symbol) && (symbol == '+' || symbol == '-' || symbol == '*' || symbol == '/'))
            {
                Console.WriteLine($"Символ арифметической операции: {args[1]}");

                if (second == 0)
                {
                    Console.WriteLine("К сожалению деление на ноль невозможно!\n");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Некорректное значение символа арифметической операции!");
                return;
            }

            Console.WriteLine($"Решение: {first} {symbol} {second} = {Calculate(first, second, symbol)}\n");
        }

        static double Calculate(double a, double b, char c)
        {
            double result;

            switch (c)
            {
                case '+':
                    result = a + b;
                    break;
                case '-':
                    result = a - b;
                    break;
                case '*':
                    result = a * b;
                    break;
                case '/':
                    result = a / b;
                    break;
                default:
                    throw new ArgumentException("Неверный оператор");
            }

            return result;
        }
    }
}