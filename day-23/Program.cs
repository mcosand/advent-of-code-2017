using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_23
{
  class Program
  {
    Match[] list = File.ReadAllLines("input.txt").Select(f => f.Trim()).Where(f => !string.IsNullOrWhiteSpace(f)).Select(f => Regex.Match(f, "^([a-z]{3}) ([^ ]+) ?([^ ]*)$")).ToArray();
    long pc = 0;
    Dictionary<string, long> registers = new Dictionary<string, long>();

    static void Main(string[] args)
    {
      var p = new Program();
      p.Run();
      Console.WriteLine(p.registers["h"]);
    }

    static int isPrime(long number)
    {
      if (number == 1) return 0;
      if (number == 2) return 1;

      for (int i = 2; i <= Math.Ceiling(Math.Sqrt(number)); ++i)
      {
        if (number % i == 0) return 0;
      }

      return 1;

    }


    static int mulTimes = 0;

    public Program()
    {
      registers.Add("a", 1);
    }

    void Run()
    {
      while (true)
      {
        if (pc < 0 || pc >= list.Length)
        {
          return;
        }

        if (list[pc].Groups[0].Value == "set d 2")
        {
          Console.WriteLine("!! short circuit.");
          Write("f", isPrime(Read("b")));
          pc = 24;
        }

        var inst = list[pc++].Groups;
        Console.Write(pc + " " + inst[0].Value);


        switch (inst[1].Value)
        {
          case "set":
            Write(inst[2].Value, Read(inst[3].Value));
            break;
          case "sub":
            Write(inst[2].Value, Read(inst[2].Value) - Read(inst[3].Value));
            break;
          case "mul":
            Write(inst[2].Value, Read(inst[2].Value) * Read(inst[3].Value));
            mulTimes++;
            break;
          case "jnz":
            if (Read(inst[2].Value) != 0)
            {
              pc += Read(inst[3].Value) - 1;
              Console.Write($" jumped to {pc}");
            }
            break;
          default:
            throw new NotImplementedException();
        }
        Console.WriteLine(" " + string.Join(" ", registers.Select(f => $"{f.Key}={f.Value}")));
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
      }
      else
      {
        registers.Add(r, value);
      }
    }
  }
}
