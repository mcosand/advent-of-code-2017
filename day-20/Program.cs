using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_20
{
  class Program
  {
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines("input.txt")
        .Select(f => new Particle(f))
        .ToList();

      for (var s = 0;s<1000000;s++)
      {
        var groups = lines.Select((f, i) => new { i = i, p = f }).GroupBy(f => f.p.P.ToString()).Where(f => f.Count() > 1).SelectMany(f => f.ToArray()).ToArray();

        if (groups.Length > 0)
        {
          Console.WriteLine("Removing " + groups.Length);

          foreach (var gone in groups.OrderByDescending(f => f.i))
          {           
            lines.RemoveAt(gone.i);
          }
        }

        for (var i=0;i<lines.Count; i++)
        {
          var p = lines[i];
          p.V = new Tuple<int, int, int>(p.V.Item1 + p.A.Item1, p.V.Item2 + p.A.Item2, p.V.Item3 + p.A.Item3);
          p.P = new Tuple<int, int, int>(p.P.Item1 + p.V.Item1, p.P.Item2 + p.V.Item2, p.P.Item3 + p.V.Item3);
        }
      }
    }


  }
  class Particle
  {
    public Particle(string args)
    {
      var match = Regex.Match(args, "p=\\<([ \\d-]+),([ \\d-]+),([ \\d-]+)\\>, v=\\<([ \\d-]+),([ \\d-]+),([ \\d-]+)\\>, a=\\<([ \\d-]+),([ \\d-]+),([ \\d-]+)\\>");

      P = Parse(match, 1);
      V = Parse(match, 4);
      A = Parse(match, 7);

    }

    Tuple<int,int,int> Parse(Match m, int i)
    {
      return new Tuple<int,int,int>(int.Parse(m.Groups[i].Value.Trim()), int.Parse(m.Groups[i+1].Value.Trim()), int.Parse(m.Groups[i + 2].Value.Trim()));
    }

    public Tuple<int, int, int> P { get; set; }
    public Tuple<int, int, int> V { get; set; }
    public Tuple<int, int, int> A { get; set; }

    public int AccelDistance {  get { return Math.Abs(A.Item1) + Math.Abs(A.Item2) + Math.Abs(A.Item3);  } }
  }
}
