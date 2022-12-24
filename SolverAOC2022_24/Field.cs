using System.Collections.Generic;

namespace SolverAOC2022_24
{
  public class Field
  {

    public int X { get; }
    public int Y { get; }

    public EFieldType Type { get; }

    public Dictionary<EDirection, Field> Neighbours { get; } = new Dictionary<EDirection, Field>();
    public Dictionary<EDirection, Field> LoopNeighbours { get; } = new Dictionary<EDirection, Field>();

    public List<Blizzard> Blizzards { get; } = new List<Blizzard>();

    public Field(int x, int y, EFieldType type)
    {
      X = x;
      Y = y;
      Type = type;
    }

    public override int GetHashCode()
    {
      return 65000 * X + Y;
    }

    public override bool Equals(object obj)
    {
      if(obj is Field f)
      {
        return f.X == X && f.Y == Y;
      }
      return false;
    }

    public override string ToString()
    {
      return $"[{X},{Y}]";
    }

    public char GetPrintValue()
    {
      if(Blizzards.Count > 1)
      {
        return Blizzards.Count.ToString()[0];
      } else if (Blizzards.Count == 1)
      {
        return Blizzards[0].GetPrintValue();
      }
      else if(Type == EFieldType.WALL)
      {
        return '#';
      } else if(Type == EFieldType.EMPTY)
      {
        return '.';
      } else
      {
        return '?';
      }
      
    }
  }
}