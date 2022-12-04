using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_04
{
  internal class Item
  {
    public Range Range1 { get; set; }
    public Range Range2 { get; set; }

    public Item(string line)
    {
      string[] data = line.Split(',');
      Range1 = new Range(data[0]);
      Range2 = new Range(data[1]);

    }

    internal bool FullyOverlaps()
    {
      return Range1.Contains(Range2) || Range2.Contains(Range1);
    }


    internal bool PartiallyOverlaps()
    {
      return Range1.Overlaps(Range2);
    }
  }
}
