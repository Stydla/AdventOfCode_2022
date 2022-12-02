using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_01
{
  public class Data
  {

    private List<List<int>> items = new List<List<int>>();
    public Data(string input)
    {
      List<List<int>> data;
      using (StringReader sr = new StringReader(input))
      {
        string line;
        List<int> currentList = new List<int>();
        items.Add(currentList);
        while ((line = sr.ReadLine()) != null)
        {
          if(string.IsNullOrEmpty(line))
          {
            currentList = new List<int>();
            items.Add(currentList);
          } else
          {
            currentList.Add(int.Parse(line));
          }
        }
      }
    }

    internal int Solve1()
    {

      return items.Select(x => x.Sum()).Max();

    }

    internal int Solve2()
    {

      return items.Select(x => x.Sum()).OrderBy(x=>-x).Take(3).Sum();

    }
  }
}
