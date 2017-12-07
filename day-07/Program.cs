using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

      Console.WriteLine(root.Name);
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
    public Node Parent { get; set; }
    public List<Node> Children { get; set; } = new List<Node>();
  }
}
