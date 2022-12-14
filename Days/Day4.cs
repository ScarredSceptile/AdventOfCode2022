using System;
using System.Linq;

namespace Advent_Of_Code_2022.Days
{
    public class Day4 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day4");
            Console.WriteLine(input.Where(i => ContainsOther(i.Split(","))).Count());
        }

        public void Star2()
        {
            var input = Input.Get("Day4");
            Console.WriteLine(input.Where(i => Overlaps(i.Split(","))).Count());
        }

        private bool ContainsOther(string[] ranges)
        {
            var rangeOne = ranges[0].Split("-").Select(n => int.Parse(n)).ToArray();
            var rangeTwo = ranges[1].Split("-").Select(n => int.Parse(n)).ToArray();
            return (rangeOne[0] >= rangeTwo[0]) && (rangeOne[1] <= rangeTwo[1]) || (rangeOne[0] <= rangeTwo[0]) && (rangeOne[1] >= rangeTwo[1]);
        }

        private bool Overlaps(string[] ranges)
        {
            var rangeOne = ranges[0].Split("-").Select(n => int.Parse(n)).ToArray();
            var rangeTwo = ranges[1].Split("-").Select(n => int.Parse(n)).ToArray();
            var r1 = Enumerable.Range(rangeOne[0], rangeOne[1] - rangeOne[0] + 1).ToList();
            var r2 = Enumerable.Range(rangeTwo[0], rangeTwo[1] - rangeTwo[0] + 1).ToList();
            return r1.Where(c => r2.Contains(c)).Any();
        }
    }
}