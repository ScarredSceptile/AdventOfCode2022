using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2022.Days
{
    public class Day2 : Day
    {
        private Dictionary<string, int> shapePoint = new Dictionary<string, int>
        {
            {"A", 1 },
            {"B", 2 },
            {"C", 3 }
        };

        private Dictionary<string, string> winCon = new Dictionary<string, string>
        {
            {"A", "C" },
            {"B", "A" },
            {"C", "B" }
        };

        private Dictionary<string, string> lossCon = new Dictionary<string, string>
        {
            {"A", "B" },
            {"B", "C" },
            {"C", "A" }
        };

        public void Star1()
        {
            var input = Input.Get("Day2").Select(x => x.Split(" ")).ToList();
            int score = 0;
            foreach (var item in input)
            {
                switch (item[1])
                {
                    case "X": item[1] = "A"; score++; break;
                    case "Y": item[1] = "B"; score += 2; break;
                    case "Z": item[1] = "C"; score += 3; break;
                    default: break;
                }
                if (item[0] == item[1])
                    score += 3;
                else if (winCon[item[1]] == item[0])
                    score += 6;
            }
            Console.WriteLine(score);

        }

        public void Star2()
        {
            var input = Input.Get("Day2").Select(x => x.Split(" ")).ToList();
            int score = 0;
            foreach (var item in input)
            {
                //Lose
                if (item[1] == "X")
                    score += shapePoint[winCon[item[0]]];
                else if (item[1] == "Y")
                    score += 3 + shapePoint[item[0]];
                else
                    score += 6 + shapePoint[lossCon[item[0]]];
            }
            Console.WriteLine(score);
        }
    }
}
