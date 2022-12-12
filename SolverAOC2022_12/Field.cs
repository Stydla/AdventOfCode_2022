using System.Collections.Generic;

namespace SolverAOC2022_12
{
  public class Field
  {

    public int X { get; set; }
    public int Y { get; set; }
    public char C { get; set; }
    public int Height { get; set; }
    public int Distance { get; set; } = 999;
    public List<Field> Neighbours { get; internal set; } = new List<Field>();

    public Field(char c, int x, int y)
    {
      C = c;
      X = x;
      Y = y;
      if(c == 'S')
      {
        Height = 0;
      } else if (c == 'E')
      {
        Height = 'z' - 'a';
      } else
      {
        Height = c - 'a';
      }
    }
  }
}