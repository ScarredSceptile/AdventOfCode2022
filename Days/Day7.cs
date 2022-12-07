using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2022.Days
{
    public class Day7 : Day
    {
        public void Star1()
        {
            var root = GetSystem(Input.Get("Day7"));
            var total = root.GetSize();
            Console.WriteLine(total.sum);
        }

        public void Star2()
        {
            var root = GetSystem(Input.Get("Day7"));
            var total = root.GetSize();
            long totalSpace = 70000000;
            long required = 30000000;
            long available = totalSpace - total.size;
            long missing = required - available;
            var allSizes = root.GetAllSizes();
            Console.WriteLine(allSizes.Where(s => s >= missing).OrderBy(s => s).First());
        }

        private class Directory
        {
            public string name;
            public Directory parent;
            public List<Directory> children;
            public List<File> files;
            public long dirSize;

            private static long maxSize = 100000;

            public Directory(string name, Directory parent)
            {
                this.name = name;
                this.parent = parent;
                children = new List<Directory>();
                files = new List<File>();
            }

            public (long size, long sum) GetSize()
            {
                dirSize = files.Sum(f => f.Size);
                long thisSum = 0;
                foreach (Directory child in children)
                {
                    (long size, long sum) = child.GetSize();
                    dirSize += size;
                    thisSum += sum;
                }
                if (dirSize <= maxSize)
                    thisSum += dirSize;
                return (dirSize, thisSum);
            }

            public List<long> GetAllSizes()
            {
                List<long> sizes = new List<long>();
                foreach (Directory child in children)
                    sizes.AddRange(child.GetAllSizes());
                sizes.Add(dirSize);
                return sizes;
            }
        }

        private class File
        {
            public string Name;
            public long Size;

            public File(string name, long size) { Name = name; Size = size; }
        }

        private Directory GetSystem(string[] input)
        {
            var root = new Directory("/", null);
            var curDir = root;

            for (int i = 1; i < input.Length; i++)
            {
                var command = input[i].Split(" ");
                if (command[1] == "ls")
                {
                    int count = 1;
                    do
                    {
                        var file = input[i + count].Split(" ");
                        if (IsNumeric(file[0]))
                            curDir.files.Add(new File(file[1], long.Parse(file[0])));
                        else
                            curDir.children.Add(new Directory(file[1], curDir));
                        count++;
                        if (i + count == input.Length)
                            break;
                    } while (input[i + count].StartsWith("$") == false);
                    i += count - 1;
                }
                else
                {
                    if (command[2] == "/")
                        curDir = root;
                    else if (command[2] == "..")
                        curDir = curDir.parent;
                    else
                        curDir = curDir.children.Where(c => c.name == command[2]).FirstOrDefault();
                }
            }
            return root;
        }

        private bool IsNumeric(string input)
        {
            return input.All(c => char.IsDigit(c));
        }
    }
}
