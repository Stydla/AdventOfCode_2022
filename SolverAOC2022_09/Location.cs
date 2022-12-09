using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_09
{
  class Location
  {
    

    public int X { get; set; }
    public int Y { get; set; }

    public Location(int x, int y)
    {
      X = x;
      Y = y;
    }

    public Location(Location tail)
    {
      X = tail.X;
      Y = tail.Y;
    }
  }
}
