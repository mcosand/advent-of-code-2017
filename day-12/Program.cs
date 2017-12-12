using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day_12
{
  class Program
  {
    static void Main(string[] args)
    {
      Dictionary<int, int[]> nodes = new Dictionary<int, int[]>();
      foreach (var line in File.ReadAllLines("input.txt").Where(f => !string.IsNullOrWhiteSpace(f)))
      {
        int[] parts = line.Trim().Replace(" <->", ",").Replace(" ", "").Split(',').Select(f => int.Parse(f)).ToArray();
        nodes.Add(parts[0], parts.Skip(1).ToArray());
      }

      Dictionary<int, bool> inSet = new Dictionary<int, bool>();

      FillSet(nodes, inSet, 0);
      Console.WriteLine(inSet.Count);
    }

    static void FillSet(Dictionary<int, int[]> nodes, Dictionary<int, bool> inSet, int node)
    {
      if (inSet.ContainsKey(node)) return;

      inSet.Add(node, true);

      foreach (var child in nodes[node])
      {
        FillSet(nodes, inSet, child);
      }
    }
  }
}
