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

      var buffer = new Node { Value = 0 };
      buffer.Next = buffer;

      for (int i = 1; i < 2018; i++)
      {
        for (int j = 0; j < input; j++) { buffer = buffer.Next; }

        var node = new Node { Value = i, Next = buffer.Next };
        buffer.Next = node;
        buffer = buffer.Next;
      }
      var answer = buffer.Next.Value;
    }
  }

  class Node
  {
    public Node Next { get; set; }
    public int Value { get; set; }
  }
}
