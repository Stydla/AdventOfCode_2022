namespace SolverAOC2022_17
{
  internal class Point
  {

    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }

    public override string ToString()
    {
      return $"[{X},{Y}]";
    }
  }
}