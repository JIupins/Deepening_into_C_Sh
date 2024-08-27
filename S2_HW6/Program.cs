namespace S2_HW6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int counter = 1;
            string action = "";

            ICalculator calculator = new Calculator();
            calculator.GotResult += Calculator_GotResult;

            while (true)
            {
                Console.Write($"Введите {counter}-е число: ");
                string input = Console.ReadLine();
                if (input.Equals("q", StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(input)) return;

                double number = ReadDouble(input);

                Console.Write("Введите арифметическое действие (+, -, *, /) или 'q' для выхода: ");
                action = Console.ReadLine();
                switch (action)
                {
                    case "+":
                        calculator.Sum(number);
                        break;
                    case "-":
                        calculator.Substract(number);
                        break;
                    case "*":
                        calculator.Multiply(number);
                        break;
                    case "/":
                        calculator.Divide(number);
                        break;
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Неверное действие!");
                        Console.WriteLine("Начните сначала.");
                        Console.WriteLine();
                        counter--;
                        break;
                }
                counter++;
            }
        }

        private static void Calculator_GotResult(object sender, OperandChangedEventArgs e)
        {
            // Определяем тип значения
            string type = e.Operand % 1 == 0 ? "целое" : "вещественное"; // Проверяем, является ли число целым
            Console.WriteLine($"Ответ: {e.Operand} ({type})");
            Console.WriteLine();
        }

        private static double ReadDouble(string input)
        {
            double result;
            while (!double.TryParse(input, out result))
            {
                Console.WriteLine("Это не число!");
                Console.WriteLine("Введите число.");
                input = Console.ReadLine();
            }
            return result;
        }
    }
}