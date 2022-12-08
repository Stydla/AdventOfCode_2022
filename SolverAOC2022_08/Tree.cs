using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_08
{
  internal class Tree
  {

    public bool IsVisible { get; set; }
    public int Size { get; set; }
    public int TreeScore { get; internal set; }

    public Tree(int size)
    {
      Size = size;
      IsVisible = false;
    }

    

  }
}
