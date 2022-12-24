using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_24
{
  internal partial class Map
  {

    public List<Blizzard> Blizzards { get; set; } = new List<Blizzard>();
    public Player Player { get; set; }

    public HashSet<Field> AllFields { get; set; } = new HashSet<Field>();

    public int MaxX {get; set; }
    public int MaxY { get; set; }

    public Field FinishField { get; set; }
    public Field StartField { get; set; }

    public int MapRepeat { get; set; }
    public int Round { get; set; }

    public Map(Map source)
    {
      
      AllFields = source.AllFields.Select(x => new Field(x)).ToHashSet();
      MaxX = source.MaxX;
      MaxY = source.MaxY;
      MapRepeat = source.MapRepeat;
      Round = source.Round;

      Field ff;
      AllFields.TryGetValue(source.FinishField, out ff);
      FinishField = ff;

      Field sf;
      AllFields.TryGetValue(source.StartField, out sf);
      StartField = sf;

      InitNeighbours();
      InitLoopNeighbours();

      Field playerField;
      AllFields.TryGetValue(source.Player.Position, out playerField);
      Player = new Player(playerField);

      foreach (Blizzard b in source.Blizzards)
      {
        Field f;
        AllFields.TryGetValue(b.Position, out f);
        Blizzard tmp = new Blizzard(f, b.Direction);
        Blizzards.Add(tmp);
      }
    }


    public Map(string input)
    {
      using(StringReader sr = new StringReader(input))
      {
        string line;
        int y = 0;
        while((line = sr.ReadLine()) != null)
        {
          for(int i = 0; i < line.Length; i++)
          {
            char c = line[i];
            switch (c)
            {
              case '#':
                {
                  Field f = new Field(i, y, EFieldType.WALL);
                  AllFields.Add(f);
                  break;
                }
              case '.':
                {
                  Field f = new Field(i, y, EFieldType.EMPTY);
                  AllFields.Add(f);
                  break;
                }
              case '^':
                {
                  Field f = new Field(i, y, EFieldType.EMPTY);
                  AllFields.Add(f);
                  Blizzard b = new Blizzard(f, EDirection.UP);
                  Blizzards.Add(b);
                  break;
                }
              case '>':
                {
                  Field f = new Field(i, y, EFieldType.EMPTY);
                  AllFields.Add(f);
                  Blizzard b = new Blizzard(f, EDirection.RIGHT);
                  Blizzards.Add(b);
                  break;
                }
              case '<':
                {
                  Field f = new Field(i, y, EFieldType.EMPTY);
                  AllFields.Add(f);
                  Blizzard b = new Blizzard(f, EDirection.LEFT);
                  Blizzards.Add(b);
                  break;
                }
              case 'v':
                {
                  Field f = new Field(i, y, EFieldType.EMPTY);
                  AllFields.Add(f);
                  Blizzard b = new Blizzard(f, EDirection.DOWN);
                  Blizzards.Add(b);
                  break;
                }
              default:
                throw new Exception($"Invalid map imput: {c}");
            }
          }
          y++;
        }
      }

      Field playerField;
      AllFields.TryGetValue(new Field(1, 0, EFieldType.UNKNOWN), out playerField);
      Player = new Player(playerField);

      MaxX = AllFields.Max(x => x.X);
      MaxY = AllFields.Max(x => x.Y);

      Field ff;
      AllFields.TryGetValue(new Field(MaxX - 1, MaxY, EFieldType.UNKNOWN), out ff);
      FinishField = ff;

      Field sf;
      AllFields.TryGetValue(new Field(1, 0, EFieldType.UNKNOWN), out sf);
      StartField = sf;

      InitNeighbours();
      InitLoopNeighbours();

      MapRepeat = LCM(MaxX - 1, MaxY - 1);
      Round = 0;

    }

    
    static int LCM(int a, int b)
    {
      return Math.Abs(a * b) / GCD(a, b);
    }
    static int GCD(int a, int b)
    {
      return b == 0 ? a : GCD(b, a % b);
    }

    private void InitLoopNeighbours()
    {
      foreach (Field f in AllFields)
      {
        if (f.Type == EFieldType.WALL) continue;
        if (f.Equals(StartField)) continue;
        if (f.Equals(FinishField)) continue;

        Field sourceField;
        Field targetField;

        targetField = f.Neighbours[EDirection.RIGHT];
        if(targetField.Type == EFieldType.WALL)
        {
          sourceField = new Field(1, f.Y, EFieldType.UNKNOWN);
          if (AllFields.TryGetValue(sourceField, out targetField))
          {
            f.LoopNeighbours.Add(EDirection.RIGHT, targetField);
          }
        } else
        {
          f.LoopNeighbours.Add(EDirection.RIGHT, targetField);
        }

        targetField = f.Neighbours[EDirection.DOWN];
        if (targetField.Type == EFieldType.WALL)
        {
          sourceField = new Field(f.X, 1, EFieldType.UNKNOWN);
          if (AllFields.TryGetValue(sourceField, out targetField))
          {
            f.LoopNeighbours.Add(EDirection.DOWN, targetField);
          }
        }
        else
        {
          f.LoopNeighbours.Add(EDirection.DOWN, targetField);
        }
        

        targetField = f.Neighbours[EDirection.LEFT];
        if (targetField.Type == EFieldType.WALL)
        {
          sourceField = new Field(MaxX - 1, f.Y, EFieldType.UNKNOWN);
          if (AllFields.TryGetValue(sourceField, out targetField))
          {
            f.LoopNeighbours.Add(EDirection.LEFT, targetField);
          }
        }
        else
        {
          f.LoopNeighbours.Add(EDirection.LEFT, targetField);
        }

        targetField = f.Neighbours[EDirection.UP];
        if (targetField.Type == EFieldType.WALL)
        {
          sourceField = new Field(f.X, MaxY - 1, EFieldType.UNKNOWN);
          if (AllFields.TryGetValue(sourceField, out targetField))
          {
            f.LoopNeighbours.Add(EDirection.UP, targetField);
          }
        }
        else
        {
          f.LoopNeighbours.Add(EDirection.UP, targetField);
        }
      }
    }

    private void InitNeighbours()
    {
      foreach(Field f in AllFields)
      {
        Field sourceField;
        Field targetField;

        sourceField = new Field(f.X + 1, f.Y, EFieldType.UNKNOWN);
        if (AllFields.TryGetValue(sourceField, out targetField))
        {
          f.Neighbours.Add(EDirection.RIGHT, targetField);
        }

        sourceField = new Field(f.X - 1, f.Y, EFieldType.UNKNOWN);
        if (AllFields.TryGetValue(sourceField, out targetField))
        {
          f.Neighbours.Add(EDirection.LEFT, targetField);
        }

        sourceField = new Field(f.X, f.Y + 1, EFieldType.UNKNOWN);
        if (AllFields.TryGetValue(sourceField, out targetField))
        {
          f.Neighbours.Add(EDirection.DOWN, targetField);
        }

        sourceField = new Field(f.X, f.Y - 1, EFieldType.UNKNOWN);
        if (AllFields.TryGetValue(sourceField, out targetField))
        {
          f.Neighbours.Add(EDirection.UP, targetField);
        }

        sourceField = new Field(f.X, f.Y, EFieldType.UNKNOWN);
        if (AllFields.TryGetValue(sourceField, out targetField))
        {
          f.Neighbours.Add(EDirection.NONE, targetField);
        }
      }
    }


    internal bool IsPlayerAtField(Field field)
    {
      return Player.Position.Equals(field);
    }

    public string GetMapHash()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(Player.Position);
      sb.Append(Round % MapRepeat);
      return sb.ToString();
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();

      for(int i = 0; i <= MaxY; i++)
      {
        for(int j = 0; j <= MaxX; j++)
        {
          Field searchField = new Field(j, i, EFieldType.UNKNOWN);
          Field actualField;
          AllFields.TryGetValue(searchField, out actualField);
          if (IsPlayerAtField(searchField))
          {
            if(actualField.Blizzards.Count > 0)
            {
              sb.Append("!");
            } else
            {
              sb.Append(Player.GetPrintValue());
            }
          } else
          {
            sb.Append(actualField.GetPrintValue());
          }
        }
        sb.AppendLine();
      }
      return sb.ToString();

    }

    internal void SimulateBlizzards()
    {
      foreach (Blizzard b in this.Blizzards)
      {
        b.Move();
      }
    }

    internal List<Field> GetNextPlayerPositions()
    {
      List<Field> ret = new List<Field>();
      foreach (EDirection dir in Enum.GetValues(typeof(EDirection)))
      {
        bool move = this.Player.Move(dir);
        if (move)
        {
          ret.Add(new Field(Player.Position));
          this.Player.MoveBack(dir);
        }
      }
      return ret;
    }

    internal void SetPlayerPosition(Field playerPosition)
    {
      Field f;
      AllFields.TryGetValue(playerPosition, out f);
      Player.Position = f;
    }

    internal void SetStartField(Field start)
    {
      Field f;
      AllFields.TryGetValue(start, out f);
      StartField= f;
      Player.Position = f;
    }

    internal void SetFinishField(Field finish)
    {
      Field f;
      AllFields.TryGetValue(finish, out f);
      FinishField = f;
    }
  }
}
