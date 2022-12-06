using System;
using System.Linq;

namespace Advent_Of_Code_2022.Days
{
    public class Day6 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day6");
            for (int i = 0; i < input.Length; i++)
            {
                if (input.Substring(i, 4).Distinct().Count() == 4)
                {
                    Console.WriteLine(i + 4);
                    break;
                }
            }
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day6");
            for (int i = 0; i < input.Length; i++)
            {
                if (input.Substring(i, 14).Distinct().Count() == 14)
                {
                    Console.WriteLine(i + 14);
                    break;
                }
            }
        }
    }
}
