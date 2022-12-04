using System;
using System.Linq;

namespace Advent_Of_Code_2022.Days
{
    public class Day3 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day3");
            int total = 0;
            foreach (var item in input)
            {
                var left = item.Take(item.Length / 2);
                var right = item.Remove(0, item.Length / 2);
                var shared = left.Where(l => right.Any(r => r == l)).FirstOrDefault();
                var add = char.IsLower(shared) ? 0 : 26;
                total += char.ToUpper(shared) + add - 64;
            }
            Console.WriteLine(total);
        }

        public void Star2()
        {
            var input = Input.Get("Day3");
            int total = 0;
            for (int i = 0; i < input.Length; i += 3)
            {
                var common = input[i].Where(c => input[i + 1].Any(r => r == c)).ToArray();
                var shared = common.Where(c => input[i + 2].Any(r => r == c)).FirstOrDefault();
                var add = char.IsLower(shared) ? 0 : 26;
                total += char.ToUpper(shared) + add - 64;
            }
            Console.WriteLine(total);
        }
    }
}
