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
  }
}