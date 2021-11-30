using System.Collections.Generic;
using System.Linq;

namespace GraphСoloring
{
    public static class Painter
    {
        /// <summary>
        /// Жадный алгоритм правильной раскраски графа
        /// </summary>
        /// <param name="matrix">matrix: матрица смежности</param>
        /// <returns>массив цветов, каждый цвет - натуральное число</returns>
        public static int[] GreedyColoring(bool[,] matrix)
        {
            var count = matrix.GetLength(0); // 1
            var colorSet = new int[count]; // 1
            var usedColors = new List<int>(); // 1

            for (int numVertix = 0; numVertix < count; numVertix++) // 1 + (count + 1) + count 
            {
                var currentUsedColors = new List<int>(); // 1

                for (int i = 0; i < count; i++) // 1 + (count + 1) + count 
                {
                    // условие - 3 или 1
                    // внутри условия - 1 
                    if (matrix[numVertix, i] && colorSet[i] != 0) // смежные вершины numVertix и i && цвет i вершины уже определён.
                    {
                        currentUsedColors.Add(colorSet[i]); // 
                    }
                }

                // оценим линейно, count
                var affordableColors = usedColors.Except(currentUsedColors).ToList(); // получаем список доступных цветов

                // условие = 1
                // if: 2 + 1 + 1 = 4
                // else: 1
                if (affordableColors.Count == 0) // если цветов нет, устанавливаем новый цвет
                {
                    var num = usedColors.Count + 1;
                    colorSet[numVertix] = num;
                    usedColors.Add(num);
                }
                else // если есть доступные цвета, красим в любой (в первый)
                {
                    colorSet[numVertix] = affordableColors[0];
                }
            }
            return colorSet; // 1
        }

        /// <summary>
        /// Алгоритм правильной раскраски графа минимальным количеством цветов
        /// </summary>
        /// <param name="matrix">matrix: матрица смежности</param>
        /// <returns>массив цветов, каждый цвет - натуральное число</returns>
        public static int[] BruteForceColoring(bool[,] matrix)
        {
            BruteForce(matrix);

            return minColorSet;
        }

        private static int[] minColorSet;
        private static int minCountColors;

        private static bool Check(int[] colorSet, bool[,] matrix)
        {
            int count = matrix.GetLength(0);

            bool r = true;

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (matrix[i, j] && colorSet[i] == colorSet[j]) // смежные вершины i и j имеют одинаковый цвет
                    {
                        //return false;
                        r = false;
                    }
                }
            }

            return r;
        }

        private static void BruteForce(bool[,] matrix, int countVetrix = 0, int[] colorSet = null, int currentVertix = 0, int maxColor = 1)
        {
            if (colorSet is null)
            {
                countVetrix = matrix.GetLength(0);
                minCountColors = countVetrix + 1;
                colorSet = new int[countVetrix];
                minColorSet = new int[countVetrix];
            }

            if (currentVertix == countVetrix)
            {
                if (Check(colorSet, matrix))
                {
                    var countColors = colorSet.Distinct().ToArray().Length;
                    if (minCountColors > countColors)
                    {
                        minCountColors = countColors;
                        colorSet.CopyTo(minColorSet, 0);
                    }
                }
                return;
            }

            for (int i = 0; i < currentVertix + 1; i++)
            {
                bool isMaxColorIncreased = false;
                int num = i + 1;

                if (num > maxColor + 1)
                {
                    break;
                }
                if (num > maxColor)
                {
                    maxColor = num;
                    isMaxColorIncreased = true;
                }

                colorSet[currentVertix] = num;
                
                BruteForce(matrix, countVetrix, colorSet, currentVertix + 1, maxColor);
                
                if (isMaxColorIncreased)
                {
                    maxColor--;
                }
            }
        }
    }
}
