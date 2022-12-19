using System;
using System.IO;

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
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          Factory f = new Factory(line);
          f.Simulate1(24);
          res += f.MaxGeodes * f.ID;
        }
      }

      return res;

    }

    internal int Solve2()
    {
      int res = 1;
      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        for(int i = 0; i < 3; i++)
        {
          line = sr.ReadLine();
          Factory f = new Factory(line);
          f.Simulate1(32);
          res *= f.MaxGeodes;
        }
      }

      return res;
    }
  }
}