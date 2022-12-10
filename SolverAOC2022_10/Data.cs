using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_10
{
  internal class Data
  {
    private string inputData;

    private List<int> RegisterX = new List<int>();

    public Data(string inputData)
    {
      this.inputData = inputData;

      using (StringReader sr = new StringReader(inputData))
      {
        RegisterX.Add(1);
        int lastVal = 0;
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if(line.StartsWith("addx"))
          {
            
            RegisterX.Add(RegisterX.Last() + lastVal);
            RegisterX.Add(RegisterX.Last());

            lastVal = int.Parse(line.Substring(5));
          } else
          {
            RegisterX.Add(RegisterX.Last() + lastVal);
            lastVal = 0;
          }
        }
      }
    }

    internal int Solve1()
    {
      int sum = 0;
      for(int i = 0; i < 6; i++)
      {
        int index = 20 + 40 * i;
        sum += RegisterX[index] * index;
      }
      return sum;
    }

    internal string Solve2()
    {
      StringBuilder sb = new StringBuilder();
      int lineLength = 40;
      for(int i = 0; i < 240; i++) 
      {
        if (i % lineLength == 0 && i != 0)
        {
          sb.AppendLine();
        }

        int index = i % lineLength;
        int val = RegisterX[i+1];
        if (val - 1 <= index && val + 1 >= index)
        {
          sb.Append('#');
        } else
        {
          sb.Append('.');
        }

        
      }
      return sb.ToString();
    }
  }
}
