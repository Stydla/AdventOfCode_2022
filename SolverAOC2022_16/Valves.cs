using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2022_16
{
  internal class Valves
  {

    public List<Valve> Items { get; set; } = new List<Valve>();

    public Valves(string inputData)
    {
      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          AddValve(line);
        }
      }
    }

    public void AddValve(string inputLine)
    {
      //Valve AA has flow rate=0; tunnels lead to valves DD, II, BB

      Match m = Regex.Match(inputLine, "Valve (\\w*) has flow rate=(\\d+); tunnel[s]{0,1} lead[s]{0,1} to valve[s]{0,1} (.*)");
      Valve v = GetOrCreateValve(m.Groups[1].Value);
      int flowRate = int.Parse(m.Groups[2].Value);
      v.FlowRate = flowRate;

      List<Valve> valvesTmp = new List<Valve>();
      string[] connectedValves = m.Groups[3].Value.Replace(" ","").Split(',');
      foreach(string valveName in connectedValves)
      {
        Valve vTmp = GetOrCreateValve(valveName);
        v.ConnectedValves.Add(vTmp);
      }
    }

    


    private Valve GetOrCreateValve(string name)
    {
      Valve v = Items.FirstOrDefault(x=>x.Name == name);
      if (v != null)
      {
        return v;
      }
      v = new Valve(name);
      Items.Add(v);
      return v;
    }

    public void MeasureDistances()
    {
      foreach(Valve v in Items)
      {
        v.MeasureDistances(Items);
      }
    }


  }
}
