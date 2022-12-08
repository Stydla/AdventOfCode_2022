using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_08
{
  internal class Grid
  {

    public List<List<Tree>> Trees { get; set; } = new List<List<Tree>>();

    internal void SolveVisibility()
    {
      foreach(List<Tree> t in Trees)
      {
        SolveVisibility(t);
        SolveVisibility(Reverse(t).ToList());
      }

      for(int i = 0; i < Trees[0].Count; i++)
      {
        List<Tree> tmp = Trees.Select(x => x[i]).ToList();
        SolveVisibility(tmp);
        SolveVisibility(Reverse(tmp).ToList());
      }
      
    }

    private IEnumerable<Tree> Reverse (List<Tree> items)
    {
      for (int i = items.Count - 1; i >= 0; i--)
      {
        yield return items[i];
      }
    }

    private void SolveVisibility(List<Tree> tree)
    {
      int currentLength = -1;
      for (int i = 0; i < tree.Count; i++)
      {
        if (tree[i].Size > currentLength)
        {
          currentLength = tree[i].Size;
          tree[i].IsVisible = true;
        }
      }
    }

    internal int VisbleTreesCount()
    {
      return Trees.SelectMany(x=>x).Where(x=>x.IsVisible).Count();
    }

    internal void ScoreTrees()
    {
      for(int i = 0; i < Trees.Count; i++)
      {
        for(int j = 0; j < Trees[i].Count; j++)
        {
          SolveTreeScore(i, j);
        }
      }
    }

    private void SolveTreeScore(int i, int j)
    {
      //vlevo
      List<int> results = new List<int>();
      int h = Trees[i][j].Size;

      int cnt = 0;
      for(int a = j - 1; a >= 0; a--)
      {
        cnt++;
        if (Trees[i][a].Size >= h)
        {
          break;
        }
        
      }
      results.Add(cnt);

      //vpravo
      cnt = 0;
      for (int a = j + 1; a < Trees[i].Count; a++)
      {
        cnt++;
        if (Trees[i][a].Size >= h)
        {
          break;
        }
      }
      results.Add(cnt);

      //nahoru
      cnt = 0;
      for (int a = i - 1; a >= 0; a--)
      {
        cnt++;
        if (Trees[a][j].Size >= h)
        {
          break;
        }
      }
      results.Add(cnt);

      //dolu
      cnt = 0;
      for (int a = i + 1; a < Trees.Count; a++)
      {
        cnt++;
        if (Trees[a][j].Size >= h)
        {
          break;
        }
      }
      results.Add(cnt);

      Trees[i][j].TreeScore = results.Aggregate((x, y) => x * y);
    }

    internal int HighestScore()
    {
      return Trees.SelectMany(x => x).Max(x => x.TreeScore);
    }
  }
}
