using System;

namespace SolverAOC2022_24
{
  public class Blizzard
  {

    public Field Position { get; set; }

    public EDirection Direction { get; set; }

    public Blizzard(Field position, EDirection direction)
    {
      Position = position;
      Direction = direction;

      Position.Blizzards.Add(this);
    }

    public Blizzard(Blizzard source)
    {
      Position = new Field(source.Position);
      Direction = source.Direction;
    }

    public char GetPrintValue()
    {
      switch (Direction)
      {
        case EDirection.UP:
          return '^';
        case EDirection.RIGHT:
          return '>';
        case EDirection.DOWN:
          return 'v';
        case EDirection.LEFT:
          return '<';
        default:
          throw new System.Exception($"Invalid blizzard direction {Direction}");
      }
    }

    internal void Move()
    {
      Field target = Position.LoopNeighbours[Direction];
      Position.Blizzards.Remove(this);
      target.Blizzards.Add(this);
      Position = target;
    }

    internal void MoveBack()
    {
      EDirection dir = Direction.GetOpposite();
      Field target = Position.LoopNeighbours[dir];
      Position.Blizzards.Remove(this);
      target.Blizzards.Add(this);
      Position = target;
    }

  }
}