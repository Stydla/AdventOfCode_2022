using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_15
{
  internal class Point
  {
    public long X { get; set; }
    public long Y { get; set; }

    public Point(long x, long y)
    {
      X = x;
      Y = y;
    }

    public long ManhatanDistance(Point p)
    {
      return Math.Abs(p.X - X) + Math.Abs(p.Y - Y);
    }


  }
}
