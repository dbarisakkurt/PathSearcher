using System;
using System.Text;

namespace PathFinder
{
    public class GameMap
    {
        private const int c_MapSize = 8;
        private string[,] m_Map;
        private BoardSquare m_Square = BoardSquare.Null;

        public GameMap()
        {
            // _ normal terrain
            // X elevated terrain
            // 0 start point
            // F finish point

            m_Map = new string[c_MapSize, c_MapSize]
            {
                {"_","_","_","_","X","_","F","_"},
                {"_","_","_","_","X","X","X","_"},
                {"_","_","_","_","X","_","_","_"},
                {"_","_","_","_","X","_","X","X"},
                {"_","_","X","X","X","_","X","_"},
                {"_","X","_","X","_","_","X","_"},
                {"_","X","X","X","_","X","_","_"},
                {"_","X","0","_","_","_","_","_"},
            };
        }

        public BoardSquare GetSpecificSquare(string text)
        {
            if (text != "0" && text != "F")
            {
                throw new InvalidOperationException("Only Start and Finish square info can be received");
            }
            if (!m_Square.Equals(BoardSquare.Null))
            {
                return m_Square;
            }

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (GetContent(j, i) == text)
                    {
                        return new BoardSquare(j, i);
                    }
                }
            }
            return m_Square;

        }

        public void Update(int x, int y, string value)
        {
            m_Map[x, y] = value;
        }

        public string GetContent(int x, int y)
        {
            return m_Map[x, y];
        }

        public int Size => c_MapSize;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    builder.Append(string.Format("{0,-5}", m_Map[i, j]));
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }
    }
}
