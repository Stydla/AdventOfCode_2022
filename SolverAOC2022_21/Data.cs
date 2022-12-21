using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SolverAOC2022_21
{
  internal class Data
  {
    private string inputData;

    public List<Node> Nodes { get; set; } = new List<Node>();

    public Node Root { get; set; }
    public Node Human { get; set; }

    public Data(string inputData)
    {
      this.inputData = inputData;

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          Match m = Regex.Match(line, "^(\\w*): (\\w*) (.) (\\w*)$");
          if(m.Success)
          {
            string nameParent = m.Groups[1].Value;
            string lChild = m.Groups[2].Value;
            string operation = m.Groups[3].Value;
            string rChild = m.Groups[4].Value;

            Node parent = GetOrCreateNode(nameParent);
            Node left = GetOrCreateNode(lChild);
            Node right = GetOrCreateNode(rChild);

            ENodeType type = Node.GetNodeType(operation);

            parent.AddChilds(left, right, type);

          }

          m = Regex.Match(line, "^(\\w*): (\\d*)$");
          if (m.Success)
          {
            string name = m.Groups[1].Value;
            long val = long.Parse(m.Groups[2].Value);

            Node n = GetOrCreateNode(name);
            n.Value = val;
            n.Type = ENodeType.Value;
          }
        }
      }

      Root = Nodes.First(x => x.Name == "root");
      Human = Nodes.First(x => x.Name == "humn");

    }

    private Node GetOrCreateNode(string name)
    {
      Node node = Nodes.Find(x => x.Name == name);
      if (node == null)
      {
        node = new Node(name);
        Nodes.Add(node);
      }
      return node;
    }

    internal long Solve1()
    {
      return Root.Solve();
    }

    internal long Solve2()
    {
      Human.Type = ENodeType.Value;
      Root.Type = ENodeType.Equal;

      Root.Solve();

      Human.SetUnknownPath();

      Root.SolveTopDown(0);

      long res = Root.Solve();

      return Human.Value;
    }
  }
}