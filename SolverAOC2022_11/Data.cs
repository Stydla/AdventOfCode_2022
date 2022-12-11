using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_11
{
  internal class Data
  {
    private string inputData;

    public List<Monkey> Monkeys = new List<Monkey>();
    BigInteger prod = 1;

    public Data(string inputData)
    {
      this.inputData = inputData;

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while (true)
        {
          Monkey tmp = new Monkey(sr);
          Monkeys.Add(tmp);

          if((line = sr.ReadLine()) == null){
            break;
          }
        }
      }
      
      foreach (Monkey m in Monkeys)
      {
        prod = prod * m.Test_DivisibleBy;
      }
    }

    internal BigInteger Solve1()
    {
      for(int i = 0; i < 20; i++)
      {
        ExecuteRound(3);
      }

      List<Monkey> res = Monkeys.OrderBy(x => x.InspectCount).ToList();
      res.Reverse();
      return res[0].InspectCount * res[1].InspectCount;
    }

    private void ExecuteRound(int divBy)
    {
      
      foreach (Monkey monkey in Monkeys)
      {
        monkey.Execute(Monkeys, divBy, prod);
      }
    }

    internal BigInteger Solve2()
    {
      for (int i = 0; i < 10000; i++)
      {
        ExecuteRound(1);
      }

      List<Monkey> res = Monkeys.OrderBy(x => x.InspectCount).ToList();
      res.Reverse();
      return res[0].InspectCount * res[1].InspectCount;
    }
  }
}
