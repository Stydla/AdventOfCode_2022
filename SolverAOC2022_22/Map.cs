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

      InitNeighbours();

    }

    private void InitNeighbours()
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
        f.Neighbours.Add(EDirection.UP, tmp);

        //RIGHT
        hash = Field.GetCoordsHash(f.Row, f.Column + 1);
        if (!DictFields.TryGetValue(hash, out tmp))
        {
          int index = AllFields.Where(x => x.Row == f.Row).Min(x => x.Column);
          hash = Field.GetCoordsHash(f.Row, index);
          DictFields.TryGetValue(hash, out tmp);
        }
        f.Neighbours.Add(EDirection.RIGHT, tmp);

        //DOWN
        hash = Field.GetCoordsHash(f.Row + 1, f.Column);
        if (!DictFields.TryGetValue(hash, out tmp))
        {
          int index = AllFields.Where(x => x.Column == f.Column).Min(x => x.Row);
          hash = Field.GetCoordsHash(index, f.Column);
          DictFields.TryGetValue(hash, out tmp);
        }
        f.Neighbours.Add(EDirection.DOWN, tmp);

        //LEFT
        hash = Field.GetCoordsHash(f.Row, f.Column - 1);
        if (!DictFields.TryGetValue(hash, out tmp))
        {
          int index = AllFields.Where(x => x.Row == f.Row).Max(x => x.Column);
          hash = Field.GetCoordsHash(f.Row, index);
          DictFields.TryGetValue(hash, out tmp);
        }
        f.Neighbours.Add(EDirection.LEFT, tmp);
      }
    }

    

    public void Solve1()
    {
      while(!Path.IsFinished())
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


  }
}
