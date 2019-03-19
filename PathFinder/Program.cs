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

            PrintResult(result);

            Console.WriteLine(map);

            Console.ReadLine();
        }

        private static void PrintResult(List<BoardSquare> result)
        {
            Console.WriteLine("Solution steps:");
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine("{" + result[i].X + ", " + result[i].Y + "}");
            }
        }
    }
}
