using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_02
{
  class Program
  {
    static void Main(string[] args)
    {
      string[] sample = { "5 9 2 8",
"9 4 7 3",
"3 8 6 5"
 };

      string[] input = File.ReadLines("input.txt").ToArray();
      //string[] input = sample;

      int sum = 0;
      for (int i = 0; i < input.Length; i++)
      {
        int[] fields = input[i].Split(new char[] { '\t',' ' }, StringSplitOptions.RemoveEmptyEntries).Select(f => int.Parse(f)).ToArray();
        var divisor = (from a in fields join b in fields on 1 equals 1 where a > b && (double)(a / b) == (double)a / (double)b select (int)a/b).First(); // where a / b == (double)a / (double)b select a / b).First();
        Console.WriteLine(divisor);
        sum += divisor;
      }
      Console.WriteLine("The answer is " + sum);
    }
  }
}
