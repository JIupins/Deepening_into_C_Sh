// Отсортировать данные массива по возрастанию. И вывести их в консоль

namespace S1_HW_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Console.Write("Введите колличество чисел i в рассматриваемом массиве размерностью ixi: ");
            int number = Convert.ToInt32(Console.ReadLine());

            int[,] array = CreateArray(number);
            ShowArray(array, "\n Введен массив чисел:");

            array = SortArray(array);
            ShowArray(array, "\n Массив отсортированный в порядке возрастания:");
        }

        static int[,] CreateArray(int num)
        {
            int[,] ary = new int[num, num];

            for (int i = 0; i < ary.GetLength(0); i++)
            {
                for (int j = 0; j < ary.GetLength(1); j++)
                {
                    Console.Write($"Введите значение {i + 1},{j + 1}-го числа: ");
                    ary[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }
            return ary;
        }

        static void ShowArray(int[,] array, string note)
        {
            Console.WriteLine(note);

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static int[,] SortArray(int[,] array)
        {
            int[,] newArray = new int[array.GetLength(0), array.GetLength(1)];

            int[] data = new int[array.Length];
            int count = 0;

            foreach (var item in array)
            {
                data[count++] = item;
            }

            Array.Sort(data);

            count = 0;

            for (int i = 0; i < newArray.GetLength(0); i++)
            {
                for (int j = 0; j < newArray.GetLength(1); j++)
                {
                    newArray[i, j] = data[count++];
                }
            }

            return newArray;
        }
    }
}
