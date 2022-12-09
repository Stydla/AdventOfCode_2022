using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_09
{
  class Move
  {

    public Direction Direction { get; }
    public int Count { get; }

    public Move(string input)
    {
      switch(input[0])
      {
        case 'U':
          Direction = Direction.Up;
          break;
        case 'D':
          Direction = Direction.Down;
          break;
        case 'L':
          Direction = Direction.Left;
          break;
        case 'R':
          Direction = Direction.Right;
          break;
        default:
          throw new Exception("Direction not found");
      }
      Count = int.Parse(input.Substring(2));
    }

  }
}
