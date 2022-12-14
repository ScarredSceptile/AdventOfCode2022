using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2022.Days
{
    public class Day5 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day5").Split("\r\n\r\n");
            var data = input[0].Split("\n");
            var stack = ParseStack(data);
            var instructions = input[1].Split("\n");

            foreach (var instruction in instructions)
            {
                var items = instruction.Split(" ");
                var amount = int.Parse(items[1]);
                var from = int.Parse(items[3]) - 1;
                var to = int.Parse(items[5]) - 1;
                for (int i = 0; i < amount; i++)
                {
                    var item = stack[from][^1];
                    stack[from].RemoveAt(stack[from].Count - 1);
                    stack[to].Add(item);
                }
            }
            foreach (var item in stack)
                Console.Write(item[^1]);
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day5").Split("\r\n\r\n");
            var data = input[0].Split("\n");
            var stack = ParseStack(data);
            var instructions = input[1].Split("\n");

            foreach (var instruction in instructions)
            {
                var items = instruction.Split(" ");
                var amount = int.Parse(items[1]);
                var from = int.Parse(items[3]) - 1;
                var to = int.Parse(items[5]) - 1;

                var stackItems = stack[from].GetRange(stack[from].Count - amount, amount);
                stack[from].RemoveRange(stack[from].Count - amount, amount);
                stack[to].AddRange(stackItems);
            }
            foreach (var item in stack)
                Console.Write(item[^1]);
        }

        private List<char>[] ParseStack(string[] data)
        {
            var count = data[data.Length - 1].Where(c => char.IsDigit(c)).Count();
            List<char>[] stack = new List<char>[count];
            for (int i = 0; i < stack.Length; i++)
                stack[i] = new List<char>();
            for (int i = data.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < data[i].Length; j += 4)
                {
                    if (data[i][j + 1] != ' ')
                        stack[j / 4].Add(data[i][j + 1]);
                }
            }
            return stack;
        }
    }
}
