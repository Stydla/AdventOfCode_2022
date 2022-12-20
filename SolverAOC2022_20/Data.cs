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
        if (val < 0)
        {
          Node prev = current.Prev;
          Node prevPrev = current.Prev.Prev;
          Node next = current.Next;

          prevPrev.Next = current;

          current.Prev = prevPrev;
          current.Next = prev;

          prev.Prev = current;
          prev.Next = next;

          next.Prev = prev;

        }
        else
        {
          Node prev = current.Prev;
          Node next = current.Next;
          Node nextNext = current.Next.Next;

          prev.Next = next;

          next.Prev = prev;
          next.Next = current;

          current.Prev = next;
          current.Next = nextNext;

          nextNext.Prev = current;
        }
      }
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