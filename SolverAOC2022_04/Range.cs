using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_04
{
  internal class Range
  {

    public int From { get; set; }
    public int To { get; set; }

    public Range(string v)
    {
      string[] data = v.Split('-');
      From = int.Parse(data[0]);
      To = int.Parse(data[1]);
    }

    public bool Contains(Range r)
    {
      if(From <= r.From && To >= r.To) return true;
      return false;
    }

    internal bool Overlaps(Range range2)
    {
      if (From <= range2.To && To >= range2.From) return true;
      if (To >= range2.From && From <= range2.To) return true;
      return false;
    }
  }
}
