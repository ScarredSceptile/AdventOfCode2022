using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_2022.Days
{
    public class Day13 : Day
    {
        //Incomplete. Hate the parsing of this
        public void Star1()
        {
            var input = Input.GetSingle("Day13").Split("\r\n\r\n");
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var order = input[i].Split("\r\n");
                var first = order[0];
                var second = order[1];

                var firstPack = new ListPackage(first.Remove(first.Length-1, 1).Remove(0, 1));
                var secondPack = new ListPackage(second.Remove(second.Length-1, 1).Remove(0, 1));

                if (firstPack.IsBefore(secondPack))
                {
                    Console.WriteLine("Good");
                    sum += i + 1;
                }
                Console.WriteLine(i+1);
            }
            Console.WriteLine(sum);
        }

        public void Star2()
        {
            throw new NotImplementedException();
        }

        private class Package
        {
            public virtual bool IsBefore(Package otherPack) { return false; }
        }
        private class IntegerPackage : Package
        {
            public List<int> value;
            bool isSingleint;
            public IntegerPackage(List<int> num, bool single) { value = num; isSingleint = single; }
            public override bool IsBefore(Package otherPack)
            {
                if (otherPack is ListPackage)
                {
                    var op = otherPack as ListPackage;
                    if (op.packages.Count == 0)
                    {
                        if (value.Count == 0) return true;
                        else return false;
                    }
                    return IsBefore(op.packages[0]);
                }
                else
                {
                    var op = otherPack as IntegerPackage;
                    if (value.Count > op.value.Count && op.isSingleint == false)
                        return false;
                    for (int i = 0; i < op.value.Count; i++)
                    {
                        if (i == value.Count)
                            return true;
                        if (value[i] > op.value[i])
                            return false;
                    }

                }
                return true;
            }
        }
        private class ListPackage : Package
        {
            public List<Package> packages;
            public ListPackage(string content)
            {
                packages = ParseLists(content);
            }
            public override bool IsBefore(Package otherPack)
            {
                if (otherPack is ListPackage)
                {
                    var op = otherPack as ListPackage;
                    if (packages.Count > op.packages.Count)
                        return false;

                    for (int i = 0; i < packages.Count; i++)
                        if (!packages[i].IsBefore(op.packages[i]))
                            return false;
                }
                else
                {
                    if (packages.Count > 1)
                        return false;
                    if (!packages[0].IsBefore(otherPack))
                        return false;

                }
                return true;
            }
        }

        private static List<Package> ParseLists(string content)
        {
            List<Package> pack = new();
            if (content.Contains('[') == false)
            {
                if (content.Length == 0)
                    return pack;
                pack.Add(new IntegerPackage(content.Split(",").Select(int.Parse).ToList(), false));
                return pack;
            }

            List<Package> list = new();
            string curStr = "";
            for (var i = 0; i < content.Length; i++)
            {
                if (content[i] == '[')
                {
                    int count = 0;
                    for (int j = i+1; j < content.Length; j++)
                    {
                        if (content[j] == '[')
                            count++;
                        else if (content[j] == ']')
                        {
                            if (count > 0)
                                count--;
                            else
                            {
                                if (curStr.Length > 0)
                                {
                                    bool single;
                                    if (curStr.Length == 2)
                                        single = true;
                                    else
                                        single = false;
                                    list.Add(new IntegerPackage(curStr.Remove(curStr.Length-1).Split(",").Select(int.Parse).ToList(), single));
                                }
                                list.Add(new ListPackage(content[(i+1)..(j)]));
                                curStr = "";
                                i = j + 1;
                                break;
                            }
                        }
                    }
                    continue;
                }
                curStr+= content[i];
            }
            if (curStr.Length > 0)
            {
                bool single;
                if (curStr.Length == 1)
                    single = true;
                else
                    single = false;
                list.Add(new IntegerPackage(curStr.Split(",").Select(int.Parse).ToList(), single));
            }
            return list;
        }
    }
}
