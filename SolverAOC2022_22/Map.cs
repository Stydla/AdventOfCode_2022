using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace SolverAOC2022_22
{
  internal class Map
  {

    public Field StartField { get; set; }

    public List<Field> AllFields { get; set; } = new List<Field>();
    public Dictionary<int, Field> DictFields { get; set; } = new Dictionary<int, Field>();

    public Player Player { get; set; }

    public Path Path { get; set; }

    public int MaxColumn { get; set; }
    public int MaxRow { get; set; }

    public Map(string input)
    {
      using (StringReader sr = new StringReader(input))
      {
        string line;
        int row = 1;
        while ((line = sr.ReadLine()) != string.Empty)
        {
          for (int column = 1; column <= line.Length; column++)
          {
            Field fTmp = null;
            char c = line[column - 1];
            if (c == ' ')
            {
              continue;
            }
            else if (c == '.')
            {
              fTmp = new Field(EFieldType.EMPTY, row, column);
              if(Player == null)
              {
                StartField = fTmp;
                Player = new Player(fTmp);
              }
            }
            else if (c == '#')
            {
              fTmp = new Field(EFieldType.WALL, row, column);
            }
            else
            {
              throw new Exception($"Invalid map input: {c}");
            }
            AllFields.Add(fTmp);
            int hash = Field.GetCoordsHash(fTmp);
            DictFields.Add(hash, fTmp);
          }
          row++;
        }

        line = sr.ReadLine();
        Path = new Path(line);
      }

      MaxColumn = AllFields.Max(x => x.Column);
      MaxRow = AllFields.Max(x => x.Row);


    }

    private void InitNeighbours1()
    {
      foreach(Field f in AllFields)
      {
        Field tmp;
        int hash;

        //UP
        hash = Field.GetCoordsHash(f.Row - 1, f.Column);
        if(!DictFields.TryGetValue(hash, out tmp))
        {
          int index = AllFields.Where(x => x.Column == f.Column).Max(x => x.Row);
          hash = Field.GetCoordsHash(index, f.Column);
          DictFields.TryGetValue(hash, out tmp);
        }
        f.NeighbourDirections.Add(EDirection.UP, new FieldDirection(tmp, ERotation.NONE));

        //RIGHT
        hash = Field.GetCoordsHash(f.Row, f.Column + 1);
        if (!DictFields.TryGetValue(hash, out tmp))
        {
          int index = AllFields.Where(x => x.Row == f.Row).Min(x => x.Column);
          hash = Field.GetCoordsHash(f.Row, index);
          DictFields.TryGetValue(hash, out tmp);
        }
        f.NeighbourDirections.Add(EDirection.RIGHT, new FieldDirection(tmp, ERotation.NONE));

        //DOWN
        hash = Field.GetCoordsHash(f.Row + 1, f.Column);
        if (!DictFields.TryGetValue(hash, out tmp))
        {
          int index = AllFields.Where(x => x.Column == f.Column).Min(x => x.Row);
          hash = Field.GetCoordsHash(index, f.Column);
          DictFields.TryGetValue(hash, out tmp);
        }
        f.NeighbourDirections.Add(EDirection.DOWN, new FieldDirection(tmp, ERotation.NONE));

        //LEFT
        hash = Field.GetCoordsHash(f.Row, f.Column - 1);
        if (!DictFields.TryGetValue(hash, out tmp))
        {
          int index = AllFields.Where(x => x.Row == f.Row).Max(x => x.Column);
          hash = Field.GetCoordsHash(f.Row, index);
          DictFields.TryGetValue(hash, out tmp);
        }
        f.NeighbourDirections.Add(EDirection.LEFT, new FieldDirection(tmp, ERotation.NONE));
      }
    }

    

    public void Solve1()
    {
      InitNeighbours1();
      while (!Path.IsFinished())
      {
        PathStep ps = Path.GetNextPathStep();

        Player.Move(ps);
      }
    }

    public void Print()
    {
      StringBuilder sb = new StringBuilder();
      for(int i = 1; i <= MaxRow; i++)
      {
        for(int j = 1; j <= MaxColumn; j++)
        {
          int hash = Field.GetCoordsHash(i, j);
          Field fTmp;
          if(DictFields.TryGetValue(hash, out fTmp))
          {
            sb.Append(fTmp.Value);
          } else
          {
            sb.Append(' ');
          }
        }
        sb.AppendLine();
      }
      Console.WriteLine(sb.ToString());
    }

    internal void Solve2()
    {
      InitNeighbours2();
      while (!Path.IsFinished())
      {
        PathStep ps = Path.GetNextPathStep();

        Player.Move(ps);
      }
    }

    private void InitNeighbours2()
    {
      if(AllFields.Count == 96 )
      {
        InitNeighbours2Test();
      } else
      {
        InitNeighbours2Task2();
      }
    }

    private void InitCommonNeighbours()
    {
      foreach (Field f in AllFields)
      {
        Field tmp;
        int hash;

        //UP
        hash = Field.GetCoordsHash(f.Row - 1, f.Column);
        if (DictFields.TryGetValue(hash, out tmp))
        {
          f.NeighbourDirections.Add(EDirection.UP, new FieldDirection(tmp, ERotation.NONE));
        }

        //RIGHT
        hash = Field.GetCoordsHash(f.Row, f.Column + 1);
        if (DictFields.TryGetValue(hash, out tmp))
        {
          f.NeighbourDirections.Add(EDirection.RIGHT, new FieldDirection(tmp, ERotation.NONE));
        }

        //DOWN
        hash = Field.GetCoordsHash(f.Row + 1, f.Column);
        if (DictFields.TryGetValue(hash, out tmp))
        {
          f.NeighbourDirections.Add(EDirection.DOWN, new FieldDirection(tmp, ERotation.NONE));
        }

        //LEFT
        hash = Field.GetCoordsHash(f.Row, f.Column - 1);
        if (DictFields.TryGetValue(hash, out tmp))
        {
          f.NeighbourDirections.Add(EDirection.LEFT, new FieldDirection(tmp, ERotation.NONE));
        }
      }
    }

    private void InitNeighbours2Task2()
    {
      InitCommonNeighbours();

      List<int> colStart = new List<int>() { 1, 51, 101 };
      List<int> colEnd = new List<int>() { 50, 100, 150 };
      List<int> rowStart = new List<int>() { 1, 51, 101, 151 };
      List<int> rowEnd = new List<int>() { 50, 100, 150, 200 };

      for (int i = 0; i < 50; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(rowStart[0], colStart[1] + i)];
        Field f2 = DictFields[Field.GetCoordsHash(rowStart[3] + i, colStart[0])];
        f1.NeighbourDirections.Add(EDirection.UP, new FieldDirection(f2, ERotation.RIGHT));
        f2.NeighbourDirections.Add(EDirection.LEFT, new FieldDirection(f1, ERotation.LEFT));
      }

      for (int i = 0; i < 50; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(rowStart[0] + i, colStart[1])];
        Field f2 = DictFields[Field.GetCoordsHash(rowEnd[2] - i, colStart[0])];
        f1.NeighbourDirections.Add(EDirection.LEFT, new FieldDirection(f2, ERotation.OPOSITE));
        f2.NeighbourDirections.Add(EDirection.LEFT, new FieldDirection(f1, ERotation.OPOSITE));
      }

      for (int i = 0; i < 50; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(rowStart[0], colStart[2] + i)];
        Field f2 = DictFields[Field.GetCoordsHash(rowEnd[3], colStart[0] + i)];
        f1.NeighbourDirections.Add(EDirection.UP, new FieldDirection(f2, ERotation.NONE));
        f2.NeighbourDirections.Add(EDirection.DOWN, new FieldDirection(f1, ERotation.NONE));
      }

      for (int i = 0; i < 50; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(rowStart[0] + i, colEnd[2])];
        Field f2 = DictFields[Field.GetCoordsHash(rowEnd[2] - i, colEnd[1])];
        f1.NeighbourDirections.Add(EDirection.RIGHT, new FieldDirection(f2, ERotation.OPOSITE));
        f2.NeighbourDirections.Add(EDirection.RIGHT, new FieldDirection(f1, ERotation.OPOSITE));
      }




      for (int i = 0; i < 50; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(rowEnd[0], colStart[2] + i)];
        Field f2 = DictFields[Field.GetCoordsHash(rowStart[1] + i, colEnd[1])];
        f1.NeighbourDirections.Add(EDirection.DOWN, new FieldDirection(f2, ERotation.RIGHT));
        f2.NeighbourDirections.Add(EDirection.RIGHT, new FieldDirection(f1, ERotation.LEFT));
      }

      for (int i = 0; i < 50; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(rowStart[1] + i, colStart[1])];
        Field f2 = DictFields[Field.GetCoordsHash(rowStart[2], colStart[0] + i)];
        f1.NeighbourDirections.Add(EDirection.LEFT, new FieldDirection(f2, ERotation.LEFT));
        f2.NeighbourDirections.Add(EDirection.UP, new FieldDirection(f1, ERotation.RIGHT));
      }

      for (int i = 0; i < 50; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(rowEnd[2], colStart[1] + i)];
        Field f2 = DictFields[Field.GetCoordsHash(rowStart[3] + i, colEnd[0])];
        f1.NeighbourDirections.Add(EDirection.DOWN, new FieldDirection(f2, ERotation.RIGHT));
        f2.NeighbourDirections.Add(EDirection.RIGHT, new FieldDirection(f1, ERotation.LEFT));
      }

    }

    private void InitNeighbours2Test()
    {
      InitCommonNeighbours();

      for (int i = 0; i < 4; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(5 + i, 12)];
        Field f2 = DictFields[Field.GetCoordsHash(9, 16 - i)];
        f1.NeighbourDirections.Add(EDirection.RIGHT, new FieldDirection(f2, ERotation.RIGHT));
        f2.NeighbourDirections.Add(EDirection.UP, new FieldDirection(f1, ERotation.LEFT));
      }

      for (int i = 0; i < 4; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(1, i + 9)];
        Field f2 = DictFields[Field.GetCoordsHash(5, 4 - i)];
        f1.NeighbourDirections.Add(EDirection.UP, new FieldDirection(f2, ERotation.OPOSITE));
        f2.NeighbourDirections.Add(EDirection.UP, new FieldDirection(f1, ERotation.OPOSITE));
      }

      for (int i = 0; i < 4; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(1 + i, 9)];
        Field f2 = DictFields[Field.GetCoordsHash(5, 5 + i)];
        f1.NeighbourDirections.Add(EDirection.LEFT, new FieldDirection(f2, ERotation.LEFT));
        f2.NeighbourDirections.Add(EDirection.UP, new FieldDirection(f1, ERotation.RIGHT));
      }

      for (int i = 0; i < 4; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(1 + i, 12)];
        Field f2 = DictFields[Field.GetCoordsHash(9 + i, 16)];
        f1.NeighbourDirections.Add(EDirection.RIGHT, new FieldDirection(f2, ERotation.OPOSITE));
        f2.NeighbourDirections.Add(EDirection.RIGHT, new FieldDirection(f1, ERotation.OPOSITE));
      }

      for (int i = 0; i < 4; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(5 + i, 1)];
        Field f2 = DictFields[Field.GetCoordsHash(12, 13 + i)];
        f1.NeighbourDirections.Add(EDirection.LEFT, new FieldDirection(f2, ERotation.RIGHT));
        f2.NeighbourDirections.Add(EDirection.DOWN, new FieldDirection(f1, ERotation.LEFT));
      }

      for (int i = 0; i < 4; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(8, 1 + i )];
        Field f2 = DictFields[Field.GetCoordsHash(12, 12 - i)];
        f1.NeighbourDirections.Add(EDirection.DOWN, new FieldDirection(f2, ERotation.OPOSITE));
        f2.NeighbourDirections.Add(EDirection.DOWN, new FieldDirection(f1, ERotation.OPOSITE));
      }

      for (int i = 0; i < 4; i++)
      {
        Field f1 = DictFields[Field.GetCoordsHash(8, 5 + i)];
        Field f2 = DictFields[Field.GetCoordsHash(12 - i, 9)];
        f1.NeighbourDirections.Add(EDirection.DOWN, new FieldDirection(f2, ERotation.LEFT));
        f2.NeighbourDirections.Add(EDirection.LEFT, new FieldDirection(f1, ERotation.RIGHT));
      }

      

    }
  }
}
