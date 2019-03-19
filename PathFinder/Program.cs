using System;
using System.Collections.Generic;

namespace PathFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            GameMap map = new GameMap();
            PathSearcher finder = new PathSearcher(map);
            List<BoardSquare> result = finder.Find();

            PrintResult(result); //Prints the path as list
            Console.WriteLine(map); //Prints the solution map

            Console.ReadLine();
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
