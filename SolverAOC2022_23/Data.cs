using System;
using System.Collections.Generic;
using System.Linq;

namespace SolverAOC2022_23
{
  internal class Data
  {
    private string inputData;

    public Data(string inputData)
    {
      this.inputData = inputData;
    }

    internal int Solve1()
    {
      Map m = new Map(inputData);
      for (int i = 0; i < 10; i++)
      {

        List<Consideration> allConsiderations = new List<Consideration>();
        List<Elf> onlyUpdateOrder = new List<Elf>();
        foreach(Elf elf in m.AllElves)
        {
          Consideration consideration = elf.Consider(m);
          if(consideration != null)
          {
            if (consideration.TargetField == null)
            {
              onlyUpdateOrder.Add(elf);
            } else
            {
              allConsiderations.Add(consideration);
            }
          }
        }

        var groups = allConsiderations.GroupBy(x => x.TargetField);
        foreach (var group in groups)
        {
          if(group.Count() == 1)
          {
            Consideration c = group.First();
            c.Execute(m);
          }
        }

        foreach(Elf e in m.AllElves)
        {
          e.UpdateConsiderOrder();
        }
      }
      
      return m.GetEmptyFieldCount();
    }

    internal int Solve2()
    {
      Map m = new Map(inputData);

      int round = 0; 
      while(true)
      {
        round++;
        List<Consideration> allConsiderations = new List<Consideration>();
        List<Elf> onlyUpdateOrder = new List<Elf>();
        foreach (Elf elf in m.AllElves)
        {
          Consideration consideration = elf.Consider(m);
          if (consideration != null)
          {
            if (consideration.TargetField == null)
            {
              onlyUpdateOrder.Add(elf);
            }
            else
            {
              allConsiderations.Add(consideration);
            }
          }
        }

        if(allConsiderations.Count == 0)
        {
          break;
        }

        var groups = allConsiderations.GroupBy(x => x.TargetField);
        foreach (var group in groups)
        {
          if (group.Count() == 1)
          {
            Consideration c = group.First();
            c.Execute(m);
          }
        }

        foreach (Elf e in m.AllElves)
        {
          e.UpdateConsiderOrder();
        }
      }

      return round;
    }
  }
}