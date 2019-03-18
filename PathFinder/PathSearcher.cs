using System;
using System.Collections.Generic;

namespace PathFinder
{
    public class PathSearcher
    {
        private GameMap m_Map;
        private Dictionary<BoardSquare, bool> m_VisitInfo;

        public PathSearcher(GameMap map)
        {
            m_Map = map;
            m_VisitInfo = new Dictionary<BoardSquare, bool>();

            InitVisitInfo();
        }

        /// <summary>
        /// Finds the path between start and finish using Breadth First Search
        /// </summary>
        public List<BoardSquare> Find()
        {
            List<BoardSquare> result = new List<BoardSquare>();

            BoardSquare start = GetStartAndFinishPoints()[0];
            BoardSquare finish = GetStartAndFinishPoints()[1];

            Queue<BoardSquare> toBeProcessed = new Queue<BoardSquare>();
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

        /// <summary>
        /// Returns all available coordinates in the map for the given board square
        /// </summary>
        private List<BoardSquare> GetAllAvailableNeighbours(BoardSquare center)
        {
            List<BoardSquare> result = new List<BoardSquare>();

            if ((center.X < m_Map.Size && center.X >= 0) &&
                (center.Y < m_Map.Size && center.Y >= 0))
            {
                if (center.X - 1 >= 0 && m_Map.GetContent(center.X - 1, center.Y) == 0)
                {
                    result.Add(new BoardSquare(center.X - 1, center.Y));
                }
                if (center.X + 1 < m_Map.Size && m_Map.GetContent(center.X + 1, center.Y) == 0)
                {
                    result.Add(new BoardSquare(center.X + 1, center.Y));
                }
                if (center.Y - 1 >= 0 && m_Map.GetContent(center.X, center.Y - 1) == 0)
                {
                    result.Add(new BoardSquare(center.X, center.Y - 1));
                }
                if (center.Y + 1 < m_Map.Size && m_Map.GetContent(center.X, center.Y + 1) == 0)
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

        private List<BoardSquare> GetStartAndFinishPoints()
        {
            List<BoardSquare> result = new List<BoardSquare>();
            result.Add(BoardSquare.Null);
            result.Add(BoardSquare.Null);

            for (int i = 0; i < m_Map.Size; i++)
            {
                for (int j = 0; j < m_Map.Size; j++)
                {
                    if (m_Map.GetContent(i, j) == 2)
                    {
                        result[0] = new BoardSquare(i, j);
                    }
                    if (m_Map.GetContent(i, j) == 3)
                    {
                        result[1] = new BoardSquare(i, j);
                    }
                }
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
