using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_24
{
  class Program
  {
    static void Main(string[] args)
    {
      var parts = File.ReadAllLines("input.txt").Select(f => f.Split('/').Select(g => int.Parse(g)).ToArray()).ToList();

      var answer = GetLongest(parts, 0);
      Console.WriteLine(answer.Strength);
    }

    static Max GetLongest(List<int[]> parts, int start)
    {
      Max max = new Max { Strength = 0, Length = 0, Path = "" };
      foreach (var part in parts)
      {
        if (part[0] == start || part[1] == start)
        {
          var list = parts.Where(f => f != part).ToList();
          var child = GetLongest(list, part[0] == start ? part[1] : part[0]);
          var strength = part[0] + part[1] + child.Strength;
          var length = 1 + child.Length;
          if (length > max.Length || (length == max.Length && strength > max.Strength))
            max = new Max
            {
              Strength = strength,
              Length = 1 + child.Length,
              Path = string.Format(part[0] == start ? "{0}/{1}--{2}" : "{1}/{0}--{2}", part[0], part[1], child.Path)
            };
        }
      }
      return max;
    }

    struct Max
    {
      public int Strength;
      public int Length;
      public string Path;
    }
  }
}
