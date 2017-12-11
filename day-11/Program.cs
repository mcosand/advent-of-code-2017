using System;
using System.IO;
using System.Linq;

namespace day_11
{
  class Program
  {
    static void Main(string[] args)
    {
      int x = 0;
      int y = 0;

      int max = 0;

      foreach (var step in File.ReadAllText("input.txt").Trim().Split(',').Select(f => f.Trim()))
      {
        switch (step)
        {
          case "n":
            y = y - 1;
            break;
          case "ne":
            x += 1;
            break;
          case "se":
            x += 1;
            y += 1;
            break;
          case "s":
            y += 1;
            break;
          case "sw":
            x -= 1;
            break;
          case "nw":
            y -= 1;
            x -= 1;
            break;
        }
        max = Math.Max(max, GetDistance(x, y));
      }
      Console.WriteLine(max);
    }

    static int GetDistance(int x, int y)
    {
      if ((x > 0 && y > 0) || (y < 0 && y < 0))
      {
        return Math.Max(x, y);
      }
      else
      {
        return Math.Abs(x) + Math.Abs(y);
      }
    }
  }
}
