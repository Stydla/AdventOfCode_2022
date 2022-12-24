using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_24
{
  public enum EDirection
  {
    DOWN = 0,
    RIGHT = 1,
    LEFT = 2,
    UP = 3,
    NONE = 100
  }

  public static class Extensions
  {
    public static EDirection GetOpposite(this EDirection dir)
    {
      switch (dir)
      {
        case EDirection.UP:
          return EDirection.DOWN;
        case EDirection.RIGHT:
          return EDirection.LEFT;
        case EDirection.DOWN:
          return EDirection.UP;
        case EDirection.LEFT:
          return EDirection.RIGHT;
        case EDirection.NONE:
          return EDirection.NONE;
        default:
          throw new System.Exception($"Invalid direction {dir}");
      }
    }
  }
}
