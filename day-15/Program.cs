using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_15
{
  class Program
  {
    static void Main(string[] args)
    {
      int matches = 0;
      var genA = new Generator(512, 16807);
      var genB = new Generator(191, 48271);

      for (int i=0;i<40000000;i++)
      {
        if (i % 1000000 == 0) Console.WriteLine(i);
        if (genA.GetNext() == genB.GetNext()) matches++;
      }

    }

    class Generator
    {
      public long previous;
      public int factor;
      public Generator(int start, int factor)
      {
        previous = start;
        this.factor = factor;
      }

      public int GetNext()
      {
        previous = (previous * factor) % int.MaxValue;
        //Console.WriteLine(previous);

        //Console.WriteLine(Convert.ToString((int)(previous & 0xffff), 2));
        return (int)(previous & 0xffff);
      }
    }
  }
}
