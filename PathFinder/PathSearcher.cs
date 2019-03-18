using System;
using System.Collections.Generic;

namespace PathFinder
{
    public class PathSearcher
    {
        private int[,] m_Map;
        private Dictionary<Tuple<int, int>, bool> m_VisitInfo;
        private int m_MapSize;

        public PathSearcher(int[,] map)
        {
            if (map.GetLength(0) != 8)
            {
                throw new InvalidOperationException("Map must hae 64 size");
            }
            if (map.GetLength(0) != map.GetLength(1))
            {
                throw new InvalidOperationException("Map must be a square for the given requirements");
            }

            m_Map = map;
            m_MapSize = m_Map.GetLength(0);
            m_VisitInfo = new Dictionary<Tuple<int, int>, bool>();

            //assign all nodes to visited = false
            for (int i = 0; i < m_Map.GetLength(0); i++)
            {
                for (int j = 0; j < m_Map.GetLength(1); j++)
                {
                    m_VisitInfo[new Tuple<int, int>(i, j)] = false;
                }
            }
        }

        /// <summary>
        /// Finds the path between start and finish using Breadth First Search
        /// </summary>
        public List<Tuple<int, int>> Find()
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();

            Tuple<int, int> start = GetStartAndFinishPoints()[0];
            Tuple<int, int> finish = GetStartAndFinishPoints()[1];

            Queue<Tuple<int, int>> toBeProcessed = new Queue<Tuple<int, int>>();
            toBeProcessed.Enqueue(start);

            m_VisitInfo[start] = true;

            var current = start;

            while (toBeProcessed.Count > 0)
            {
                var point = toBeProcessed.Dequeue();

                var neighbours = GetAllAvailableNeighbours(point);

                foreach (var item in neighbours)
                {
                    if (m_VisitInfo[item] == false)
                    {
                        toBeProcessed.Enqueue(item);
                        result.Add(item);
                        m_VisitInfo[point] = true;
                    }
                }
            }


            return result;
        }

        private List<Tuple<int, int>> GetAllAvailableNeighbours(Tuple<int, int> center)
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();

            if ((center.Item1 < m_Map.GetLength(0) && center.Item1 >= 0) &&
                (center.Item2 < m_Map.GetLength(1) && center.Item2 >= 0))
            {
                if (center.Item1 - 1 >= 0 && m_Map[center.Item1 - 1, center.Item2] == 0)
                {
                    result.Add(new Tuple<int, int>(center.Item1 - 1, center.Item2));
                }
                if (center.Item1 + 1 < m_Map.GetLength(0) && m_Map[center.Item1 + 1, center.Item2] == 0)
                {
                    result.Add(new Tuple<int, int>(center.Item1 + 1, center.Item2));
                }
                if (center.Item2 - 1 >= 0 && m_Map[center.Item1, center.Item2 - 1] == 0)
                {
                    result.Add(new Tuple<int, int>(center.Item1, center.Item2 - 1));
                }
                if (center.Item2 + 1 < m_Map.GetLength(1) && m_Map[center.Item1, center.Item2 + 1] == 0)
                {
                    result.Add(new Tuple<int, int>(center.Item1, center.Item2 + 1));
                }
            }

            if (result.Count > 4)
            {
                throw new InvalidOperationException("A point in the map has 4 neighbours at most");
            }

            return result;
        }

        private List<Tuple<int, int>> GetStartAndFinishPoints()
        {
            List<Tuple<int, int>> result = new List<Tuple<int, int>>();
            result.Add(null);
            result.Add(null);

            for (int i = 0; i < m_Map.GetLength(0); i++)
            {
                for (int j = 0; j < m_Map.GetLength(1); j++)
                {
                    if (m_Map[i, j] == 2)
                    {
                        result[0] = new Tuple<int, int>(i, j);
                    }
                    if (m_Map[i, j] == 3)
                    {
                        result[1] = new Tuple<int, int>(i, j);
                    }
                }
            }

            return result;
        }
    }
}
