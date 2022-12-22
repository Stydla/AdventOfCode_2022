using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_22
{
  public class PathStep
  {
    

    public int Count { get; set; }
    public ERotation Rotation { get; set; }

    public PathStep(ERotation rotation, int count)
    {
      Rotation = rotation;
      Count = count;
    }

  }
}
