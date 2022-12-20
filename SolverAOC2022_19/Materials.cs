using System;
using System.Text;

namespace SolverAOC2022_19
{
  public class Materials
  {

    public int Ore { get; set; }
    public int Clay { get; set; }
    public int Obsidian { get;  set;}
    public int Geode { get; set; }

    internal void Add(Materials materials)
    {
      Ore += materials.Ore;
      Clay += materials.Clay;
      Obsidian += materials.Obsidian;
      Geode += materials.Geode;
    }

    internal bool IsEnough(Materials m)
    {
      return Ore - m.Ore >= 0 &&
        Clay - m.Clay >= 0 &&
        Obsidian - m.Obsidian >= 0 &&
        Geode - m.Geode >= 0;
    }

    internal void Remove(Materials materials)
    {
      Ore -= materials.Ore;
      Clay -= materials.Clay;
      Obsidian -= materials.Obsidian;
      Geode -= materials.Geode;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(Ore > 4 ? "*" : Ore.ToString());
      sb.Append(Clay > 20 ? "*" : Clay.ToString());
      sb.Append(Obsidian > 20 ? "*" : Obsidian.ToString());
      sb.Append(Geode);
      return sb.ToString();

    }
  }
}