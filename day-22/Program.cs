using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_22
{
  class Program
  {
    static void Main(string[] args)
    {
      Dictionary<string, bool> nodes = new Dictionary<string, bool>();

      var lines = File.ReadAllLines("input.txt");
      for (var y=0;y<lines.Length;y++)
      {
        for (var x=0;x<lines[y].Length;x++)
        {
          if (lines[y][x] == '#') nodes.Add($"{x},{y}", true);
        }
      }

      var carrierX = lines[0].Length / 2;
      var carrierY = lines.Length / 2;
      var direction = Direction.Up;

      long infecting = 0;

      for (var i = 0; i < 10000; i++)
      {
        string key = $"{carrierX},{carrierY}";
        if (!nodes.TryGetValue(key, out bool infected))
        {
          infected = false;
          nodes.Add(key, false);
        }

        direction = (Direction)(((int)direction + (infected ? 1 : -1) + 4) % 4);
        nodes[key] = !infected;
        if (!infected) infecting++;

        switch (direction)
        {
          case Direction.Up:
            carrierY--;
            break;
          case Direction.Down:
            carrierY++;
            break;
          case Direction.Left:
            carrierX--;
            break;
          case Direction.Right:
            carrierX++;
            break;
          default:
            break;
        }

     //   Console.WriteLine($"{carrierX},{carrierY}  {direction}  {!infected}");

      }
    }

    enum Direction
    {
      Up = 0, Right = 1,  Down = 2, Left = 3
    }
  }
}
