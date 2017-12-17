using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_17
{
  class Program
  {
    static void Main(string[] args)
    {
      var input = 329;
      //var input = 3;

      int answer = Try2(input);
      Console.WriteLine(answer);
    }

    private static int Try1(int input)
    {
      var buffer = new Node { Value = 0 };
      buffer.Next = buffer;
      var zero = buffer;

      for (int i = 1; i < 50000000; i++)
      {
        if (i % 1000000 == 0) Console.WriteLine(i);

        for (int j = 0; j < input; j++) { buffer = buffer.Next; }

        var node = new Node { Value = i, Next = buffer.Next };
        buffer.Next = node;
        buffer = buffer.Next;
      }

      var answer = zero.Next.Value;
      return answer;
    }

    static int Try2(int input) {
      List<int> buffer = new List<int> { 0 };
      int zeroIndex = 0;
      int cursor = 0;

      for (var i = 1; i < 50000000; i++)
      {
        if (i % 1000000 == 0) Console.WriteLine(i);
        cursor = (cursor + input) % buffer.Count;
        buffer.Insert(cursor++, i);
        if (zeroIndex >= cursor) zeroIndex++;
      }
      return buffer[(zeroIndex + 1) % buffer.Count];
    }
  }

  class Node
  {
    public Node Next { get; set; }
    public int Value { get; set; }
  }
}
