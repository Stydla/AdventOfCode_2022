using System;

namespace SolverAOC2022_22
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

      m.Solve1();

      Field f = m.Player.Field;

      return f.Row * 1000 + f.Column * 4 + (int)m.Player.Direction;
    }

    internal int Solve2()
    {
      Map m = new Map(inputData);

      m.Solve2();

      Field f = m.Player.Field;

      return f.Row * 1000 + f.Column * 4 + (int)m.Player.Direction;
    }
  }
}