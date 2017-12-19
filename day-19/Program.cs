using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_19
{
  class Program
  {
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines("input.txt");
      var maxWidth = lines.Max(f => f.Length);


      var grid = lines.Select(f => f.PadRight(maxWidth).ToCharArray().ToList()).ToList();


      var y = 0;
      var x = grid[y].IndexOf('|');
      var direction = 's';
      string message = "";

      while (grid[y][x] != ' ')
      {
        while (grid[y][x] != '+')
        {
          if (!new[] { '+', '-', '|', ' ' }.Contains(grid[y][x])) message += grid[y][x];

          if (direction == 's')
          {
            y++;
          }
          else if (direction == 'n')
          {
            y--;
          }
          else if (direction == 'e')
          {
            x++;
          }
          else
          {
            x--;
          }
        }

        if ((direction == 's' || direction == 'n') && x < maxWidth - 1 && grid[y][x + 1] == '-') { direction = 'e'; x++; }
        else if ((direction == 's' || direction == 'n') && x > 0 && grid[y][x - 1] == '-') { direction = 'w'; x--; }
        else if ((direction == 'w' || direction == 'e') && y > 0 && grid[y - 1][x] == '|') { direction = 'n'; y--; }
        else if ((direction == 'w' || direction == 'e') && y < lines.Length - 1 && grid[y + 1][x] == '|') { direction = 's'; y++; }
        else
        {
          Console.WriteLine("I don't know!");
        }
      }
    }
  }
}
