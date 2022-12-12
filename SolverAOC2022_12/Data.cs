using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_12
{
  internal class Data
  {
    private string inputData;

    private Grid Grid;

    public Data(string inputData)
    {
      this.inputData = inputData;

      Grid = new Grid(inputData);
    }

    internal int Solve1()
    {
      Grid.FindDistances();

      return Grid.AllFields.Where(x => x.C == 'S').FirstOrDefault().Distance;
    }

    internal int Solve2()
    {
      Grid.FindDistances();

      return Grid.AllFields.Where(x => x.C == 'a').Min(x=>x.Distance);
    }
  }
}
