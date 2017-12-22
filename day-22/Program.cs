using System;
using System.Collections.Generic;
using System.IO;

namespace day_22
{
  class Program
  {
    static void Main(string[] args)
    {
      Dictionary<string, Health> nodes = new Dictionary<string, Health>();

      var lines = File.ReadAllLines("input.txt");
      for (var y=0;y<lines.Length;y++)
      {
        for (var x=0;x<lines[y].Length;x++)
        {
          if (lines[y][x] == '#') nodes.Add($"{x},{y}", Health.Infected);
        }
      }

      var carrierX = lines[0].Length / 2;
      var carrierY = lines.Length / 2;
      var direction = Direction.Up;

      long infecting = 0;

      for (var i = 0; i < 10000000; i++)
      {
        if (i % 1000000 == 0) Console.WriteLine(i);

        string key = $"{carrierX},{carrierY}";
        if (!nodes.TryGetValue(key, out Health health))
        {
          health = Health.Clean;
          nodes.Add(key, health);
        }

        int delta = 0;
        if (health == Health.Infected)
          delta = 1;
        else if (health == Health.Clean)
          delta = -1;
        else if (health == Health.Flagged)
          delta = 2;

        direction = (Direction)(((int)direction + delta + 4) % 4);

        Health newHealth = (Health)(((int)health + 5) % 4);
        nodes[key] = newHealth;

        if (newHealth == Health.Infected) infecting++;
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
      }
      Console.WriteLine(infecting);
    }

    enum Direction
    {
      Up = 0, Right = 1,  Down = 2, Left = 3
    }

    enum Health
    {
      Clean = 0,
      Weakened = 1,
      Infected = 2,
      Flagged = 3
    }
  }
}
