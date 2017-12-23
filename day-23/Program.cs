using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_23
{
  class Program
  {
    Match[] list;
    int id;
    long pc = 0;
    Dictionary<string, long> registers = new Dictionary<string, long>();
    public long sent = 0;
    bool log = true;

    Queue<long> queue = new Queue<long>();
    Queue<long> sendQueue;
    Status status = Status.SendNext;


    static void Main(string[] args)
    {
      var list = File.ReadAllLines("input.txt").Select(f => f.Trim()).Where(f => !string.IsNullOrWhiteSpace(f)).Select(f => Regex.Match(f, "^([a-z]{3}) ([^ ]+) ?([^ ]*)$")).ToArray();
      var zero = new Program(list, 0);
      //var one = new Program(list, 1) { sendQueue = zero.queue };
      //zero.sendQueue = one.queue;

      do
      {
        if (zero.status != Status.Terminate) zero.Run();
        //if (one.status != Status.Terminate) one.Run();

      } while (zero.status == Status.SendNext/* || one.status == Status.SendNext*/);
      //var answer = one.sent;
     // Console.WriteLine(answer);
    }

    static int mulTimes = 0;

    public Program(Match[] list, int id)
    {
      this.id = id;
      this.list = list;
      registers.Add("p", id);
    }

    void Run()
    {
      if (pc < 0 || pc >= list.Length)
      {
        status = Status.Terminate;
        return;
      }

      var inst = list[pc++].Groups;
      if (log) Console.Write(id + " " + inst[0].Value);
      status = Status.SendNext;

      switch (inst[1].Value)
      {
        case "set":
          Write(inst[2].Value, Read(inst[3].Value));
          break;
        //case "snd":
        //  sendQueue.Enqueue(Read(inst[2].Value));
        //  sent++;
        //  break;
        //case "add":
        //  Write(inst[2].Value, Read(inst[2].Value) + Read(inst[3].Value));
        //  break;
        case "sub":
          Write(inst[2].Value, Read(inst[2].Value) - Read(inst[3].Value));
          break;
        case "mul":
          Write(inst[2].Value, Read(inst[2].Value) * Read(inst[3].Value));
          mulTimes++;
          break;
        //case "mod":
        //  Write(inst[2].Value, Read(inst[2].Value) % Read(inst[3].Value));
        //  break;
        //case "rcv":
        //  if (queue.Count > 0)
        //  {
        //    Write(inst[2].Value, queue.Dequeue());
        //  }
        //  else
        //  {
        //    pc--;
        //    status = Status.Waiting;
        //  }
        //  break;
        //case "jgz":
        //  if (Read(inst[2].Value) > 0)
        //  {
        //    pc += Read(inst[3].Value) - 1;
        //    if (log) Console.Write($" jumped to {pc}");
        //  }
        //  break;
        case "jnz":
          if (Read(inst[2].Value) != 0)
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

  enum Status
  {
    SendNext,
    Terminate,
    Waiting
  }
}
