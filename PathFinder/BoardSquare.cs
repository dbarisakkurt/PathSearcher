namespace PathFinder
{
    public struct BoardSquare
    {
        public static readonly BoardSquare Null = new BoardSquare(-1, -1);

        private int m_X;
        private int m_Y;

        public int X { get { return m_X; } set { m_X = value; } }
        public int Y { get { return m_Y; } set { m_Y = value; } }

        public BoardSquare(int x, int y)
        {
            m_X = x;
            m_Y = y;
        }

        public override string ToString()
        {
            return "(" + m_X+ ", " + m_Y + ")";
        }
    }
}
