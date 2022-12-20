using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2022_19
{
  internal class Factory
  {

    public string Blueprint { get; set; }

    public Materials Material { get; set; } = new Materials();

    public List<Robot> Robots { get; set; } = new List<Robot>();

    public int MaxGeodes { get; set; }

    public Dictionary<ERobotType, Materials> RobotPrice { get; set; } = new Dictionary<ERobotType, Materials>();
    public int ID { get; set; }

    public Factory(string blueprint)
    {
      Blueprint = blueprint;
      Robots.Add(new Robot(ERobotType.Ore));

      Match m = Regex.Match(blueprint, "Blueprint (\\d*): Each ore robot costs (\\d*) ore\\. Each clay robot costs (\\d*) ore\\. Each obsidian robot costs (\\d*) ore and (\\d*) clay\\. Each geode robot costs (\\d*) ore and (\\d*) obsidian\\.");

      ID = int.Parse(m.Groups[1].Value);

      Materials oreRobot = new Materials();
      oreRobot.Ore = int.Parse(m.Groups[2].Value);

      Materials clayRobot = new Materials();
      clayRobot.Ore = int.Parse(m.Groups[3].Value);

      Materials obsidianRobot = new Materials();
      obsidianRobot.Ore = int.Parse(m.Groups[4].Value);
      obsidianRobot.Clay = int.Parse(m.Groups[5].Value);

      Materials geodeRobot = new Materials();
      geodeRobot.Ore = int.Parse(m.Groups[6].Value);
      geodeRobot.Obsidian = int.Parse(m.Groups[7].Value);

      RobotPrice.Add(ERobotType.Ore, oreRobot);
      RobotPrice.Add(ERobotType.Clay, clayRobot);
      RobotPrice.Add(ERobotType.Obsidian, obsidianRobot);
      RobotPrice.Add(ERobotType.Geode, geodeRobot);

    }

    public void Simulate1(int days)
    {
      Rec(0, days);
    }


    private List<ERobotType> order = new List<ERobotType>() { ERobotType.Ore, ERobotType.Clay, ERobotType.Obsidian, ERobotType.Geode };


    private HashSet<string> Solved = new HashSet<string>();

    private List<int> rest = new List<int>()
    {
      0
,0
,1
,2
,4
,6
,9
,12
,16
,20
,25
,30
,36
,42
,49
,56
,64
,72
,81
,90
,100
,110
,121
,132
,144
,156
,169
,182
,296
,210
,225
,240
,256
    };

    private void Rec(int currentDay, int maxDay)
    {
      int maxTmp = Material.Geode + Robots.Where(x => x.Type == ERobotType.Geode).Count() * (maxDay - currentDay) + 2*rest[(maxDay - currentDay)];
      if (MaxGeodes > maxTmp)
      {
        return;
      }
      if(currentDay >= maxDay)
      {
        if(MaxGeodes < Material.Geode)
        {
          MaxGeodes = Material.Geode;
        }
        return;
      }

      History h = new History(Robots, Material, currentDay);
      string hash = h.GenerateHash();
      if(Solved.Contains(hash))
      {
        return;
      } else
      {
        Solved.Add(hash);
      }

      

      foreach(ERobotType type in order)
      {
        Robot r = CreateRobot(type);

        foreach (Robot ro in Robots)
        {
          ro.Collect(Material);
        }

        if (r != null)
        {
          Robots.Add(r);
        }

        Rec(currentDay + 1, maxDay);

        if(r!= null)
        {
          Robots.Remove(r);
          RecycleRobot(r);
        }

        foreach (Robot ro in Robots)
        {
          ro.UnCollect(Material);
        }


      }

      

    }


    private Robot CreateRobot(ERobotType type)
    {
      if (Material.IsEnough(RobotPrice[type]))
      {
        Robot r = new Robot(type);
        Material.Remove(RobotPrice[type]);
        return new Robot(type);
      }
      return null;
    }

    private void RecycleRobot(Robot r)
    {
      Material.Add(RobotPrice[r.Type]);
    }



  }
}
