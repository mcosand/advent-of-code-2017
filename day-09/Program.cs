using System;
using System.IO;

namespace day_09
{
  class Program
  {
    static void Main(string[] args)
    {
      string input = File.ReadAllText("input.txt");
      //string input = "{{<ab>},{<ab>},{<ab>},{<ab>}}";

      bool garbage = false;
      int group = 0;
      int sum = 0;
      int garbageCount = 0;

      for (var i = 0; i< input.Length; i++)
      {
        if (input[i] == '!')
        {
          i++;
        }
        else if (garbage)
        {
          if (input[i] == '>') garbage = false;
          else
          {
            garbageCount++;
          }
        }
        else if (input[i] == '<')
        {
          garbage = true;
        }
        else if (input[i] == '{')
        {
          group++;
        }
        else if (input[i] == '}')
        {
          sum += group--;
        }
      }
      Console.WriteLine(garbageCount);
    }
  }
}
