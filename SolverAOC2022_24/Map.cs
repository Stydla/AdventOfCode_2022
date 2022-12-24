using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_24
{
  internal class Map
  {

    public List<Blizzard> Blizzards { get; set; } = new List<Blizzard>();
    public Player Player { get; set; }

    public HashSet<Field> AllFields { get; set; } = new HashSet<Field>();

    public int MaxX {get; set; }
    public int MaxY { get; set; }

    public Field FinishField { get; set; }
    public Field StartField { get; set; }

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
                  break;
                }
              case '>':
                {
                  Field f = new Field(i, y, EFieldType.EMPTY);
                  AllFields.Add(f);
                  Blizzard b = new Blizzard(f, EDirection.RIGHT);
                  break;
                }
              case '<':
                {
                  Field f = new Field(i, y, EFieldType.EMPTY);
                  AllFields.Add(f);
                  Blizzard b = new Blizzard(f, EDirection.LEFT);
                  break;
                }
              case 'v':
                {
                  Field f = new Field(i, y, EFieldType.EMPTY);
                  AllFields.Add(f);
                  Blizzard b = new Blizzard(f, EDirection.DOWN);
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


      Console.WriteLine(this);
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
      }
    }

    internal bool IsPlayerAtField(Field field)
    {
      return Player.Position.Equals(field);
    }

    private string CreateMapHash()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(Player.Position);
      foreach(Blizzard b in Blizzards)
      {
        sb.Append(b.Position);
      }
      return sb.ToString();
    }

    internal int Simulate()
    {
      throw new NotImplementedException();
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
  }
}
