using System;

namespace SolverAOC2022_24
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
      Map m = new Map(inputData);

      return m.Simulate();

    }

    internal int Solve2()
    {
      throw new NotImplementedException();
    }
  }
}