using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_16
{
  internal class Valve
  {
    public string Name { get; set; }

    public int FlowRate { get; set; }
    public List<Valve> ConnectedValves { get; set; } = new List<Valve>();

    public bool Opened { get; set; } = false;

    public Dictionary<Valve, int> ValvesDistance { get; set; } = new Dictionary<Valve, int>();
    public Dictionary<Valve, int> ValvesDistanceNonZeroFlowRate { get; set; } = new Dictionary<Valve, int>();

    public Valve(string name)
    {
      Name = name;
    }

    public void MeasureDistances(List<Valve> valves)
    {

      List<Valve> current = new List<Valve>();
      List<Valve> next = new List<Valve>();
      current.AddRange(ConnectedValves);

      int distance = 0;
      ValvesDistance[this] = distance;

      while (true)
      {
        distance++;
        if(current.Count == 0)
        {
          break;
        }
        
        foreach(Valve valve in current)
        {
          if (!ValvesDistance.ContainsKey(valve))
          {
            ValvesDistance[valve] = distance;
            next.AddRange(valve.ConnectedValves);
          }
        }
        
        current = next.Distinct().ToList();
        next.Clear();
      }

      foreach(var kv in ValvesDistance)
      {
        if(kv.Key.FlowRate > 0)
        {
          ValvesDistanceNonZeroFlowRate.Add(kv.Key, kv.Value);
        }
      }
    }

    public override string ToString()
    {
      return $"{Name}:{FlowRate}";
    }

  }
}
