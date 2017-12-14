using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_14
{
  class Program
  {
    static void Main(string[] args)
    {
      //string input = "flqrgnkx";
      string input = "ljoxqyyw";

      int used = 0;
      for (int i=0;i<128;i++)
      {
        int[] hash = knotHash(input + "-" + i);
        for (int j=0;j<hash.Length;j++)
        {
          for (int k = 0; k<8;k++)
          {
            if ((hash[j] & 0x01) == 0x01) used++;
            hash[j] = hash[j] >> 1;
          }
        }
      }

    }

    private static int[] knotHash(string input)
    {

      //int[] lengths = input.Split(',').Select(f => int.Parse(f.Trim())).ToArray();
      int[] lengths = input.Trim().ToCharArray().Select(f => (int)f).Concat(new int[] { 17, 31, 73, 47, 23 }).ToArray();
      int size = 256;

      int[] nodes = new int[size];
      int current = 0;
      int skip = 0;

      for (int i = 0; i < size; i++) { nodes[i] = i; }

      for (int r = 0; r < 64; r++)
      {
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
      }


      int[] hashBytes = new int[16];
      for (int i = 0; i < 16; i++)
      {
        for (int j = 0; j < 16; j++)
        {
          hashBytes[i] = hashBytes[i] ^ nodes[i * 16 + j];
        }
      }
      //Console.WriteLine(nodes[0] * nodes[1]);
      return hashBytes;
    }
  }
}
