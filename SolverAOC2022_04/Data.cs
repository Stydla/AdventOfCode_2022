using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_04
{
  internal class Data
  {

    public List<Item> Items { get; set; } = new List<Item>();

    public Data(string inputData)
    {
      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          Items.Add(new Item(line));
        }
      }
    }

    internal int Solve1()
    {
      int result = 0;
      foreach(Item i in Items)
      {
        if(i.FullyOverlaps())
        {
          result++;
        }
      }
      return result;
    }

    internal int Solve2()
    {
      int result = 0;
      foreach (Item i in Items)
      {
        if (i.PartiallyOverlaps())
        {
          result++;
        }
      }
      return result;
    }
  }
}
