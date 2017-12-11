using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace day_11
{
  class Program
  {
    static void Main(string[] args)
    {
      int minX = 0, minY = 0, maxX = 0, maxY = 0;

      WalkSteps((x, y) =>
      {
        minX = Math.Min(x, minX);
        minY = Math.Min(y, minY);
        maxX = Math.Max(x, maxX);
        maxY = Math.Max(y, maxY);
      });

      using (var b = new Bitmap(maxX - minX, maxY - minY))
      {
        using (Graphics g = Graphics.FromImage(b))
        {
          g.Clear(Color.White);
          g.FillEllipse(new SolidBrush(Color.Red), -minX - 5, -minY - 5, 10, 10);
          var pen = new Pen(Color.Black);
          int x = 0;
          int y = 0;
          WalkSteps((x2, y2) =>
          {
            g.DrawLine(pen, x - minX, y - minY, x2 - minX, y2 - minY);
            x = x2;
            y = y2;
          });
        }
        b.Save("output.png");
      }
    }

    public static void WalkSteps(Action<int, int> onStep)
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
        onStep(x, y);
      }
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
