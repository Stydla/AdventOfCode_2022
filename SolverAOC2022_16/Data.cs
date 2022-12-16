using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverAOC2022_16
{
  internal class Data
  {
    private string inputData;

    public Valves Valves { get; set; }

    public Data(string inputData)
    {
      this.inputData = inputData;

      Valves = new Valves(inputData);
    }

    internal int Solve1()
    {
      Valves.MeasureDistances();
      int maxVal = 0;
      Valve first = Valves.Items.Where(x=>x.Name == "AA").First();
      FindMaxPreasure(first, first.ValvesDistanceNonZeroFlowRate.Keys.ToList(), new List<Valve>() { first }, 0, 0, 0, 30, ref maxVal);
      return maxVal;
    }

    internal void FindMaxPreasure(Valve fromValve, List<Valve> valves, List<Valve> path, int preasureSum, int currentPreasure, int currentMinute, int minutes, ref int maxValue)
    {
      if(valves.All(x=>x.Opened))
      {
        int rest = minutes - currentMinute;
        preasureSum += rest * currentPreasure;
        if(preasureSum > maxValue)
        {
          maxValue = preasureSum;
          PrintPath(path);
        }
        return;
      }

      foreach(Valve v in valves)
      {
        if (v.Opened == true) continue;

        v.Opened = true;

        currentMinute += fromValve.ValvesDistanceNonZeroFlowRate[v] + 1;
        if (currentMinute > minutes)
        {
          currentMinute -= fromValve.ValvesDistanceNonZeroFlowRate[v] + 1;

          int rest = minutes - currentMinute;
          preasureSum += rest * currentPreasure;
          if (preasureSum > maxValue)
          {
            maxValue = preasureSum;
            PrintPath(path);
          }
          preasureSum -= rest * currentPreasure;
          v.Opened = false;

          continue;
        }

        preasureSum += currentPreasure * (fromValve.ValvesDistanceNonZeroFlowRate[v] + 1);
        
        currentPreasure += v.FlowRate;
        path.Add(v);
        FindMaxPreasure(v, valves, path, preasureSum, currentPreasure, currentMinute, minutes, ref maxValue);
        path.Remove(v);
        currentPreasure -= v.FlowRate;

        preasureSum -= currentPreasure * (fromValve.ValvesDistanceNonZeroFlowRate[v] + 1);
        currentMinute -= (fromValve.ValvesDistanceNonZeroFlowRate[v] + 1);

        v.Opened = false;
      }
    }

    private void PrintPath(List<Valve> path)
    {
      Console.WriteLine(string.Join(" ", path)); 
    }
    

    internal int Solve2()
    {
      throw new NotImplementedException();
    }
  }
}