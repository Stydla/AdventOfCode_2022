using System;
using System.Security.Cryptography.X509Certificates;

namespace SolverAOC2022_17
{
  internal class Data
  {
    private string inputData;
   

    public Data(string inputData)
    {
      this.inputData = inputData;
      
    }

    internal long Solve1()
    {

      Chamber c = new Chamber(inputData);

      c.Simulate(2022);

      return c.Height;
    }


    internal long Solve2()
    {
      Chamber c = new Chamber(inputData);

      long computed = c.Simulate2(1000000000000);

      return c.Height + computed;
    }
  }
}