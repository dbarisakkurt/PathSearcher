
namespace PathFinder
{
    public class GameMap
    {
        private const int c_MapSize = 8;
        private int[,] m_Map;

        public GameMap()
        {
            // 0 normal terrain
            // 1 elevated terrain
            // 2 start point
            // 3 finish point

            //I intentionally did not use enum, because plain numbers looked more readable here
            m_Map = new int[c_MapSize, c_MapSize]
            {
                {0,0,0,0,1,0,3,0},
                {0,0,0,0,1,1,1,0},
                {0,0,0,0,1,0,0,0},
                {0,0,0,0,1,0,1,1},
                {0,0,1,1,1,0,1,0},
                {0,1,0,1,0,0,1,0},
                {0,1,1,1,0,1,0,0},
                {0,1,2,0,0,1,0,0},
            };
        }

        public int GetContent(int x, int y)
        {
            return m_Map[x, y];
        }

        public int Size => c_MapSize;
    }
}
