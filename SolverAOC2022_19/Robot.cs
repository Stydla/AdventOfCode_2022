using System.Security.Cryptography.X509Certificates;

namespace SolverAOC2022_19
{
  public class Robot
  {

    public ERobotType Type { get; set; }

    public Robot(ERobotType type)
    {
      Type = type;
    }

    public void Collect(Materials m)
    {
      switch (Type)
      {
        case ERobotType.Ore:
          m.Ore++;
          break;
        case ERobotType.Clay:
          m.Clay++;
          break;
        case ERobotType.Obsidian:
          m.Obsidian++;
          break;
        case ERobotType.Geode:
          m.Geode++;
          break;
      }
    }

    public void UnCollect(Materials m)
    {
      switch (Type)
      {
        case ERobotType.Ore:
          m.Ore--;
          break;
        case ERobotType.Clay:
          m.Clay--;
          break;
        case ERobotType.Obsidian:
          m.Obsidian--;
          break;
        case ERobotType.Geode:
          m.Geode--;
          break;
      }
    }

    public override string ToString()
    {
      return ((int)Type).ToString();
    }

  }

  public enum ERobotType
  {
    Ore = 0,
    Clay = 1,
    Obsidian = 2,
    Geode = 3
  }
}