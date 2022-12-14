using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_14
{
  internal class Field
  {

    public int X { get; set; }
    public int Y { get; set; }
    public string ID;

    public char Value { get; set; }

    public Field Down { get; set; }
    public Field DownLeft { get; set; }
    public Field DownRight { get; set; }

    public Field(int x, int y)
    {
      X = x;
      Y = y;
      Value = '.';
      ID = GetID(x, y);
    }

    public override string ToString()
    {
      return $"[{X},{Y}]: {Value}";
    }

    public static string GetID(int x, int y)
    {
      return $"{x},{y}";
    }

    internal void InitNeighbours(Dictionary<string, Field> fastSearch)
    {
      string idTmp = GetID(X - 1, Y + 1);
      if (fastSearch.ContainsKey(idTmp))
      {
        DownLeft = fastSearch[idTmp];
      }

      idTmp = GetID(X, Y + 1);
      if (fastSearch.ContainsKey(idTmp))
      {
        Down = fastSearch[idTmp];
      }

      idTmp = GetID(X + 1, Y + 1);
      if (fastSearch.ContainsKey(idTmp))
      {
        DownRight = fastSearch[idTmp];
      }
    }

    public Field MoveSand()
    {
      if(Down != null && Down.Value == '.')
      {
        this.Value = '.';
        Down.Value = 'o';
        return Down;
      }

      if (DownLeft != null && DownLeft.Value == '.')
      {
        this.Value = '.';
        DownLeft.Value = 'o';
        return DownLeft;
      }

      if (DownRight != null && DownRight.Value == '.')
      {
        this.Value = '.';
        DownRight.Value = 'o';
        return DownRight;
      }

      if(Down != null)
      {
        return this;
      }

      this.Value = '.';
      return null;
    }
  }
}
