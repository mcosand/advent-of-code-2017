using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_03
{
  class Program
  {
    static void Main(string[] args)
    {
      int input = 289326;
      //int input = 23;
      int side = 1;
      while (side * side < input)
      {
        side += 2;
      }
      int[][] grid = new int[side+2][];
      for (int i=0;i<grid.Length;i++)
      {
        grid[i] = new int[side+2];
      }

      int[][] directions = { new int[] { 1, 0 }, new int[] { 0, 1 }, new int[]{ -1, 0 }, new int[] { 0, -1 } };

      int direction = 0;
      int x = 0;
      int y = 0;
      int origin_x = side / 2 + 1;
      int origin_y = side / 2 + 1;
      int radius = 0;
      int val;

      do
      {
        val = 0;
        for (int i = -1; i < 2; i++)
        {
          for (int j = -1; j < 2; j ++)
          {
            val += grid[origin_y - y + i][origin_x + x + j];
          }
        }
        grid[origin_y - y][origin_x + x] = val;
        grid[origin_y][origin_x] = 1;

        //Console.WriteLine($"{value:0000} ({x}, {y}). Distance = {Math.Abs(x) + Math.Abs(y)}");

        if (x == -y && x >= 0)
        {
        //  Console.WriteLine($"Hit corner ({x},{y}), v = {value}");
          radius = radius + 1;
        }

        int new_x = x + directions[direction][0];
        int new_y = y + directions[direction][1];
        //Console.Write($"test {radius} ({new_x}, {new_y}) ");
        if (Math.Abs(new_x) > radius || Math.Abs(new_y) > radius)        {
        //  Console.WriteLine("x");
          direction = (direction + 1) % 4;
          new_x = x + directions[direction][0];
          new_y = y + directions[direction][1];
        //} else
        //{
        //  Console.WriteLine("o");
        }
        x = new_x;
        y = new_y;

        Console.WriteLine(val);



      } while (val < input);

     // Console.WriteLine($"{value} at ({x},{y}), distance = {Math.Abs(x) + Math.Abs(y)}");

      Console.WriteLine(val);      
    }
  }
}
