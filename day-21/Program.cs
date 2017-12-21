using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace day_21
{
  class Program
  {
    static Dictionary<string, string> rules;

    static void Main(string[] args)
    {
      rules = File.ReadAllLines("input.txt").Select(f => f.Split(new[] { " => " }, StringSplitOptions.None)).ToDictionary(f => f[0], f=> f[1]);

      List<string> grids = new List<string> { ".#./..#/###" };

      int answer = 0;
      for (var c = 0; c < 5; c++)
      {
        Console.WriteLine("starting iteration " + (c + 1));
        List<string> nextGrids = new List<string>();
        for (var g = 0; g < grids.Count; g++)
        {
          if (!RuleSearch(grids, nextGrids, g))
          {
            Console.WriteLine("Can't find rule for " + grids[g]);
          }
        }
        grids = nextGrids;
        //answer = grids.Sum(f => f.ToCharArray().Where(g => g == '#').Count());
        answer = string.Join("", grids).Replace(".", "").Replace("/", "").Length;
      }
      
    }

    private static bool RuleSearch(List<string> grids, List<string> nextGrids, int g)
    {
      var orig = grids[g];
      for (var f = 0; f < 3; f++)
      {
        if (f == 0)
        {
        }
        else if (f == 1)
        {
          grids[g] = VFlipGrid(grids[g]);
        }
        else
        {
          grids[g] = HFlipGrid(grids[g]);
        }


        for (var i = 0; i < 4; i++)
        {
          //Console.WriteLine(grids[g]);
          if (rules.TryGetValue(grids[g], out string match))
          {
            dumpMatch(orig, grids[g]);
            if (match.Length == 11)
            {
              // new grid is 3x3;
              nextGrids.Add(match);
            }
            else
            {
              nextGrids.Add(new string(new[] { match[0], match[1], '/', match[5], match[6] }));
              nextGrids.Add(new string(new[] { match[2], match[3], '/', match[7], match[8] }));
              nextGrids.Add(new string(new[] { match[10], match[11], '/', match[15], match[16] }));
              nextGrids.Add(new string(new[] { match[12], match[13], '/', match[17], match[18] }));
            }
            return true;
            // do split
          }
          grids[g] = RotateGrid(grids[g]);
        }
      }
      return false;
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
      var l = left.Split('/');
      var r = right.Split('/');
      for (var i=0;i<l.Length;i++)
      {
        Console.WriteLine($"{l[i]}     {r[i]}");
      }
      Console.WriteLine();
    }

    static void dumpAll(List<string> grids)
    {
      for (var i=0;i<Math.Sqrt(grids.Count);i++)
      {

      }
    }
  }
}
