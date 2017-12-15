using System;

namespace day_15
{
  class Program
  {
    static void Main(string[] args)
    {
      int matches = 0;
      var genA = new Generator(512, 16807, 4);
      var genB = new Generator(191, 48271, 8);
      for (int i=0;i<5000000;i++)
      {
        if (i % 1000000 == 0) Console.WriteLine(i);
        if (genA.GetNext() == genB.GetNext()) matches++;
      }

    }

    class Generator
    {
      public long previous;
      public int factor;
      public int mask;

      public Generator(int start, int factor, int mask)
      {
        previous = start;
        this.factor = factor;
        this.mask = mask;
      }

      public int GetNext()
      {
        do
        {
          previous = (previous * factor) % int.MaxValue;
        } while ((previous % mask) != 0);


        //Console.WriteLine(previous);

        //Console.WriteLine(Convert.ToString((int)(previous & 0xffff), 2));
        return (int)(previous & 0xffff);
      }
    }
  }
}
