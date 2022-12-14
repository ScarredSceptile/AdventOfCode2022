using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_Of_Code_2022.Days
{
    public class Day8 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day8");
            Tree[,] forest = new Tree[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    forest[i, j] = new Tree(input[i][j] - '0');
            var totalVisible = FindAllVisibleTrees(forest);
            Console.WriteLine(totalVisible);
        }

        public void Star2()
        {
            var input = Input.Get("Day8");
            Tree[,] forest = new Tree[input.Length, input[0].Length];
            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    forest[i, j] = new Tree(input[i][j] - '0');
            List<int[]> sceneryscores = new();

            for (int i = 0; i < forest.GetLength(0); i++)
            {
                for (int j = 0; j < forest.GetLength(1); j++)
                {
                    int[] scores = new int[4];
                    //UP
                    int k = i - 1;
                    int height = forest[i, j].height;
                    while (k >= 0)
                    {
                        scores[0]++;
                        if (forest[k, j].height >= height)
                        {
                            break;
                        }
                        k--;
                    }

                    //DOWN
                    k = i + 1;
                    while (k < forest.GetLength(0))
                    {
                        scores[1]++;
                        if (forest[k, j].height >= height)
                        {
                            break;
                        }
                        k++;
                    }

                    //LEFT
                    k = j - 1;
                    while (k >= 0)
                    {
                        scores[2]++;
                        if (forest[i, k].height >= height)
                        {
                            break;
                        }
                        k--;
                    }

                    //RIGHT
                    k = j + 1;
                    while (k < forest.GetLength(1))
                    {
                        scores[3]++;
                        if (forest[i, k].height >= height)
                        {
                            break;
                        }
                        k++;
                    }
                    sceneryscores.Add(scores);
                }
            }
            var bestScenery = sceneryscores.Select(s => MultiplyScore(s)).Max();
            Console.WriteLine(bestScenery);
        }

        private int MultiplyScore(int[] scores)
        {
            int score = 1;
            for (int i = 0; i < scores.Length; i++)
                score *= scores[i];
            return score;
        }

        private class Tree
        {
            public int height;
            public bool visible;

            public Tree(int h) { height = h; visible = false; }
        }

        private int FindAllVisibleTrees(Tree[,] forest)
        {
            int newVisible = 0;
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                int top = -1;
                int bottom = -1;
                int length = forest.GetLength(1);
                for (int j = 0; j < length; j++)
                {
                    //Console.WriteLine($"i: {i}, j: {j}");
                    if (forest[i, j].height > top)
                    {
                        top = forest[i, j].height;
                        if (!forest[i, j].visible)
                        {
                            forest[i, j].visible = true;
                            newVisible++;
                        }
                    }
                    if (forest[i, length - 1 - j].height > bottom)
                    {
                        bottom = forest[i, length - 1 - j].height;
                        if (!forest[i, length - 1 - j].visible)
                        {
                            forest[i, length - 1 - j].visible = true;
                            newVisible++;
                        }
                    }
                }
            }
            for (int i = 0; i < forest.GetLength(0); i++)
            {
                int top = -1;
                int bottom = -1;
                int length = forest.GetLength(1);
                for (int j = 0; j < length; j++)
                {
                    //Console.WriteLine($"i: {i}, j: {j}");
                    if (forest[j, i].height > top)
                    {
                        top = forest[j, i].height;
                        if (!forest[j, i].visible)
                        {
                            forest[j, i].visible = true;
                            newVisible++;
                        }
                    }
                    if (forest[length - 1 - j, i].height > bottom)
                    {
                        bottom = forest[length - 1 - j, i].height;
                        if (!forest[length - 1 - j, i].visible)
                        {
                            forest[length - 1 - j, i].visible = true;
                            newVisible++;
                        }
                    }
                }
            }
            return newVisible;
        }
    }
}
