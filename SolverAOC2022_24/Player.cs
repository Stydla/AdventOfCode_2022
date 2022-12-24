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

  }
}