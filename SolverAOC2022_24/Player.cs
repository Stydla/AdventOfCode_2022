using System;

namespace SolverAOC2022_24
{
  public class Player
  {

    public Field Position { get; set; }

    public Player(Field position)
    {
      Position = position;
    }

    public char GetPrintValue()
    {
      return 'E';
    }

    internal bool Move(EDirection dir)
    {
      Field target;
      if(!Position.Neighbours.TryGetValue(dir, out target))
      {
        return false;
      }
      if(target.Type == EFieldType.WALL ||
        target.Blizzards.Count > 0)
      {
        return false;
      }
      Position = target;
      return true;
    }

    internal void MoveBack(EDirection dir)
    {
      dir = dir.GetOpposite();
      Field target = Position.Neighbours[dir];
      if (target.Type == EFieldType.WALL)
      {
        throw new Exception("Cannot move back");
      }
      Position = target;
    }
  }
}