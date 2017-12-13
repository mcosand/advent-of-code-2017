using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_13
{
  class Program
  {
    static void Main(string[] args)
    {
      Dictionary<int, Scanner> scanners = File.ReadAllLines("input.txt").Select(f => f.Trim().Split(new[] { ": " }, StringSplitOptions.None)).ToDictionary(f => int.Parse(f[0]), f => new Scanner { Range = int.Parse(f[1]) });

      int maxDepth = scanners.Keys.Max();
      int severity = 0;

      for (int i=0;i <= maxDepth; i++)
      {
        if (scanners.TryGetValue(i, out Scanner scanner))
        {
          if (scanner.Position == 0) severity += (i * scanner.Range);
        }

        foreach (var s in scanners.Values)
        {
          if (s.Position == 0 && !s.Forward)
          {
            s.Forward = true;
            s.Position = 1;
          } else if (s.Position == s.Range - 1 && s.Forward)
          {
            s.Forward = false;
            s.Position = s.Range - 2;
          }
          else
          {
            s.Position = s.Position + (s.Forward ? 1 : -1);
          }

        }
      }
      
    }

    class Scanner
    {
      public int Range { get; set; }
      public int Position { get; set; }
      public bool Forward { get; set; } = true;
    }
  }
}
