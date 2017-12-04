using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_04
{
  class Program
  {
    static void Main(string[] args)
    {
      int valid = 0;
      foreach (string line in File.ReadLines("input.txt").Select(f => f.Trim())) {
        var dict = line.Split(' ').GroupBy(f => f).ToDictionary(f => f.Key, f => f.Count());
        if (dict.All(f => f.Value == 1)) valid++;
      }
      Console.WriteLine(valid);
    }
  }
}
