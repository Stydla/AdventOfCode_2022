using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_14
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
      Grid g = new Grid(inputData);
      g.Simulate1();
      return g.SandCount();
    }

    internal int Solve2()
    {
      Grid g = new Grid(inputData);
      g.Simulate2();
      return g.SandCount();
    }
  }
}
