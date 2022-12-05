using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_05
{
  public class Program : BaseAdventSolver, IAdventSolver
  {

    public override string SolverName => "Day 5: Supply Stacks"/*TODO: Task Name*/;

    public override string InputsFolderName => "SolverAOC2022_05";

    public override string SolveTask1(string InputData)
    {
      Data d = new Data(InputData);
      string res = d.Solve1();
      return res;
    }

    public override string SolveTask2(string InputData)
    {
      Data d = new Data(InputData);
      string res = d.Solve2();
      return res;
    }
  }
}
