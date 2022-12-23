using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_23
{
  public class Field
  {
    
    public int X { get; }
    public int Y { get; }

    public Field(int x, int y)
    {
      X = x;
      Y = y;
    }

    public override bool Equals(object obj)
    {
      if(obj is Field f)
      {
        return f.X == X && f.Y == Y;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return X * 65536 + Y;
    }

    public Field GetField(EDirection dir)
    {
      switch (dir)
      {
        case EDirection.North:
          return new Field(X, Y - 1);
        case EDirection.NorthWest:
          return new Field(X - 1, Y - 1);
        case EDirection.West:
          return new Field(X - 1, Y);
        case EDirection.SouthWest:
          return new Field(X - 1, Y + 1);
        case EDirection.South:
          return new Field(X, Y + 1);
        case EDirection.SouthEast:
          return new Field(X + 1, Y + 1);
        case EDirection.East:
          return new Field(X + 1, Y);
        case EDirection.NorthEast:
          return new Field(X + 1, Y - 1);
        default:
          throw new Exception($"Invalid direction {dir}");
      }
    }

    public override string ToString()
    {
      return $"[{X},{Y}]";
    }
  }
}