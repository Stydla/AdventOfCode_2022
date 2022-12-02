using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_02
{
  class Data
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

    public int Solve1()
    {
      int res = 0;
      foreach(string item in items)
      {
        res += scoreList[item];
      }
      return res;
    }

    private Dictionary<string, int> scoreList = new Dictionary<string, int>()
    {
      { "A X", 4 },
      { "A Y", 8 },
      { "A Z", 3 },
      { "B X", 1 },
      { "B Y", 5 },
      { "B Z", 9 },
      { "C X", 7 },
      { "C Y", 2 },
      { "C Z", 6 },
    };

    private Dictionary<string, int> scoreList2 = new Dictionary<string, int>()
    {
      { "A X", 0 + 3},
      { "A Y", 3 + 1},
      { "A Z", 6 + 2},
      { "B X", 0 + 1},
      { "B Y", 3 + 2},
      { "B Z", 6 + 3},
      { "C X", 0 + 2},
      { "C Y", 3 + 3},
      { "C Z", 6 + 1},
    };

    public int Solve2()
    {
      int res = 0;
      foreach (string item in items)
      {
        res += scoreList2[item];
      }
      return res;
    }

  }
}
