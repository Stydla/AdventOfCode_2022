using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
      List<Result> results = new List<Result>();
      List<Valve> path = new List<Valve>() { first };
      FindMaxPreasure(first, first.ValvesDistanceNonZeroFlowRate.Keys.ToList(), path, results, 0, 0, 0, 30, ref maxVal);
      return maxVal;
    }

    internal void FindMaxPreasure(Valve fromValve, List<Valve> valves, List<Valve> path, List<Result> results, int preasureSum, int currentPreasure, int currentMinute, int minutes, ref int maxValue)
    {
      if(valves.All(x=>x.Opened))
      {
        int rest = minutes - currentMinute;
        preasureSum += rest * currentPreasure;
        if(preasureSum > maxValue)
        {
          maxValue = preasureSum;
        }
        results.Add(new Result(preasureSum, new List<Valve>(path)));
        return;
      }

      int restTmp = minutes - currentMinute;
      int preasureSumTmp = preasureSum + restTmp * currentPreasure;
      results.Add(new Result(preasureSumTmp, new List<Valve>(path)));

      foreach (Valve v in valves)
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
          }
          results.Add(new Result(preasureSum, new List<Valve>(path)));
          preasureSum -= rest * currentPreasure;
          v.Opened = false;

          continue;
        }
        
        preasureSum += currentPreasure * (fromValve.ValvesDistanceNonZeroFlowRate[v] + 1);
        
        currentPreasure += v.FlowRate;
        path.Add(v);
        
        FindMaxPreasure(v, valves, path, results, preasureSum, currentPreasure, currentMinute, minutes, ref maxValue);
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
    

    public class Result
    {
      public List<Valve> Valves;
      public int Value;
      private long ID = 0;

      public Result(int value, List<Valve> valves)
      {
        Valves = valves;
        Value = value;
        foreach(Valve v in Valves)
        {
          ID += v.ID;
        }
      }

      
      

      public override bool Equals(object obj)
      {
        long res = (obj as Result).ID & ID;
        return res != 0;
      }

      public override int GetHashCode()
      {
        return (int)ID;
      }
    }

    public class CompareResult : IComparer<Result>
    {

      public int Compare(Result x, Result y)
      {
        return x.Value - y.Value;
      }

    }

    private static int BinarySearch(IList<Result> list, Result value)
    {
      if (list == null)
        throw new ArgumentNullException("list");
      var comp = new CompareResult();
      int lo = 0, hi = list.Count - 1;
      while (lo < hi)
      {
        int m = (hi + lo) / 2;  // this might overflow; be careful.
        if (comp.Compare(list[m], value) < 0) lo = m + 1;
        else hi = m - 1;
      }
      if (comp.Compare(list[lo], value) < 0) lo++;
      return lo;
    }


    internal int Solve2()
    {
      Valves.MeasureDistances();
      int maxVal = 0;
      Valve first = Valves.Items.Where(x => x.Name == "AA").First();
      List<Result> results = new List<Result>();
      List<Valve> path = new List<Valve>();
      FindMaxPreasure(first, first.ValvesDistanceNonZeroFlowRate.Keys.ToList(), path, results, 0, 0, 0, 26, ref maxVal);


      results = results.Where(x => x.Valves.Count > 0).OrderBy(x => x.Value).ToList();

      int maxVal2 = 0;

      for (int i = 0; i < results.Count; i++)
      {
        Result r = results[i];

        int tmpRestVal = maxVal2 - r.Value;
        int index = BinarySearch(results, new Result(tmpRestVal, new List<Valve>()));

        for(int j = index; j < results.Count; j++)
        {
          Result result2 = results[j];
          if (r.Equals(result2)) continue;
          
          if (result2.Value + r.Value > maxVal2)
          {
            maxVal2 = result2.Value + r.Value;
          }
        }
    
      }

      return maxVal2;
    }
  }
}