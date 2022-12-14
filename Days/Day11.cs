using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2022.Days
{
    public class Day11 : Day
    {
        private int mod = 1;

        public void Star1()
        {
            var input = Input.GetSingle("Day11").Split("\r\n\r\n");
            var monkeys = ParseMonkeys(input);
            Console.WriteLine(GetWorryLevel(20, monkeys, true));
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day11").Split("\r\n\r\n");
            var monkeys = ParseMonkeys(input);
            Console.WriteLine(GetWorryLevel(10000, monkeys, false));
        }

        private long GetWorryLevel(int rounds, List<Monkey> monkeys, bool divide)
        {
            for (int i = 0; i < rounds; i++)
            {
                foreach (Monkey monkey in monkeys)
                {
                    while (monkey.items.Count > 0)
                    {
                        var item = monkey.items[0];
                        monkey.items.RemoveAt(0);
                        item = monkey.op.operation(item);
                        if (divide)
                            item /= 3;
                        else
                            item %= mod;
                        if (item % monkey.divisible == 0)
                            monkeys.FirstOrDefault(m => m.id == monkey.throwTrue).items.Add(item);
                        else
                            monkeys.FirstOrDefault(m => m.id == monkey.throwFalse).items.Add(item);
                        monkey.inspected++;
                    }
                }
            }
            var values = monkeys.OrderByDescending(m => m.inspected).Select(m => m.inspected).ToArray();
            return values[0] * values[1];
        }

        private List<Monkey> ParseMonkeys(string[] input)
        {
            List<Monkey> monkeyList = new List<Monkey>();
            foreach (var item in input)
            {
                var rows = item.Split("\r\n");
                var id = rows[0].Split(" ")[1][0] - '0';
                var items = rows[1].Split(":")[1].Split(",").Select(n => long.Parse(n)).ToList();
                var opItems = rows[2].Split(" ");
                Operation op;
                if (opItems[^1] == "old")
                    op = new OpSelf(0);
                else
                {
                    if (opItems[^2] == "+")
                        op = new OpAdd(int.Parse(opItems[^1]));
                    else
                        op = new OpMult(int.Parse(opItems[^1]));
                }
                var divisible = int.Parse(rows[3].Split(" ")[^1]);
                mod *= divisible;
                var throwTrue = int.Parse(rows[4].Split(" ")[^1]);
                var throwFalse = int.Parse(rows[5].Split(" ")[^1]);

                var monkey = new Monkey(id, items, op, divisible, throwTrue, throwFalse);
                monkeyList.Add(monkey);
            }
            return monkeyList;
        }

        private class Monkey
        {
            public int id;
            public List<long> items;
            public Operation op;
            public int divisible;
            public int throwTrue;
            public int throwFalse;
            public long inspected;

            public Monkey(int id, List<long> items, Operation o, int divisible, int tru, int fals)
            {
                this.id = id;
                this.items = items;
                op = o;
                this.divisible = divisible;
                throwTrue = tru;
                throwFalse = fals;
                inspected = 0;
            }
        }

        private class Operation
        {
            protected int rate;
            public virtual long operation(long old) { return old; }
            public Operation(int rate) { this.rate = rate; }
        }

        private class OpMult : Operation
        {
            public OpMult(int rate) : base(rate) { }
            public override long operation(long old) { return old * rate; }
        }
        private class OpAdd : Operation
        {
            public OpAdd(int rate) : base(rate) { }
            public override long operation(long old) { return old + rate; }
        }
        private class OpSelf : Operation
        {
            public OpSelf(int rate) : base(rate) { }
            public override long operation(long old) { return old * old; }
        }
    }
}
