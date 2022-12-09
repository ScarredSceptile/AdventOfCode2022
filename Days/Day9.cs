using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Advent_Of_Code_2022.Days
{
    public class Day9 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day9");
            var head = new Vector2(0, 0);
            var tail = new Tail();

            foreach (var item in input)
            {
                var task = item.Split(" ");
                var dir = GetDirection(task[0]);
                var amount =  int.Parse(task[1]);

                for (var i = 0; i < amount; i++)
                {
                    var prev = head;
                    head += dir;

                    if (Math.Abs(tail.CurPos.X - head.X) > 1 || Math.Abs(tail.CurPos.Y - head.Y) > 1)
                    {
                        tail.CurPos = prev;
                        if (tail.Visited.Where(t => t.Equals(prev)).Count() == 0)
                            tail.Visited.Add(prev);
                    }
                }
            }
            Console.WriteLine(tail.Visited.Count());
        }

        public void Star2()
        {
            throw new NotImplementedException();
        }

        private class Tail
        {
            public Vector2 CurPos;
            public List<Vector2> Visited;

            public Tail() { CurPos = new Vector2(0, 0); Visited = new List<Vector2>(); Visited.Add(CurPos); }
        }

        private Vector2 GetDirection(string dir)
        {
            if (dir == "U")
                return new Vector2(1, 0);
            else if (dir == "D")
                return new Vector2(-1, 0);
            else if (dir == "R")
                return new Vector2(0, 1);
            else
                return new Vector2(0, -1);
        }
    }
}
