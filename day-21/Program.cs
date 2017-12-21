using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace day_21
{
  class Program
  {
    static Dictionary<string, string> rules;

    static void Main(string[] args)
    {
      rules = File.ReadAllLines("input.txt").Select(f => f.Split(new[] { " => " }, StringSplitOptions.None)).ToDictionary(f => f[0], f=> f[1]);

      string theGrid = ".#./..#/###";

      int answer = 0;
      for (var c = 0; c < 18; c++)
      {
        List<string> grids = SplitGrid(theGrid);
        List<string> nextGrids = new List<string>();
        for (var g = 0; g < grids.Count; g++)
        {
          nextGrids.Add(RuleSearch(grids[g]));
        }
        theGrid = MergeGrid(nextGrids);
        answer = theGrid.Where(g => g == '#').Count();
        Console.WriteLine($"{c + 1}: {answer}");
      }
    }

    private static string RuleSearch(string grid)
    {
      var orig = grid;
      for (var f = 0; f < 3; f++)
      {
        grid = orig;
        if (f == 0)
        {
        }
        else if (f == 1)
        {
          grid = VFlipGrid(grid);
        }
        else
        {
          grid = HFlipGrid(grid);
        }


        for (var i = 0; i < 4; i++)
        {
          if (rules.TryGetValue(grid, out string match))
          {
            dumpMatch(orig, grid);
            return match;
          }
          grid = RotateGrid(grid);
        }
      }
      throw new ApplicationException("Rule not found");
    }

    static int[] rotate3 = new int[] { 8, 4, 0, 3, 9, 5, 1, 7, 10, 6, 2, 11 };
    static int[] rotate2 = new int[] { 3, 0, 2, 4, 1, 5 };
    
    static string RotateGrid(string grid)
    {
      char[] newChars = new char[grid.Length];
      for (var i=0;i<grid.Length;i++)
      {
        newChars[i] = grid[(grid.Length == 11 ? rotate3 : rotate2)[i]];
      }
      return new string(newChars);
    }

    static string VFlipGrid(string grid)
    {
      var lines = grid.Split('/');
      var swap = lines[0];
      lines[0] = lines[lines.Length - 1];
      lines[lines.Length - 1] = swap;
      return string.Join("/", lines);
    }

    static string HFlipGrid(string grid)
    {
      var lines = grid.Split('/');
      return string.Join("/", lines.Select(f => new string(f.Reverse().ToArray())));
    }

    static void dumpMatch(string left, string right)
    {
      return;
      var l = left.Split('/');
      var r = right.Split('/');
      for (var i=0;i<l.Length;i++)
      {
        Console.WriteLine($"{l[i]}     {r[i]}");
      }
      Console.WriteLine();
    }

    static List<string> SplitGrid(string grid)
    {
      var lines = grid.Split('/');
      var gridSize = lines.Length % 2 == 0 ? 2 : 3;

      Debug.Assert(lines[0].Length == lines.Length);

      var splits = new List<string>();

      for (var gridY = 0; gridY < lines.Length / gridSize; gridY++)
      {
        for (var gridX = 0; gridX < lines.Length / gridSize; gridX++)
        {
          splits.Add(
            string.Join("/",
            Enumerable.Range(0, gridSize)
            .Select(innerY => new string(lines[gridY * gridSize + innerY].Skip(gridX * gridSize).Take(gridSize).ToArray()))));
        }
      }
      return splits;
    }

    static string MergeGrid(List<string> grids)
    {
      if (grids.Count == 1) return grids[0];

      int srcSize = grids[0].IndexOf('/');
      int gridsOnSide = (int)Math.Sqrt(grids.Count);
      int targetSize = gridsOnSide * srcSize;

      var rows = new List<string>();

      for (var outerY = 0; outerY < gridsOnSide; outerY++)
      {
        for (var innerY = 0; innerY < srcSize; innerY++)
        {
          rows.Add(string.Join("", Enumerable.Range(0,gridsOnSide)
            .Select(g => g + (outerY * gridsOnSide))
            .Select(g => grids[g].Split('/')[innerY])

            ));
        }
      }

      return string.Join("/", rows);
    }
  }
}
