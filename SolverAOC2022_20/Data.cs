using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SolverAOC2022_20
{
  internal class Data
  {
    private string inputData;

    public List<Node> Nodes = new List<Node>();
    public List<Node> Order;

    public Data(string inputData)
    {
      this.inputData = inputData;

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          Nodes.Add(new Node(long.Parse(line)));
        }
      }

      for (int i = 0; i < Nodes.Count; i++)
      {
        Nodes[i].Next = Nodes[(i + 1) % Nodes.Count];
        Nodes[i].Prev = Nodes[(i - 1 + Nodes.Count) % Nodes.Count];
      }

      Order = new List<Node>(Nodes);
    }

    internal long Solve1()
    {
      foreach (Node n in Order)
      {
        Swap(n);
      }

      long sum = 0;

      Node tmp = Nodes.First(x => x.Value == 0);
       
      for(int i = 1; i <= 3000; i++)
      {
        tmp = tmp.Next;
        if(i != 0 && i % 1000 == 0)
        {
          sum += tmp.Value;
        }
      }

      return sum;
    }

    private void Swap(Node n)
    {
      Node current = n;
      long val = n.Value % (Nodes.Count - 1);
      for (int j = 0; j < Math.Abs(val); j++)
      {
        if(val < 0)
        {
          current = current.Prev;
        } else
        {
          current = current.Next;
        }
      }
      if(val < 0)
      {
        MoveNodeBefore(n, current);
      } 
      else
      {
        MoveNodeBefore(n, current.Next);
      }
      
    }

    private void MoveNodeBefore(Node movedNode, Node targetNode)
    {
      Node movedPrev = movedNode.Prev;
      Node movedNext = movedNode.Next;

      movedPrev.Next = movedNext;
      movedNext.Prev = movedPrev;

      Node targetPrev = targetNode.Prev;

      targetPrev.Next = movedNode;
      movedNode.Prev = targetPrev;
      movedNode.Next = targetNode;
      targetNode.Prev = movedNode;
    }


    private void Print(Node start, int cnt)
    {
      List<long> res = new List<long>();
      Node current = start;
      for(int i  = 0; i < cnt; i++)
      {
        res.Add(current.Value);
        current = current.Next;
      }
      Console.WriteLine(string.Join(", ", res));

    }

    internal long Solve2()
    {
      foreach(Node n in Nodes)
      {
        n.Value = n.Value * 811589153;
      }

      for(int i = 0; i < 10; i++)
      {
        foreach (Node n in Order)
        {
          Swap(n);
        }
      }
      
      long sum = 0;

      Node tmp = Nodes.First(x => x.Value == 0);

      for (int i = 1; i <= 3000; i++)
      {
        tmp = tmp.Next;
        if (i != 0 && i % 1000 == 0)
        {
          sum += tmp.Value;
        }
      }

      return sum;

    }
  }
}