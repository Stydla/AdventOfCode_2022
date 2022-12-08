using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_08
{
  internal class Data
  {
    private string inputData;

    public Grid Grid { get; set; }

    public Data(string inputData)
    {
      this.inputData = inputData;

      Grid = new Grid();
      using (StringReader sr = new StringReader(inputData))
      {
        
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          List<Tree> treeLine = new List<Tree>();
          foreach (char c in line)
          {
            int i = c - '0';
            treeLine.Add(new Tree(i));
          }
          Grid.Trees.Add(treeLine);
        }
        
      }

    }

    internal int Solve1()
    {
      Grid.SolveVisibility();
      return Grid.VisbleTreesCount();
    }

    internal int Solve2()
    {
      Grid.ScoreTrees();
      return Grid.HighestScore();
    }
  }
}
