using System;
using System.Linq;

namespace SolverAOC2022_06
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
      return Find(4);
    }

    private int Find(int charCnt)
    {
      for (int i = 0; i < inputData.Length - charCnt; i++)
      {
        string substr = inputData.Substring(i, charCnt);
        if (substr.GroupBy(x => x).Count() == charCnt)
        {
          return i + charCnt;
        }
      }
      return -1;
    }

    internal int Solve2()
    {
      return Find(14);
    }
  }
}