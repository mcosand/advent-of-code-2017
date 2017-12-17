using System;

namespace day_17
{
  class Program
  {
    static void Main(string[] args)
    {
      var input = 329;

      var answer = 0;
      var cursor = 0;

      for (var i = 1; i < 50000000; i++)
      {
        cursor = (cursor + input) % i;
        if (cursor == 0) answer = i;
        cursor++;        
      }
      Console.WriteLine(answer);
    }
  }
}
