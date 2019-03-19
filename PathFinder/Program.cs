using System;
using System.Collections.Generic;
using System.IO;

namespace PathFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine(@"Example usage: PathFinder.exe C:\Users\username\Desktop\map.txt");
                return;
            }

            string[,] mapAsArray = ReadMapFromFile(args[0]);
            
            if(mapAsArray == null)
            {
                return;
            }

            GameMap map = new GameMap(mapAsArray);

            PathSearcher finder = new PathSearcher(map);
            List<BoardSquare> result = finder.Find();

            PrintResult(result); //Prints the path as list
            Console.WriteLine(map); //Prints the solution map
        }

        /// <summary>
        /// Read file from disk. The map must be a square map
        /// </summary>
        private static string[,] ReadMapFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                int mapSize = lines.Length;

                string[,] map = new string[mapSize, mapSize];
                for(int i = 0; i< mapSize; i++)
                {
                    string[] squares = lines[i].Trim().Split(' ');
                    if(squares.Length != mapSize)
                    {
                        throw new InvalidOperationException("Map is not in a proper format. Please provide a proper map");
                    }

                    for(int j = 0; j<squares.Length; j++)
                    {
                        map[i, j] = squares[j];
                    }
                }

                return map;
            }
            catch(DirectoryNotFoundException)
            {
                Console.WriteLine("Exception occured while reading file");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Exception occured while reading file");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Exception occured while reading file");
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Exception occured while reading file");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Your map is not in proper format");
            }
            return null;
        }

        private static void PrintResult(List<BoardSquare> result)
        {
            Console.WriteLine("Solution steps:");
            Console.WriteLine("Map:");
            Console.WriteLine("TopLeft is (0,0)");
            Console.WriteLine("Bottom left is (7,0)");
            Console.WriteLine("TopRight is (0,7)");
            Console.WriteLine("BottomRight is (7,7)");

            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine("{" + result[i].X + ", " + result[i].Y + "}");
            }
        }
    }
}
