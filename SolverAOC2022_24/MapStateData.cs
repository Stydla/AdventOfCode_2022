namespace SolverAOC2022_24
{

  public class MapStateData
  {
    public int Round { get; set; }
    public bool IsEnd { get; set; }
    public Field Position { get; set; }
    public int TotalRound { get; set; }

    public MapStateData(int round, bool isEnd, Field position, int totalRound)
    {
      Round = round;
      IsEnd = isEnd;
      Position = new Field(position);
      TotalRound = totalRound;
    }

    public override string ToString()
    {
      return $"Round: {Round}, Is end: {IsEnd} - {Position}";
    }

    public override int GetHashCode()
    {
      return Round + (IsEnd ? 1 : 0) * 601 + (Position.GetHashCode() + 1000);
    }

    public override bool Equals(object obj)
    {
      if(obj is MapStateData msd)
      {
        return msd.IsEnd == IsEnd &&
          msd.Position.Equals(Position) &&
          msd.Round == Round;
      }
      return false;
    }



  }
}
