using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_05
{
  internal class Data
  {
    public string InputData;

    public Stacks Stacks;
    public List<Move> Moves;

    public Data(string inputData)
    {
      this.InputData = inputData;

      

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        List<string> stackLines = new List<string>();
        while ((line = sr.ReadLine()) != null)
        {
          if(string.IsNullOrEmpty(line))
          {
            //load moves
            break;
          }
          stackLines.Add(line);
        }
        stackLines.Reverse();
        Stacks = new Stacks(stackLines);

        Moves = new List<Move>();
        while ((line = sr.ReadLine()) != null)
        {
          
          Moves.Add(new Move(line));
        }
      }

    }

    internal string Solve1()
    {
      foreach(Move m in Moves)
      {
        m.Execute1(Stacks);
      }
      return Stacks.GetResult1();
    }

    internal string Solve2()
    {
      foreach (Move m in Moves)
      {
        m.Execute2(Stacks);
      }
      return Stacks.GetResult1();
    }
  }
}
