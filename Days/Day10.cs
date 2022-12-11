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
                    signal += (++cycle - 20) % 40 == 0 ? cycle * X : 0;

                else
                {
                    var arg = int.Parse(item.Split(" ")[1]);
                    signal += (++cycle - 20) % 40 == 0 ? cycle * X : 0;
                    signal += (++cycle - 20) % 40 == 0 ? cycle * X : 0;
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
                    screen[cycle / 40, cycle % 40] = Math.Abs((cycle++ % 40) - X) <= 1 ? '#' : '.';

                else
                {
                    var arg = int.Parse(item.Split(" ")[1]);
                    screen[cycle / 40, cycle % 40] = Math.Abs((cycle++ % 40) - X) <= 1 ? '#' : '.';
                    screen[cycle / 40, cycle % 40] = Math.Abs((cycle++ % 40) - X) <= 1 ? '#' : '.';
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
    }
}
