using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_15
{
  internal class Interval
  {

    public List<IntervalPoint> Points { get; set; } = new List<IntervalPoint>();

    public Interval(long start, long end)
    {
      Points.Add(new IntervalPoint(start, EIntervalPointType.Start));
      Points.Add(new IntervalPoint(end, EIntervalPointType.End));
    }

    internal long GetLength()
    {
      return Points[1].Value - Points[0].Value + 1;
    }

    public bool ContainsX(Point p)
    {
      if (Points[0].Value <= p.X && Points[1].Value >= p.X)
      {
        return true;
      }
      return false;
    }
  }
}
