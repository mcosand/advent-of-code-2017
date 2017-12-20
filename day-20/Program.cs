using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day_20
{
  class Program
  {
    static void Main(string[] args)
    {
      var lines = File.ReadAllLines("input.txt")
        .Select(f => new Particle(f))
        .ToArray();

      int minLength = int.MaxValue;
      int index = 0;

      for (var i = 0; i < lines.Length; i++)
      {
        if (lines[i].AccelDistance < minLength)
        {
          minLength = lines[i].AccelDistance;
          index = i;
        }
      }

      Console.WriteLine(index);
    }


  }
  class Particle
  {
    public Particle(string args)
    {
      var match = Regex.Match(args, "p=\\<([ \\d-]+),([ \\d-]+),([ \\d-]+)\\>, v=\\<([ \\d-]+),([ \\d-]+),([ \\d-]+)\\>, a=\\<([ \\d-]+),([ \\d-]+),([ \\d-]+)\\>");

      Position = Parse(match, 1);
      Velocity = Parse(match, 4);
      Acceleration = Parse(match, 7);

    }

    Tuple<int,int,int> Parse(Match m, int i)
    {
      return new Tuple<int,int,int>(int.Parse(m.Groups[i].Value.Trim()), int.Parse(m.Groups[i+1].Value.Trim()), int.Parse(m.Groups[i + 2].Value.Trim()));
    }

    public Tuple<int, int, int> Position { get; set; }
    public Tuple<int, int, int> Velocity { get; set; }
    public Tuple<int, int, int> Acceleration { get; set; }

    public int AccelDistance {  get { return Math.Abs(Acceleration.Item1) + Math.Abs(Acceleration.Item2) + Math.Abs(Acceleration.Item3);  } }
  }
}
