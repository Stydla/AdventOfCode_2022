using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Schema;

namespace SolverAOC2022_23
{
  public class Map
  {

    public List<Elf> AllElves { get; set; } = new List<Elf>();
    public Dictionary<Field, Elf> ElvesByPosition { get; set; } = new Dictionary<Field, Elf>();

    public Map(string input)
    {
      using(StringReader sr = new StringReader(input))
      {
        string line;
        int row = 0;
        while((line = sr.ReadLine()) != null)
        {
          for(int i = 0; i < line.Length; i++)
          {
            char c = line[i];
            if(c == '#')
            {
              Elf elf = new Elf(i, row);
              AllElves.Add(elf);
              ElvesByPosition.Add(elf.Position, elf);
            }
          }
          row++;
        }
      }
    }

    public int GetEmptyFieldCount()
    {
      int minX = AllElves.Min(x => x.Position.X);
      int maxX = AllElves.Max(x => x.Position.X);
      int minY = AllElves.Min(x => x.Position.Y);
      int maxY = AllElves.Max(x => x.Position.Y);

      int total = (maxX - minX + 1) * (maxY - minY + 1);
      return total - AllElves.Count();

    }

    public void Print(int round)
    {
      int minX = AllElves.Min(x => x.Position.X);
      int maxX = AllElves.Max(x => x.Position.X);
      int minY = AllElves.Min(x => x.Position.Y);
      int maxY = AllElves.Max(x => x.Position.Y);

      StringBuilder sb = new StringBuilder();
      sb.AppendLine($"After Round {round}");
      for (int i = minY; i <= maxY; i++)
      {
        for (int j = minX; j <= maxX; j++)
        {
          Field hash = new Field(j, i);
          Elf elfTmp;
          if (ElvesByPosition.TryGetValue(hash, out elfTmp))
          {
            sb.Append('#');
          }
          else
          {
            sb.Append('.');
          }
        }
        sb.AppendLine();
      }
        sb.AppendLine();
        Console.WriteLine(sb.ToString());
    }
  }
}