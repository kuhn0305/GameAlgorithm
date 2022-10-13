using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Initialize();

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;
            const char CIRCLE = '\u25cf';

            int lastTick = 0;

            while(true)
            {
                #region 프레임 관리

                #endregion

                // 입력
                // 로직
                // 렌더링

            }

        }
    }
}