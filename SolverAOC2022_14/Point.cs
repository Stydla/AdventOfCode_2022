namespace SolverAOC2022_14
{
  internal class Point
  {
    private string lineTmp;

    public int X { get; set; }
    public int Y { get; set; }

    public Point(string lineTmp)
    {
      this.lineTmp = lineTmp;

      string[] tmp = lineTmp.Split(',');
      X = int.Parse(tmp[0]);
      Y = int.Parse(tmp[1]);

    }

    public Point(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }
  }
}