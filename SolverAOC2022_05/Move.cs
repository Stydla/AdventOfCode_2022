using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2022_05
{
  internal class Move
  {

    public int Count;
    public int From;
    public int To;

    public Move(string line)
    {
      Regex r = new Regex("move (\\d*) from (\\d*) to (\\d*)");
      Match m = r.Match(line);
      Count = int.Parse(m.Groups[1].Value);
      From = int.Parse(m.Groups[2].Value) -1;
      To = int.Parse(m.Groups[3].Value) -1;
    }

    internal void Execute1(Stacks stacks)
    {
      var items = stacks[From].Items.Skip(stacks[From].Items.Count - Count).ToList();
      stacks[From].Items.RemoveRange(stacks[From].Items.Count - Count, Count);
      items.Reverse();
      stacks[To].Items.AddRange(items);      
    }
    internal void Execute2(Stacks stacks)
    {
      var items = stacks[From].Items.Skip(stacks[From].Items.Count - Count).ToList();
      stacks[From].Items.RemoveRange(stacks[From].Items.Count - Count, Count);
      stacks[To].Items.AddRange(items);
    }
  }
}
