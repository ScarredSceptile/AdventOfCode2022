using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    //Sand in 2 and Stone is 1 while Air is 0
    public class Day14 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day14");
            var Grid = ParseStones(input);
            var lowest = Grid.Select(n => n.Location.Y).Max();

            while (true)
            {
                Vector2 sand = new(500,0);
                if (!MoveSand(sand, Grid, lowest, false))
                    break;
                Console.WriteLine(Grid.Where(n => n.content == 2).Count());
            }
            Console.WriteLine(Grid.Where(n => n.content == 2).Count());
        }

        public void Star2()
        {
            var input = Input.Get("Day14");
            var Grid = ParseStones(input);
            var lowest = Grid.Select(n => n.Location.Y).Max();

            while (true)
            {
                Vector2 sand = new(500, 0);
                if (!MoveSand(sand, Grid, lowest, true))
                    break;
                Console.WriteLine(Grid.Where(n => n.content == 2).Count());
            }
            Console.WriteLine(Grid.Where(n => n.content == 2).Count());
        }

        private bool MoveSand(Vector2 sand, List<Tile> Grid, float lowest, bool infiniteGround)
        {
            if (sand.Y > lowest)
            {
                if (infiniteGround)
                {
                    Grid.Add(new Tile(2, sand));
                    return true;
                }
                else return false;
            }

            if (Grid.Where(n => n.Location.X == 500 && n.Location.Y == 0).Any())
                return false;

            if (Grid.Where(n => n.Location.X == sand.X && n.Location.Y == sand.Y + 1).Any() == false)
            {
                sand.Y++;
                return (MoveSand(sand, Grid, lowest, infiniteGround));
            }
            else if (Grid.Where(n => n.Location.X == sand.X - 1 && n.Location.Y == sand.Y + 1).Any() == false)
            {
                sand.X--;
                sand.Y++;
                return (MoveSand(sand, Grid, lowest, infiniteGround));
            }
            else if (Grid.Where(n => n.Location.X == sand.X + 1 && n.Location.Y == sand.Y + 1).Any() == false)
            {
                sand.X++;
                sand.Y++;
                return MoveSand(sand, Grid, lowest, infiniteGround);
            }
            else
            {
                Grid.Add(new Tile(2, sand));
                return true;
            }
        }

        private List<Tile> ParseStones(string[] input)
        {
            List<Tile> Grid = new();
            foreach (var line in input)
            {
                var corners = line.Split(" -> ");
                var curTile = corners[0].Split(",").Select(int.Parse).ToArray();
                if (Grid.Where(x => x.Location.X == curTile[0] && x.Location.Y == curTile[1]).Any() == false)
                    Grid.Add(new Tile(1, curTile[0], curTile[1]));
                for (int i = 1; i < corners.Length; i++)
                {
                    var tile = corners[i].Split(",").Select(int.Parse).ToArray();
                    if (curTile[0] == tile[0])
                    {
                        if (curTile[1] < tile[1])
                        {
                            for (int j = curTile[1]; j <= tile[1]; j++)
                                if (Grid.Where(x => x.Location.X == curTile[0] && x.Location.Y == j).Any() == false)
                                    Grid.Add(new Tile(1, curTile[0], j));
                        }

                        else
                            for (int j = curTile[1]; j >= tile[1]; j--)
                                if (Grid.Where(x => x.Location.X == curTile[0] && x.Location.Y == j).Any() == false)
                                    Grid.Add(new Tile(1, curTile[0], j));
                    }
                    else
                    {
                        if (curTile[0] < tile[0])
                        {
                            for (int j = curTile[0]; j <= tile[0]; j++)
                                if (Grid.Where(x => x.Location.X == j && x.Location.Y == curTile[1]).Any() == false)
                                    Grid.Add(new Tile(1, j, curTile[1]));
                        }

                        else
                            for (int j = curTile[0]; j >= tile[0]; j--)
                                if (Grid.Where(x => x.Location.X == j && x.Location.Y == curTile[1]).Any() == false)
                                    Grid.Add(new Tile(1, j, curTile[1]));
                    }
                    curTile = tile;
                }
            }
            return Grid;
        }

        private class Tile
        {
            public int content;
            public Vector2 Location;
            public Tile(int content, int X, int Y) { this.content = content; this.Location = new Vector2(X, Y); }
            public Tile(int content, Vector2 loc) { this.content = content; this.Location = loc; }
        }
    }
}
