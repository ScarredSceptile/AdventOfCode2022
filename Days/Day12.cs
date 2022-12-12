using System;
using System.Collections.Generic;

namespace Advent_Of_Code_2022.Days
{
    public class Day12 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day12");
            var map = BuildMap(input);
            Tile startingTile = null;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (j + 1 < map.GetLength(1))
                        if (map[i, j + 1].height - map[i, j].height <= 1)
                            map[i, j].canVisit.Add(map[i, j + 1]);
                    if (j - 1 >= 0)
                        if (map[i, j - 1].height - map[i, j].height <= 1)
                            map[i, j].canVisit.Add(map[i, j - 1]);
                    if (i + 1 < map.GetLength(0))
                        if (map[i + 1, j].height - map[i, j].height <= 1)
                            map[i, j].canVisit.Add(map[i + 1, j]);
                    if (i - 1 >= 0)
                        if (map[i - 1, j].height - map[i, j].height <= 1)
                            map[i, j].canVisit.Add(map[i - 1, j]);
                    if (map[i, j].start)
                        startingTile = map[i, j];
                }
            }
            Console.WriteLine(FindDistance(startingTile, false));
        }

        public void Star2()
        {
            var input = Input.Get("Day12");
            var map = BuildMap(input);
            Tile endTile = null;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (j + 1 < map.GetLength(1))
                        if (map[i, j].height - map[i, j + 1].height <= 1)
                            map[i, j].canVisit.Add(map[i, j + 1]);
                    if (j - 1 >= 0)
                        if (map[i, j].height - map[i, j - 1].height <= 1)
                            map[i, j].canVisit.Add(map[i, j - 1]);
                    if (i + 1 < map.GetLength(0))
                        if (map[i, j].height - map[i + 1, j].height <= 1)
                            map[i, j].canVisit.Add(map[i + 1, j]);
                    if (i - 1 >= 0)
                        if (map[i, j].height - map[i - 1, j].height <= 1)
                            map[i, j].canVisit.Add(map[i - 1, j]);
                    if (map[i, j].end)
                        endTile = map[i, j];
                }
            }
            Console.WriteLine(FindDistance(endTile, true));
        }

        private Tile[,] BuildMap(string[] input)
        {
            Tile[,] map = new Tile[input[0].Length, input.Length];
            int y = 0;
            foreach (var line in input)
            {
                int x = 0;
                foreach (var pos in line)
                {
                    if (pos == 'S')
                        map[x, y] = new Tile('a', true, false);
                    else if (pos == 'E')
                        map[x, y] = new Tile('z', false, true);
                    else
                        map[x, y] = new Tile(pos, false, false);
                    x++;
                }
                y++;
            }
            return map;
        }

        private int FindDistance(Tile startingTile, bool anyA)
        {
            startingTile.visited = true;
            Queue<Tile> queue = new Queue<Tile>();
            queue.Enqueue(startingTile);

            while (queue.Count > 0)
            {
                var tile = queue.Dequeue();
                if (tile.end && !anyA)
                    return tile.distance;
                if (anyA && tile.height == 'a')
                    return tile.distance;
                foreach (var adjacent in tile.canVisit)
                {
                    if (adjacent.visited == false)
                    {
                        adjacent.visited = true;
                        adjacent.distance = tile.distance + 1;
                        queue.Enqueue(adjacent);
                    }
                }
            }
            return -1;
        }

        private class Tile
        {
            public char height;
            public bool visited;
            public bool start;
            public bool end;
            public List<Tile> canVisit;
            public int distance;

            public Tile(char h, bool isStart, bool isEnd)
            {
                height = h;
                visited = false;
                start = isStart;
                end = isEnd;
                canVisit = new List<Tile>();
                distance = 0;
            }
        }
    }
}
