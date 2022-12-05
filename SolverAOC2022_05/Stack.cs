using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_05
{
  internal class Stack
  {
    public List<char> Items = new List<char>();

    public int Index;
    public Stack(int index)
    {
      Index = index;
    }
  }
}
