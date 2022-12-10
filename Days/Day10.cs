using System;

namespace Advent_Of_Code_2022.Days
{
    public class Day10 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day10");
            var X = 1;
            var signal = 0;
            int cycle = 0;
            foreach (var item in input)
            {
                if (item == "noop")
                    signal += CheckCycle(++cycle, X);

                else
                {
                    var arg = int.Parse(item.Split(" ")[1]);
                    signal += CheckCycle(++cycle, X);
                    signal += CheckCycle(++cycle, X);
                    X += arg;
                }
            }
            Console.Write(signal);
        }

        public void Star2()
        {
            var input = Input.Get("Day10");
            var X = 1;
            int cycle = 0;
            char[,] screen = new char[6, 40];
            foreach (var item in input)
            {
                if (item == "noop")
                {
                    screen[cycle / 40, cycle % 40] = DrawSprite(cycle++, X) ? '#' : '.';
                }
                else
                {
                    var arg = int.Parse(item.Split(" ")[1]);
                    screen[cycle / 40, cycle % 40] = DrawSprite(cycle++, X) ? '#' : '.';
                    screen[cycle / 40, cycle % 40] = DrawSprite(cycle++, X) ? '#' : '.';
                    X += arg;
                }
            }
            for (int i = 0; i < screen.GetLength(0); i++)
            {
                for (int j = 0; j < screen.GetLength(1); j++)
                    Console.Write(screen[i, j]);
                Console.Write("\n");
            }
        }

        private int CheckCycle(int cycle, int X)
        {
            if ((cycle - 20) % 40 == 0)
                return cycle * X;
            return 0;
        }

        private bool DrawSprite(int cycle, int X)
        {
            if (Math.Abs((cycle % 40) - X) <= 1)
                return true;
            return false;
        }
    }
}
