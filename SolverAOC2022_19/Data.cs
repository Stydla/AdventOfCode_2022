using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SolverAOC2022_19
{
  internal class Data
  {
    private string inputData;

    public Data(string inputData)
    {
      this.inputData = inputData;
    }

    internal int Solve1()
    {
      int res = 0;
      using (StringReader sr = new StringReader(inputData))
      {
        List<string> lines = new List<string>();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          lines.Add(line);
        }

        Parallel.For(0, lines.Count, (i) =>
        {
          string l = lines[i];
          Factory f = new Factory(l);
          f.Simulate1(24);
          res += f.MaxGeodes * f.ID;
        });
          
      }

      return res;

    }

    internal int Solve2()
    {
      int res = 1;
      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        Parallel.For(0, 3, (i) =>
        {
          line = sr.ReadLine();
          Factory f = new Factory(line);
          f.Simulate1(32);
          res *= f.MaxGeodes;
        });
        //for(int i = 0; i < 3; i++)
        //{
          
        //}
      }

      return res;
    }
  }
}