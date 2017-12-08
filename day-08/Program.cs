using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace day_08
{
  class Program
  {
    static void Main(string[] args)
    {
      new Program().Run();
    }

    static Dictionary<string, Func<int, int, bool>> ops = new Dictionary<string, Func<int, int, bool>>
    {
      { "<=", (a,b) => a <= b },
      { "<", (a,b) => a < b },
      { ">", (a,b) => a > b },
      { ">=", (a,b) => a >= b },
      { "==", (a,b) => a == b },
      { "!=", (a,b) => a != b },

    };
    Dictionary<string, int> registers = new Dictionary<string, int>();

    void Run()
    {
      

      foreach (var line in File.ReadAllLines("input.txt"))
      {
        var parsed = Regex.Match(line, "([a-z]+) (inc|dec) (-?\\d+) if ([a-z]+) ([^ ]+) (-?\\d+)");

        var test = registers.ContainsKey(parsed.Groups[4].Value) ? registers[parsed.Groups[4].Value] : 0;
        bool testResult = ops[parsed.Groups[5].Value](test, int.Parse(parsed.Groups[6].Value));

        if (testResult)
        {
          AddOrUpdate(parsed.Groups[1].Value, (parsed.Groups[2].Value == "inc" ? 1 : -1) * int.Parse(parsed.Groups[3].Value));
        }
      }

      var largest = registers.OrderByDescending(f => f.Value).First();
      Console.WriteLine($"The largest register is {largest.Key}, with value {largest.Value}");
    }

    void AddOrUpdate(string register, int added)
    {
      if (registers.TryGetValue(register, out int previous))
      {
        registers[register] = previous + added;
      } else
      {
        registers.Add(register, added);
      }
    }
  }
}
