using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_20
{
  internal class Node
  {

    public Node Next { get; set; }
    public Node Prev { get; set; }

    public long Value { get; set; }

    public Node(long val)
    {
      Value = val;
    }

    public override string ToString()
    {
      return Value.ToString();
    }

  }
}
