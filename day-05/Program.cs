using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_05
{
  class Program
  {
    static void Main(string[] args)
    {
      var offsets = File.ReadAllLines("input.txt").Select(f => int.Parse(f)).ToArray();
      var pc = 0;
      var steps = 0;
      while (pc >= 0 && pc < offsets.Length)
      {
        var old = pc;
        pc = pc + offsets[pc]++;
        steps++;
      }
      Console.WriteLine(steps);
    }
  }
}
