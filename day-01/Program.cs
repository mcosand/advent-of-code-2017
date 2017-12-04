using System;
using System.IO;
using System.Reflection;

namespace day_01
{
  class Program
  {
    static void Main(string[] args)
    {
      string input = File.ReadAllText("input.txt").Trim();
      //string input = "9121219";
      int sum = 0;
      var firstColor = Console.ForegroundColor;
      for (int i=0; i<input.Length; i++)
      {
        if (input[i] == input[(i + input.Length / 2) % input.Length])
        {
          int add = (byte)input[i] - (byte)'0';
          Console.ForegroundColor = ConsoleColor.Cyan;
          Console.WriteLine("{0:0000} {1} {2}", i, input[i], add);
          Console.ForegroundColor = firstColor;
          sum += add;
        } else {
          Console.WriteLine("{0:0000} {1}", i, input[i]);
        }
      }
      Console.WriteLine("The answer is " + sum);
    }
  }
}
