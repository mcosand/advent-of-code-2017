using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace day_22
{
  class Program
  {
    static void Main(string[] args)
    {
      Dictionary<string, Health> nodes = new Dictionary<string, Health>();

      var lines = File.ReadAllLines("input.txt");
      for (var y = 0; y < lines.Length; y++)
      {
        for (var x = 0; x < lines[y].Length; x++)
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
      ToImage(nodes);
    }

    static void ToImage(Dictionary<string, Health> grid)
    {
      int minX = 0, minY = 0, maxX = 0, maxY = 0;

      foreach (var key in grid.Keys)
      {
        int[] coords = key.Split(',').Select(g => int.Parse(g)).ToArray();
        if (coords[0] < minX) minX = coords[0];
        else if (coords[0] > maxX) maxX = coords[0];
        if (coords[1] < minY) minY = coords[1];
        else if (coords[1] > maxY) maxY = coords[1];
      }



      using (var b = new Bitmap(maxX - minX, maxY - minY))
      {
        using (Graphics g = Graphics.FromImage(b))
        {
          g.Clear(Color.White);
          g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

          Brush[] brushes = new[] { new SolidBrush(Color.FromArgb(248, 248, 248)), new SolidBrush(Color.FromArgb(128, 0, 255, 0)), new SolidBrush(Color.FromArgb(255, 0, 0)), new SolidBrush(Color.FromArgb(128, 0, 0, 255)) };
          foreach (var pair in grid)
          {
            int[] coords = pair.Key.Split(',').Select(f => int.Parse(f)).ToArray();
            g.FillRectangle(brushes[(int)pair.Value], coords[0] - minX, coords[1] - minY, 1, 1);
          }

          foreach (var brush in brushes)
          {
            brush.Dispose();
          }
        }
        b.Save("output.png");
      }
    }

    enum Direction
    {
      Up = 0, Right = 1, Down = 2, Left = 3
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
