using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolverAOC2022_03
{
  internal class Data
  {
    private List<string> items = new List<string>();

    public Data(string input)
    {
      using (StringReader sr = new StringReader(input))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          items.Add(line);
        }
      }
    }

    internal int Solve1()
    {
      int val = 0;
      foreach(string item in items)
      {
        string first = item.Substring(0, item.Length / 2);
        string second = item.Substring(item.Length / 2);
        foreach(char c in first)
        {
          if (second.Contains(c))
          {
            if(c >= 'a' && c<= 'z')
            {
              val += c - 'a' + 1;
            }
            if (c >= 'A' && c <= 'Z')
            {
              val += c - 'A' + 27;
            }
            break;
          }
        }

      }
      return val;
    }

    internal int Solve2()
    {
      int val = 0;
      for(int i = 0; i < items.Count; i+=3)
      {
        foreach (char c in items[i])
        {
          if (items[i + 1].Contains(c) && items[i + 2].Contains(c))
          {
            if (c >= 'a' && c <= 'z')
            {
              val += c - 'a' + 1;
            }
            if (c >= 'A' && c <= 'Z')
            {
              val += c - 'A' + 27;
            }
            break;
          }
        }
        
      }
      return val;
    }
  }
}