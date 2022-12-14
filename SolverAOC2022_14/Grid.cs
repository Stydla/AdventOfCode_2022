using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_14
{
  internal class Grid
  {

    public List<List<Field>> Fields { get; set; } = new List<List<Field>>();
    List<List<Point>> Paths = new List<List<Point>>();

    Dictionary<string, Field> FastSearch = new Dictionary<string, Field>();

    public int X_Offset { get; set; }

    public Grid(string input)
    {
      //498,4 -> 498,6 -> 496,6
      //503,4-> 502,4-> 502,9-> 494,9

      
      using (StringReader sr = new StringReader(input))
      {
        string inputLine;
        while ((inputLine = sr.ReadLine()) != null)
        {
          List<Point> path = new List<Point>();
          string[] pointsTmp = inputLine.Split(new string[] { " -> " }, StringSplitOptions.None);
          foreach(string point in pointsTmp)
          {
            Point l = new Point(point);
            path.Add(l);
          }
          Paths.Add(path);
        }
      }

      

      
    }

    private void InitNeighbours()
    {
      foreach(Field f in Fields.SelectMany(x=>x))
      {
        f.InitNeighbours(FastSearch);
      }
    }

    private void InitGrid(int minX, int maxX, int minY, int maxY)
    {
      X_Offset = minX - 1;
      for(int i = minY; i <= maxY; i++)
      {
        List<Field> fTmp = new List<Field>();
        for(int j = minX - 1; j <= maxX+1; j++)
        {
          Field f = new Field(j, i);
          fTmp.Add(f);
          FastSearch.Add(f.ID, f);
        }
        Fields.Add(fTmp);
      }
    }

    private void AddPaths(List<List<Point>> paths)
    {
      foreach(List<Point> path in paths)
      {
        AddPaths(path);
      }
    }

    private void AddPaths(List<Point> path)
    {
      for (int i = 1; i < path.Count; i++)
      {
        Point from = path[i - 1];
        Point to = path[i];
        
        if (from.X == to.X)
        {
          foreach(int j in Enumerable.Range(Math.Min(from.Y, to.Y), Math.Abs(from.Y - to.Y)+1))
          {
            Fields[j][from.X - X_Offset].Value = '#';
          }
        }
        if (from.Y == to.Y)
        {
          foreach (int j in Enumerable.Range(Math.Min(from.X, to.X), Math.Abs(from.X - to.X)+1))
          {
            Fields[from.Y][j - X_Offset].Value = '#';
          }
        }
      }
    }

    private string Print()
    {
      StringBuilder sb = new StringBuilder();
      for(int i = 0; i < Fields.Count; i++)
      {
        for(int j = 0; j < Fields[i].Count; j++)
        {
          sb.Append(Fields[i][j].Value);
        }
        sb.AppendLine();
      }
      return sb.ToString();
    }

    internal int SandCount()
    {
      return Fields.SelectMany(x=>x).Where(y=>y.Value == 'o').Count();
    }

    internal void Simulate1()
    {
      int minX = Paths.SelectMany(x => x).Min(x => x.X);
      int maxX = Paths.SelectMany(x => x).Max(x => x.X);
      int minY = Paths.SelectMany(x => x).Min(x => x.Y);
      int maxY = Paths.SelectMany(x => x).Max(x => x.Y);

      InitGrid(minX, maxX, 0, maxY);
      InitNeighbours();
      AddPaths(Paths);

      string id = Field.GetID(500, 0);
      Field pourField = FastSearch[id];

      while(true)
      {
        pourField.Value = 'o';

        if(!MoveSand(pourField))
        {
          break;
        } 
      }
      
    }

    internal void Simulate2()
    {
      
      int minY = Paths.SelectMany(x => x).Min(x => x.Y);
      int maxY = Paths.SelectMany(x => x).Max(x => x.Y) + 2;
      int minX = 500 - maxY;
      int maxX = 500 + maxY;

      List<Point> path = new List<Point>();
      path.Add(new Point(minX, maxY));
      path.Add(new Point(maxX, maxY));
      Paths.Add(path);

      InitGrid(minX, maxX, 0, maxY);
      InitNeighbours();
      AddPaths(Paths);


      

      string id = Field.GetID(500, 0);
      Field pourField = FastSearch[id];

      while (true)
      {
        pourField.Value = 'o';

        if (!MoveSand(pourField))
        {
          break;
        }
        if (pourField.Value == 'o') break;
      }
      Console.WriteLine(Print());
    }

    private bool MoveSand(Field field)
    {
      Field current = field;
      Field next;
      while (true)
      {
        next = current.MoveSand();
        if(next == current)
        {
          return true;
        } 
        
        if(next == null)
        {
          return false;
        }

        current = next;
      }
    }
  }
}
