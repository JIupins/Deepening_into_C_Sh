namespace S2_HW4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int targetSum = 23;

            FindThreeNumbers(array, targetSum);
        }

        static void FindThreeNumbers(int[] array, int targetSum)
        {
            Array.Sort(array);
            int n = array.Length;

            for (int i = 0; i < n - 2; i++)
            {
                int left = i + 1;
                int right = n - 1;

                while (left < right)
                {
                    int currentSum = array[i] + array[left] + array[right];

                    if (currentSum == targetSum)
                    {
                        Console.WriteLine($"Найдены числа: {array[i]}, {array[left]}, {array[right]}");
                        return;
                    }
                    else if (currentSum < targetSum)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
                }
            }

            Console.WriteLine("Три числа, сумма которых равна искомому числу, не найдены.");
        }
    }
}