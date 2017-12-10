using System;
using System.Linq;

namespace day_10
{
  class Program
  {
    static void Main(string[] args)
    {
      string input = "120,93,0,90,5,80,129,74,1,165,204,255,254,2,50,113";
      //string input = "3, 4, 1, 5";

      int[] lengths = input.Split(',').Select(f => int.Parse(f.Trim())).ToArray();
      int size = 256;

      int[] nodes = new int[size];
      int current = 0;
      int skip = 0;

      for (int i = 0; i < size; i++) { nodes[i] = i; }

      for (int i = 0; i < lengths.Length; i++)
      {
        int thisLength = lengths[i];
        //Console.WriteLine(thisLength);
        for (int l = 0; l < thisLength / 2; l++)
        {
          int left = (current + l) % size;
          int right = (current + thisLength - l - 1) % size;
          //Console.WriteLine($"swap index {left} - {right}");
          int swap = nodes[left];
          nodes[left] = nodes[right];
          nodes[right] = swap;
        }
        current = (current + skip + thisLength) % size;
        skip++;
        //Console.WriteLine($"=======  Current: {current}     Skip: {skip}");

        //Console.WriteLine(string.Join(" ", nodes.Select((f, idx) => string.Format(idx == current ? "[{0}]" : "{0}", f))));
      }
      Console.WriteLine(nodes[0] * nodes[1]);
    }
  }
}
