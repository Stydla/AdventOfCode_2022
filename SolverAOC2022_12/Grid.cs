using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_12
{
  internal class Grid
  {

    public List<List<Field>> Fields { get; set; } = new List<List<Field>>();
    public List<Field> AllFields 
    { 
      get      
      {
        return Fields.SelectMany(x => x).ToList();
      } 
    }


    public Grid(string input)
    {
      using (StringReader sr = new StringReader(input))
      {

        int hIndex = 0;
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          List<Field> tmp = new List<Field>();
          for(int i = 0; i < line.Length; i++)
          {
            char c = line[i];
            Field f = new Field(c, i, hIndex);
            tmp.Add(f);
          }
          Fields.Add(tmp);
          hIndex++;
        }
      }
      InitNeighbours();
    }

    private void InitNeighbours()
    {
      for (int i = 0; i < Fields.Count; i++)
      {
        for(int j = 0; j < Fields[i].Count; j++)
        {
          Field f = Fields[i][j];
          for(int a = -1; a <= 1; a++)
          {
            for(int b = -1; b <= 1; b++)
            {
              if (a == b || a == -b) continue;
              int indexX = j + b;
              int indexY = i + a;

              if(indexX >= 0 && indexY >= 0 && indexX < Fields[i].Count && indexY < Fields.Count)
              {
                f.Neighbours.Add(Fields[indexY][indexX]);
              }
            }
          }
        }
      }

    }

    internal void FindDistances()
    {
      
      List<Field> current = AllFields.Where(x => x.C == 'E').ToList();

      int distance = 0;
      while(current.Count > 0)
      {
        current = SolveDistance(current, distance);
        distance++;
      }

    }

    private List<Field> SolveDistance(List<Field> current, int distance)
    {
      List<Field> next = new List<Field>();
      foreach(Field f in current)
      {
        if(f.Distance > distance)
        {
          f.Distance = distance;
          next.AddRange(f.Neighbours.Where(x=>f.Height - 1 <= x.Height));
        }
      }
      return next.Distinct().ToList();
    }

    public void Print()
    {
      StringBuilder sb = new StringBuilder();

      for (int i = 0; i < Fields.Count; i++)
      {
        for(int j = 0; j< Fields[i].Count; j++)
        {
          Field f = Fields[i][j];
          sb.Append($"{f.Distance,4}_{f.C}");
        }
        sb.AppendLine();
      }
      Console.WriteLine(sb.ToString());
    }
  }
}
