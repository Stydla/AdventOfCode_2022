using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2022_22
{
  public class Path
  {
    
    public List<PathStep> Steps { get; set; } = new List<PathStep>();

    private int CurrentStepIndex = 0;

    public Path(string input)
    {
      Match m = Regex.Match(input, "(\\d*)");
      string strVal = m.Groups[1].Value;
      int val = int.Parse(strVal);

      Steps.Add(new PathStep(ERotation.NONE, val));

      input = input.Substring(strVal.Length);

      MatchCollection mc = Regex.Matches(input, "(\\w)(\\d*)");
      foreach (Match m2 in mc)
      {
        string strRotation = m2.Groups[1].Value;
        strVal= m2.Groups[2].Value;
        val = int.Parse(strVal);
        ERotation rotation;

        switch(strRotation)
        {
          case "R":
            rotation = ERotation.RIGHT;
            break;
          case "L":
            rotation = ERotation.LEFT;
            break;
          default:
            throw new Exception($"Invalid rotation: {strRotation}");
        }

        Steps.Add(new PathStep(rotation, val));

      }
    }

    public PathStep GetNextPathStep()
    {
      return Steps[CurrentStepIndex++];
    }

    public bool IsFinished()
    {
      return CurrentStepIndex == Steps.Count;
    }

  }
}
