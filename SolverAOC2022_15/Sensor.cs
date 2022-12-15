using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2022_15
{
  internal class Sensor
  {

    public long Distance { get; set; }

    public Point Location { get; set; }
    public Point BeaconLocation { get; set; }

    public Sensor(string input)
    {

      Match m = Regex.Match(input, "Sensor at x=(-{0,1}\\d*), y=(-{0,1}\\d*): closest beacon is at x=(-{0,1}\\d*), y=(-{0,1}\\d*)");
      long x1 = long.Parse(m.Groups[1].Value);
      long y1 = long.Parse(m.Groups[2].Value);
      long x2 = long.Parse(m.Groups[3].Value);
      long y2 = long.Parse(m.Groups[4].Value);

      Location = new Point(x1, y1);
      BeaconLocation = new Point(x2, y2);

      Distance = Location.ManhatanDistance(BeaconLocation);


    }

    internal Interval GetInterval(long y)
    {
      long dist = Distance - Math.Abs(Location.Y - y);

      if(dist < 0)
      {
        return null;
      }

      return new Interval(Location.X - dist, Location.X + dist);
    }
  }
}
