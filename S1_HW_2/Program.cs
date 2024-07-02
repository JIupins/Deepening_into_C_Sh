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
            int n = array.GetLength(0);
            int m = array.GetLength(1);

            int[] array1D = new int[n * m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    array1D[i * m + j] = array[i, j];
                }
            }

            for (int i = 0; i < array1D.Length; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < array1D.Length; j++)
                {
                    if (array1D[j] < array1D[minIndex])
                    {
                        minIndex = j;
                    }
                }

                int temp = array1D[i];
                array1D[i] = array1D[minIndex];
                array1D[minIndex] = temp;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    array[i, j] = array1D[i * m + j];
                }
            }

            return array;
        }
    }
}
