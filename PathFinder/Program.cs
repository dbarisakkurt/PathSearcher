using System;
using System.Collections.Generic;

namespace PathFinder
{
    class Program
    {
        //TODO find one path

        static void Main(string[] args)
        {
            GameMap map = new GameMap();
            PathSearcher finder = new PathSearcher(map);
            List<BoardSquare> result = finder.Find();
            PrintResult(result);

            Console.ReadLine();
        }

        static void PrintResult(List<BoardSquare> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine("{" + result[i].X + ", " + result[i].Y + "}");
            }
        }
    }
}
