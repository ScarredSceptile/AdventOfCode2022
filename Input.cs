using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent_Of_Code_2022
{
    public static class Input
    {
        public static string[] Get(string file)
        {
            var dir = Directory.GetCurrentDirectory();
            var measurements = File.ReadAllLines(dir.Substring(0, dir.IndexOf("bin")) + @"\Input\" + file + ".txt");
            return measurements;
        }
    }
}
