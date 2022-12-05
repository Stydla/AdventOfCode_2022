using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_05
{
  internal class Stacks : List<Stack>
  {
    private List<string> stackLines;

    public Stacks(List<string> stackLines)
    {
      this.stackLines = stackLines;

      int cnt = stackLines[0].Split(' ').Where(x => !string.IsNullOrEmpty(x)).Count();
      for(int i = 0; i < cnt; i++)
      {
        this.Add(new Stack(i));
      }

      for(int i = 1; i < stackLines.Count; i++)
      {
        string line = stackLines[i];
        for(int j = 0; j < this.Count; j++)
        {
          int index = j * 4 + 1;
          if(index < line.Length)
          {
            char item = line[index];
            if(item != ' ')
            {
              this[j].Items.Add(item);
            }
          }
        }
      }

    }

    internal string GetResult1()
    {
      StringBuilder sb = new StringBuilder();
      foreach(Stack s in this)
      {
        sb.Append(s.Items.Last());
      }
      return sb.ToString();
    }
  }
}
