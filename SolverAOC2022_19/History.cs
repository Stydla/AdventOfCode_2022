using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SolverAOC2022_19
{
  internal class History
  {

    private List<Robot> Robots;
    private Materials Materials;
    private int Day;

    public History(List<Robot> robots, Materials material, int day)
    {
      Robots = robots;
      Materials = material;
      Day = day;
    }

    internal string GenerateHash()
    {
      Robots.Sort((x, y) => x.Type - y.Type);

      return $"{Day},{string.Join("_", Robots)},{Materials}";
    }
  }
}