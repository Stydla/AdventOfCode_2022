﻿using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_21
{
  public class Program : BaseAdventSolver, IAdventSolver
  {

    public override string SolverName => "Day 21: Monkey Math"/*TODO: Task Name*/;

    public override string InputsFolderName => "SolverAOC2022_21";

    public override string SolveTask1(string InputData)
    {
      Data d = new Data(InputData);
      long res = d.Solve1();
      return res.ToString();
    }

    public override string SolveTask2(string InputData)
    {
      Data d = new Data(InputData);
      long res = d.Solve2();
      return res.ToString();
    }
  }
}
