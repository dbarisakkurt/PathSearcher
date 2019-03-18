using System;
using System.Collections.Generic;

namespace PathFinder
{
    class Program
    {
        //refactor to node and graph classes
        //find one path

        static void Main(string[] args)
        {
            //2 start, 3 finish
            const int mapSize = 8;

            int[,] map = new int[mapSize, mapSize]
            {
                {0,0,0,0,1,0,3,0 },
                {0,0,0,0,1,1,1,0 },
                {0,0,0,0,1,0,0,0 },
                {0,0,0,0,1,0,1,1 },
                {0,0,1,1,1,0,1,0 },
                {0,1,0,1,0,0,1,0 },
                {0,1,1,1,0,1,0,0 },
                {0,1,2,0,0,1,0,0 },
            };

            PathSearcher finder = new PathSearcher(map);
            List<Tuple<int, int>> result = finder.Find();
            PrintResult(result);

            Console.ReadLine();
        }

        static void PrintResult(List<Tuple<int, int>> result)
        {
            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine("{" + result[i].Item1 + ", " + result[i].Item2 + "}");
            }
        }
    }
}
