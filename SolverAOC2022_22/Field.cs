using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace SolverAOC2022_22
{
  public class Field
  {

    public int Row { get; set; }
    public int Column { get; set; }

    public EFieldType Type { get; set; }

    public Dictionary<EDirection, Field> Neighbours { get; set; } = new Dictionary<EDirection, Field>();
    public char Value
    {
      get
      {
        switch (Type)
        {
          case EFieldType.EMPTY:
            return '.';
          case EFieldType.WALL:
            return '#';
        }
        return ' ';
      }
    }

    public Field(EFieldType type, int row, int column)
    {
      Type = type;
      Row = row;
      Column = column;
    }

    public static int GetCoordsHash(Field field)
    {
      return GetCoordsHash(field.Row, field.Column);
    }

    public static int GetCoordsHash(int row, int column)
    {
      return column * 256 + row;
    }

  }
}