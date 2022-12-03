using System;
using System.Linq;

namespace Advent_Of_Code_2022.Days
{
    public class Day1 : Day
    {
        public void Star1()
        {
            var calories = Input.GetSingle("Day1").Split("\r\n\r\n");
            var max = calories.Max(m => m.Split("\n").Sum(s => Convert.ToInt32(s)));
            Console.WriteLine(max);
        }

        public void Star2()
        {
            var calories = Input.GetSingle("Day1").Split("\r\n\r\n");
            var elfCal = calories.Select(m => m.Split("\n").Sum(s => Convert.ToInt32(s))).OrderByDescending(s => s).Take(3).Sum();
            Console.WriteLine(elfCal);
        }
    }
}
