using System;
using System.IO;
using System.Linq;

namespace GraphСoloring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string input = @"input.txt";
            string output = @"output.txt";
            
            string help = "Ввод из файла - 1\nТестирование - 2\nВыход - 3\n";
            
            Console.WriteLine(help);
            int.TryParse(Console.ReadLine(), out int result);
            Console.Clear();
            switch (result)
            {
                case 1: File(input); break;
                case 2: Test(output); break;
                default: 
                    break;
            }
            Console.WriteLine("\nВыполнено!");
            Console.ReadKey();
        }

        private static void File(string input)
        {
            bool[,] matrix = ReadMatrix(input);
            Console.WriteLine("=========\nИсходные данные: \nn =  " + matrix.GetLength(0));
            PrintMatrix(matrix);

            var resultGreedy = Tester.StartAlgorithm(matrix, Painter.GreedyColoring);
            var resultAccuratee = Tester.StartAlgorithm(matrix, Painter.BruteForceColoring);
            
            PrintSet(resultGreedy.Set, "Жадный алгоритм", resultGreedy.Time);
            PrintSet(resultAccuratee.Set, "Точный алгоритм", resultAccuratee.Time);
        }

        private static void Test(string output)
        {
            string help = "Тип матрицы:\n\tматрица рандомого графа - 1\n\tматрица полного графа - 2\n\tматрица пустого графа - 3\n\tматрица цепного графа - 4\n";
            Console.WriteLine(help);

            Func<int, bool[,]> matrixType;

            int.TryParse(Console.ReadLine(), out int result);
            Console.Clear();
            Console.WriteLine(help);
            string res;
            switch (result)
            {
                case 1: matrixType = Tester.GenerateRandomMatrix; res = "рандомного"; break;
                case 2: matrixType = Tester.GenerateMatrixCompleteGraph; res = "полного"; break;
                case 3: matrixType = Tester.GenerateMatrixEmptyGraph; res = "пустого"; break;
                case 4: matrixType = Tester.GenerateChainedMatrix; res = "цепного"; break;
                default: matrixType = Tester.GenerateRandomMatrix; res = "рандомного"; break;
            }

            Console.WriteLine($"Выбрана матрица {res} графа");

            int[] tests = new int[15]
            {
                1000, 1000, 1000, 1000, 1000,
                1000, 1000, 1000, 1000, 100,
                1, 1, 1, 1, 1
            };
            
            using (var writer = new StreamWriter(output))
            {
                for (int i = 1; i < 13; i++)
                {
                    var timeStart = DateTime.Now;
                    var r = Tester.Test(i, tests[i - 1], matrixType); // r - result
                    var timeEnd = DateTime.Now;
                    writer.WriteLine(
                        $"Тест №{i} Количество тестов - {r.CountTests}:" +
                        $"\n\tКоличество вершин: {r.CountVertix} \n\tСреднее время работы: \n\t\tжадного - {r.TotalGreedyTime.TotalMilliseconds / r.CountTests}мс " +
                        $"\n\t\tточного - {r.TotalAccurateTime.TotalMilliseconds / r.CountTests}мс \n\tСовпавших решений: {r.MatchedSolutions * 100.0 / r.CountTests}%" +
                        $"\n\tСреднее относительное отклонение: {r.TotalRelativeDeviation / r.CountTests}" +
                        $"");
                    //writer.WriteLine($"{i} {r.TotalGreedyTime.TotalMilliseconds / r.CountTests}");
                    Console.WriteLine($"{i} {DateTime.Now} - {(timeEnd - timeStart).TotalMilliseconds} Тестов:{tests[i-1]}");
                }
            }
        }   

        private static void PrintSet(int[] set, string algorithmName, TimeSpan time)
        {
            Console.WriteLine($"\n=========\n{algorithmName}");
            Console.WriteLine($"Количество цветов: {set.Distinct().Count()}");
            Console.WriteLine($"Время работы: {time.Minutes}мин {time.Seconds}с {time.Milliseconds}мс");
            var count = set.Length;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(i + 1 + " - " + set[i]);
            }
        }

        private static bool[,] ReadMatrix(string path)
        {
            bool[,] matrix;
         
            using (StreamReader reader = new StreamReader(path))
            {
                int count = int.Parse(reader.ReadLine());
                matrix = new bool[count, count];
                for (int i = 0; i < count; i++)
                {
                    var line = reader.ReadLine();

                    var values = line.Split(' ').Select(x => int.Parse(x) != 0).ToArray();

                    for (int j = 0; j < count; j++)
                    {
                        matrix[i, j] = values[j];
                    }
                }
            }

            return matrix;
        }

        private static void PrintMatrix(bool[,] matrix)
        {
            int count = matrix.GetLength(0);
            
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    Console.Write(Convert.ToInt32(matrix[i, j]) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
