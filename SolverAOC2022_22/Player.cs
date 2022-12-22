using System;
using System.Diagnostics;

namespace SolverAOC2022_22
{
  public class Player
  {

    public Field Field { get; set; }

    public EDirection Direction { get; set; }

    public Player(Field startField) 
    {
      Field = startField;
      Direction = EDirection.RIGHT;
    }

    internal void Move(PathStep ps)
    {
      Direction = GetNewDirection(ps.Rotation);  
      for(int i = 0; i < ps.Count; i++)
      {
        Field nextField = Field.Neighbours[Direction];
        switch (nextField.Type)
        {
          case EFieldType.EMPTY:
            Field = nextField;
            break;
          case EFieldType.WALL:
            return;
            break;
        }
      }
    }

    private EDirection GetNewDirection(ERotation rotation)
    {
      switch (rotation)
      {
        case ERotation.NONE:
          return Direction;
        case ERotation.LEFT:
          return (EDirection)(((int)Direction - 1 + 4) % 4);
        case ERotation.RIGHT:
          return (EDirection)(((int)Direction + 1) % 4);
        default:
          throw new Exception($"Invalid rotation {rotation}");
      }
    }
  }
}