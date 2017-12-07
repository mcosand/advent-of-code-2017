using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_07
{
  class Program
  {
    static void Main(string[] args)
    {
      new Program().Run();
    }

    Dictionary<string, Node> nodes = new Dictionary<string, Node>();

    public void Run()
    {
      foreach (var line in File.ReadAllLines("input.txt"))
      {
        var parsed = Regex.Match(line, "([a-z]+) \\((\\d+)\\)( -> ([a-z, ]+))?");

        var node = GetOrCreate(parsed.Groups[1].Value);
        node.Weight = int.Parse(parsed.Groups[2].Value);
        if (parsed.Groups[4].Success)
        {
          foreach (var childName in parsed.Groups[4].Value.Split(',').Select(f => f.Trim()))
          {
            var child = GetOrCreate(childName);
            child.Parent = node;
            node.Children.Add(child);
          }
        }
      }

      var root = nodes.Values.First();
      while (root.Parent != null)
      {
        root = root.Parent;
      }


      while (root != null)
      {
        int commonWeight = root.Children.GroupBy(f => f.WeightWithChildren).Where(f => f.Count() != 1).Select(f => f.Key).First();
        var badNode = root.Children.GroupBy(f => f.WeightWithChildren).Where(f => f.Count() == 1).Select(f => f.First()).FirstOrDefault();

        if (badNode.Children.Count == 0 || badNode.Children.All(f => f.WeightWithChildren == badNode.Children[0].WeightWithChildren))
        {
          Console.WriteLine($"Bad node is {badNode.Name}. Weight {badNode.Weight} should be {commonWeight - badNode.WeightWithChildren + badNode.Weight}");
          break;
        }
        root = badNode;
      }
    }


    Node GetOrCreate(string name)
    {
      if (!nodes.TryGetValue(name, out Node node))
      {
        node = new Node { Name = name };
        nodes.Add(name, node);
      }
      return node;
    }
  }

  class Node
  {
    public string Name { get; set; }
    public int Weight { get; set; }
    public int WeightWithChildren { get { return Weight + Children.Sum(f => f.WeightWithChildren); } }
    public Node Parent { get; set; }
    public List<Node> Children { get; set; } = new List<Node>();
  }
}
