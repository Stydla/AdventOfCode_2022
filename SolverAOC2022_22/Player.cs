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
        FieldDirection fd = Field.NeighbourDirections[Direction];
        Field nextField = fd.Field;
        switch (nextField.Type)
        {
          case EFieldType.EMPTY:
            Field = nextField;
            Direction = GetNewDirection(fd.Rotation);
            break;
          case EFieldType.WALL:
            return;
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
        case ERotation.OPOSITE:
          return (EDirection)(((int)Direction + 2) % 4);
        default:
          throw new Exception($"Invalid rotation {rotation}");
      }
    }
  }
}