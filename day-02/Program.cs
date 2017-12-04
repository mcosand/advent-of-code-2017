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
      string[] sample = { "5 1 9 5",
"7 5 3",
"2 4 6 8" };

      string[] input = File.ReadLines("input.txt").ToArray();

      int sum = 0;
      for (int i = 0; i < input.Length; i++)
      {
        int[] fields = input[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(f => int.Parse(f)).ToArray();
        sum += fields.Max() - fields.Min();
      }
      Console.WriteLine("The answer is " + sum);
    }
  }
}
