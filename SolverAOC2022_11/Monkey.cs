using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2022_11
{
  internal class Monkey
  {
    public int MonkeyIndex;
    public List<BigInteger> Items = new List<BigInteger>();
    public BigInteger Test_DivisibleBy;
    public int TestTrue_TargetMonkey;
    public int TestFalse_TargetMonkey;
    public Func<BigInteger, BigInteger> Operation;

    public BigInteger InspectCount { get; set; } = 0;

    public Monkey(StringReader sr)
    {
      string line;

      line = sr.ReadLine();
      Match m = Regex.Match(line, "Monkey (\\d*):");
      MonkeyIndex = int.Parse(m.Groups[1].Value);

      line = sr.ReadLine();
      line = line.Substring("  Starting items:".Length);
      var vals =  line.Split(',');
      foreach(string val in vals)
      {
        int tmp = int.Parse(val);
        Items.Add(tmp);
      }

      line = sr.ReadLine();
      line = line.Substring("  Operation: new = ".Length);
      vals = line.Split(' ');
      if (vals[0] == "old")
      {
        if (vals[1] == "*")
        {
          if(vals[2] == "old")
          {
            Operation = new Func<BigInteger, BigInteger>((BigInteger oldval) => { return oldval * oldval; });
          } else
          {
            int num = int.Parse(vals[2]);
            Operation = new Func<BigInteger, BigInteger>((BigInteger oldval) => { return oldval * num; });
          }
        } else
        {
          // +
          if (vals[2] == "old")
          {
            Operation = new Func<BigInteger, BigInteger>((BigInteger oldval) => { return oldval + oldval; });
          }
          else
          {
            int num = int.Parse(vals[2]);
            Operation = new Func<BigInteger, BigInteger>((BigInteger oldval) => { return oldval + num; });
          }
        }
      } else
      {
        throw new NotImplementedException("OP not Implemented");
      }

      line = sr.ReadLine();
      line = line.Substring("  Test: divisible by ".Length);
      Test_DivisibleBy = int.Parse(line);

      line = sr.ReadLine();
      line = line.Substring("    If true: throw to monkey ".Length);
      TestTrue_TargetMonkey = int.Parse(line);

      line = sr.ReadLine();
      line = line.Substring("    If false: throw to monkey ".Length);
      TestFalse_TargetMonkey = int.Parse(line);

    }

    internal void Execute(List<Monkey> monkeys, BigInteger divBy, BigInteger prod)
    {
      foreach(BigInteger item in Items)
      {
        BigInteger tmp = Operation(item) / divBy;

        
        
        tmp = tmp % prod;
        

        InspectCount++;
        if(tmp % Test_DivisibleBy == 0)
        {
          monkeys[TestTrue_TargetMonkey].Items.Add(tmp);
        } else
        {
          monkeys[TestFalse_TargetMonkey].Items.Add(tmp);
        }
      }
      Items.Clear();
    }
  }
}
