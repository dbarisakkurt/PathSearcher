using System;
using System.Collections.Generic;

namespace PathFinder
{
    public class PathSearcher
    {
        private GameMap m_Map;
        private Dictionary<BoardSquare, bool> m_VisitInfo;
        private Dictionary<BoardSquare, int> m_Visited;

        public PathSearcher(GameMap map)
        {
            m_Map = map;
            m_VisitInfo = new Dictionary<BoardSquare, bool>();
            m_Visited = new Dictionary<BoardSquare, int>();

            InitVisitInfo();
        }

        /// <summary>
        /// Finds the path between start and finish using Breadth First Search
        /// </summary>
        public List<BoardSquare> Find()
        {
            BoardSquare start = m_Map.GetSpecificSquare("0");
            BoardSquare finish = m_Map.GetSpecificSquare("F");

            Queue<BoardSquare> toBeProcessed = new Queue<BoardSquare>();
            toBeProcessed.Enqueue(start);
            int order = 0;

            m_VisitInfo[start] = true;

            var current = start;

            m_Visited[current] = order;

            while (toBeProcessed.Count > 0)
            {
                var point = toBeProcessed.Dequeue();

                var neighbours = GetAllAvailableNeighbours(point);

                foreach (var item in neighbours)
                {
                    if (m_VisitInfo[item] == false)
                    {
                        toBeProcessed.Enqueue(item);
                        m_VisitInfo[point] = true;
                        m_Visited[item] = order;
                        m_Map.Update(item.X, item.Y, order.ToString());
                    }
                }

                order += 1;
            }

            List<BoardSquare> result = PrepareSolutionPath(start, finish);

            return result;
        }

        private List<BoardSquare> PrepareSolutionPath(BoardSquare start, BoardSquare finish)
        {
            List<BoardSquare> result = new List<BoardSquare>();

            BoardSquare temp = GetSmallestValueNeighbour(finish);
            result.Add(temp);

            while (!temp.Equals(start))
            {
                temp = GetSmallestValueNeighbour(temp);
                result.Add(temp);
            }

            result.Reverse();
            return result;
        }

        /// <summary>
        /// Picks the square whose numeric value is smallest
        /// </summary>
        private BoardSquare GetSmallestValueNeighbour(BoardSquare square)
        {   
            List<BoardSquare> list = GetAllNeighboursWhichisPartOfSolution(square);

            BoardSquare smallest = list[0];

            for(int i = 1; i<list.Count; i++)
            {
                if(m_Visited[list[i]] < m_Visited[smallest])
                {
                    smallest = list[i];
                }
                else if(m_Visited[list[i]] == m_Visited[m_Map.GetSpecificSquare("0")])
                {
                    smallest = list[i];
                }
            }

            return smallest;
        }

        /// <summary>
        /// Gets all neighbours which has a numeric value.
        /// </summary>
        /// <param name="center"></param>
        /// <returns>All neighbours for the given square which has a numeric value</returns>
        private List<BoardSquare> GetAllNeighboursWhichisPartOfSolution(BoardSquare center)
        {
            List<BoardSquare> result = new List<BoardSquare>();

            if ((center.X < m_Map.Size && center.X >= 0) &&
                (center.Y < m_Map.Size && center.Y >= 0))
            {
                int numeric;

                if (center.X - 1 >= 0 && int.TryParse(m_Map.GetContent(center.X - 1, center.Y), out numeric))
                {
                    result.Add(new BoardSquare(center.X - 1, center.Y));
                }
                if (center.X + 1 < m_Map.Size && int.TryParse(m_Map.GetContent(center.X + 1, center.Y), out numeric))
                {
                    result.Add(new BoardSquare(center.X + 1, center.Y));
                }
                if (center.Y - 1 >= 0 && int.TryParse(m_Map.GetContent(center.X, center.Y - 1), out numeric))
                {
                    result.Add(new BoardSquare(center.X, center.Y - 1));
                }
                if (center.Y + 1 < m_Map.Size && int.TryParse(m_Map.GetContent(center.X, center.Y + 1), out numeric))
                {
                    result.Add(new BoardSquare(center.X, center.Y + 1));
                }
            }

            if (result.Count > 4)
            {
                throw new InvalidOperationException("A point in the map has 4 neighbours at most");
            }

            return result;
        }


        /// <summary>
        /// Returns all available coordinates in the map for the given board square
        /// </summary>
        private List<BoardSquare> GetAllAvailableNeighbours(BoardSquare center)
        {
            List<BoardSquare> result = new List<BoardSquare>();

            if ((center.X < m_Map.Size && center.X >= 0) &&
                (center.Y < m_Map.Size && center.Y >= 0))
            {
                if (center.X - 1 >= 0 && m_Map.GetContent(center.X - 1, center.Y) == "_")
                {
                    result.Add(new BoardSquare(center.X - 1, center.Y));
                }
                if (center.X + 1 < m_Map.Size && m_Map.GetContent(center.X + 1, center.Y) == "_")
                {
                    result.Add(new BoardSquare(center.X + 1, center.Y));
                }
                if (center.Y - 1 >= 0 && m_Map.GetContent(center.X, center.Y - 1) == "_")
                {
                    result.Add(new BoardSquare(center.X, center.Y - 1));
                }
                if (center.Y + 1 < m_Map.Size && m_Map.GetContent(center.X, center.Y + 1) == "_")
                {
                    result.Add(new BoardSquare(center.X, center.Y + 1));
                }
            }

            if (result.Count > 4)
            {
                throw new InvalidOperationException("A point in the map has 4 neighbours at most");
            }

            return result;
        }

        /// <summary>
        /// Initialize all visited nodes to false
        /// </summary>
        private void InitVisitInfo()
        {
            for (int i = 0; i < m_Map.Size; i++)
            {
                for (int j = 0; j < m_Map.Size; j++)
                {
                    m_VisitInfo[new BoardSquare(i, j)] = false;
                }
            }
        }
    }
}
