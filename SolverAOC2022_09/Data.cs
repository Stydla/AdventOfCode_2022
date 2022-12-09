using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_09
{
  class Data
  {

    private string InputData;

    public List<Move> Moves = new List<Move>();

    public Data(string inputData)
    {
      InputData = inputData;

      using (StringReader sr = new StringReader(inputData))
      {

        string line;
        while ((line = sr.ReadLine()) != null)
        {
          Move m = new Move(line);
          Moves.Add(m);
        }
      }
    }

    internal int Solve2()
    {
      Rope r = new Rope(9);
      foreach (Move m in Moves)
      {
        r.ExecuteMove(m);
      }
      return r.GetTailLocationsCount();
    }

    internal int Solve1()
    {
      Rope r = new Rope(1);
      foreach (Move m in Moves)
      {
        r.ExecuteMove(m);
      }
      return r.GetTailLocationsCount();
    }
  }
}
