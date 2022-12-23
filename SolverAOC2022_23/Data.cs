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
      m.Print(0);
      for (int i = 0; i < 10; i++)
      {

        List<List<Consideration>> allConsiderations = new List<List<Consideration>>();
        List<Elf> onlyUpdateOrder = new List<Elf>();
        foreach(Elf elf in m.AllElves)
        {
          List<Consideration> considerations = elf.Consider(m);
          if(considerations != null)
          {
            if(considerations.Count == 0)
            {
              onlyUpdateOrder.Add(elf);
            } else
            {
              allConsiderations.Add(considerations);
            }
          } 
        }

        foreach(List<Consideration> considerations in allConsiderations)
        {
          foreach(Consideration consideration in considerations)
          {
            if (allConsiderations.SelectMany(x => x).Count(x=>x.TargetField.Equals(consideration.TargetField)) == 1)
            {
              consideration.Execute(m);
              break;
            }
          }
          considerations[0].Elf.UpdateConsiderOrder();
        }

        foreach(Elf elf in onlyUpdateOrder)
        {
          elf.UpdateConsiderOrder();
        }
        m.Print(i + 1);
      }
      
      return m.GetEmptyFieldCount();
    }

    internal int Solve2()
    {
      throw new NotImplementedException();
    }
  }
}