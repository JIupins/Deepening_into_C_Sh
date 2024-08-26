namespace S2_HW3
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int[,] labirynth1 = new int[,]
            {
            {1, 1, 1, 1, 1, 1, 1 },
            {1, 0, 0, 0, 0, 0, 1 },
            {1, 0, 1, 1, 1, 0, 1 },
            {0, 0, 0, 0, 1, 0, 0 },
            {1, 1, 1, 0, 1, 1, 1 },
            {1, 1, 1, 0, 1, 1, 1 },
            {1, 1, 1, 0, 1, 1, 1 }
            };

            int exits = HasExit(1, 1, labirynth1);
            Console.WriteLine($"Количество выходов в лабиринте: {exits}");
        }

        static int HasExit(int startI, int startJ, int[,] l)
        {
            int rows = l.GetLength(0);
            int cols = l.GetLength(1);
            bool[,] visited = new bool[rows, cols];
            int exits = 0;

            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((startI, startJ));
            visited[startI, startJ] = true;

            while (stack.Count > 0)
            {
                (int i, int j) = stack.Pop();

                // Проверка выхода из границ лабиринта
                if (i < 0 || i >= rows || j < 0 || j >= cols || l[i, j] == 1)
                    continue;

                // Если мы находимся на краю лабиринта и это не начальная точка
                if ((i == 0 || i == rows - 1 || j == 0 || j == cols - 1) && !(i == startI && j == startJ))
                {
                    exits++;
                    continue; // Переходим к следующей клетке
                }

                // Добавляем соседние клетки в стек
                for (int d = 0; d < 4; d++)
                {
                    int newI = i + new[] { -1, 1, 0, 0 }[d];
                    int newJ = j + new[] { 0, 0, -1, 1 }[d];
                    if (!visited[newI, newJ])
                    {
                        stack.Push((newI, newJ));
                        visited[newI, newJ] = true;
                    }
                }
            }

            return exits;
        }
    }
}