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
      }
      Console.WriteLine($"{x} {y}");
    }
  }
}
