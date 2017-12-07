using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_06
{
  class Program
  {
    static void Main(string[] args)
    {
      var banks = "10	3	15	10	5	15	5	15	9	2	5	8	5	2	3	6".Split('\t').Select(f => int.Parse(f)).ToArray();
      var patterns = new SortedSet<string>();
      
      while (true)
      {
        string key = string.Join(" ", banks);
        //Console.WriteLine(string.Join(" ", banks.Select(f => $"{f:000}")));
        if (patterns.Contains(key))
        {
          Console.WriteLine(patterns.Count);
          return;
        }
        patterns.Add(key);

        int maxIndex = banks.Length - 1;
        int maxValue = 0;
        for (var i = 0; i < banks.Length; i++)
        {
          if (banks[i] > maxValue)
          {
            maxValue = banks[i];
            maxIndex = i;
          }
        }

        // could optimize to divide blocks evenly and add them to a bank all at once
        banks[maxIndex] = 0;
        for (var i = 0; i < maxValue; i++)
        {
          banks[(maxIndex + 1 + i) % banks.Length]++;
        }
      }
    }
  }
}
