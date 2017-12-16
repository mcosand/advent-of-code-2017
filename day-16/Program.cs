using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace day_16
{
  class Program
  {
    static void Main(string[] args)
    {
      var steps = File.ReadAllText("input.txt").Split(',');
      int size = 16;

      var progs = new char[size];
      
      for (int i=0;i<size;i++)
      {
        progs[i] = (char)('a' + i);
      }

      foreach (var step in steps)
      {
        if (step[0] == 's')
        {
          for (int j = 0; j < int.Parse(step.Substring(1)); j++) {
            char temp = progs[progs.Length - 1];
            for (int i = progs.Length - 1; i > 0; i--)
            {
              progs[i] = progs[i - 1];
            }
            progs[0] = temp;
          }
        } else if (step[0] == 'x')
        {
          var match = Regex.Match(step, "x(\\d+)/(\\d+)");
          char temp = progs[int.Parse(match.Groups[1].Value)];
          progs[int.Parse(match.Groups[1].Value)] = progs[int.Parse(match.Groups[2].Value)];
          progs[int.Parse(match.Groups[2].Value)] = temp;
        }
        else if (step[0] == 'p')
        {
          int left = 0;
          int right = 0;
          for (int i=0;i<progs.Length;i++)
          {
            if (progs[i] == step[1]) { left = i; }
            if (progs[i] == step[3]) { right = i;}
          }

          char temp = progs[left];
          progs[left] = progs[right];
          progs[right] = temp;
        }
      }

      string answer = string.Join("", progs);
    }
  }
}
