using System;
using System.Linq;

namespace GraphСoloring
{
    public static class Tester
    {
        public static TesterResult Test(int countVertix, int countTests, Func<int, bool[,]> matrixType)
        {
            var result = new TesterResult
            {
                CountTests = countTests,
                CountVertix = countVertix
            };
            
            for (int i = 0; i < countTests; i++)
            {
                var matrix = matrixType(countVertix);
                var greedyResult = StartAlgorithm(matrix, Painter.GreedyColoring);
                var accurateResult = StartAlgorithm(matrix, Painter.BruteForceColoring);

                var greedyCountColor = greedyResult.Set.Distinct().ToArray().Length;
                var accurateCountColor = accurateResult.Set.Distinct().ToArray().Length;

                if (greedyCountColor == accurateCountColor)
                {
                    result.MatchedSolutions++;
                }

                result.TotalAccurateTime += accurateResult.Time;
                result.TotalGreedyTime += greedyResult.Time;
                result.TotalRelativeDeviation += Math.Abs(greedyCountColor - accurateCountColor) * 1.0 / accurateCountColor;
            }
            
            return result;
        }

        public static bool[,] GenerateRandomMatrix(int count)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            
            bool[,] matrix = new bool[count, count];

            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    bool value = random.Next() % 2 == 0;
                    matrix[i, j] = value;
                    matrix[j, i] = value;
                }
            }
            return matrix;
        }

        public static bool[,] GenerateChainedMatrix(int count)
        {
            bool[,] matrix = new bool[count, count];

            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < i + 2 && j < count; j++)
                {
                    matrix[i, j] = true;
                    matrix[j, i] = true;
                }
            }
            return matrix;
        }

        public static bool[,] GenerateMatrixCompleteGraph(int count)
        {
            bool[,] matrix = new bool[count, count];

            for (int i = 0; i < count; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    matrix[i, j] = true;
                    matrix[j, i] = true;
                }
            }
            return matrix;
        }

        public static bool[,] GenerateMatrixEmptyGraph(int count)
        {
            bool[,] matrix = new bool[count, count];
            
            return matrix;
        }

        public static Result StartAlgorithm(bool[,] matrix, Func<bool[,], int[]> algorithm)
        {
            var timeStart = DateTime.Now;
            var colorSet = algorithm(matrix);
            var timeEnd = DateTime.Now;
            var time = timeEnd - timeStart;

            int countColors = colorSet.Distinct().ToArray().Length;

            return new Result(time, colorSet, countColors);
        }
    }
}
