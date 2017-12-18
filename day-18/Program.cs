using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_18
{
  class Program
  {
    Match[] list = File.ReadAllLines("input.txt").Select(f => f.Trim()).Where(f => !string.IsNullOrWhiteSpace(f)).Select(f => Regex.Match(f, "^([a-z]{3}) ([^ ]+) ?([^ ]*)$")).ToArray();
    long pc = 0;
    Dictionary<string, long> registers = new Dictionary<string, long>();
    long freq = 0;

    static void Main(string[] args)
    {
      new Program().Run();
    }

    void Run()
    {
      bool log = true;

      while (pc >= 0 && pc < list.Length)
      {
        var inst = list[pc++].Groups;
        if (log) Console.Write(inst[0].Value);
        switch (inst[1].Value)
        {
          case "set":
            Write(inst[2].Value, Read(inst[3].Value));
            break;
          case "snd":
            freq = Read(inst[2].Value);
            break;
          case "add":
            Write(inst[2].Value, Read(inst[2].Value) + Read(inst[3].Value));
            break;
          case "mul":
            Write(inst[2].Value, Read(inst[2].Value) * Read(inst[3].Value));
            break;
          case "mod":
            Write(inst[2].Value, Read(inst[2].Value) % Read(inst[3].Value));
            break;
          case "rcv":
            if (Read(inst[2].Value) != 0)
            {
              Console.WriteLine("Recovered " + freq);
              break;
              // recover frequency
            }
            break;
          case "jgz":
            if (Read(inst[2].Value) > 0)
            {
              pc += Read(inst[3].Value) - 1;
              if (log) Console.Write($" jumped to {pc}");
            }
            break;
          default:
            throw new NotImplementedException();
        }
        if (log) Console.WriteLine(" " + string.Join(" ", registers.Select(f => $"{f.Key}={f.Value}")));
      }
    }

    long Read(string r)
    {
      if (!long.TryParse(r, out long retVal))
      {
        if (!registers.TryGetValue(r, out retVal))
        {
          registers.Add(r, 0);
          retVal = 0;
        }
      }
      
      return retVal;
    }

    void Write(string r, long value)
    {
      if (registers.ContainsKey(r))
      {
        registers[r] = value;
      } else
      {
        registers.Add(r, value);
      }
    }
  }
}
