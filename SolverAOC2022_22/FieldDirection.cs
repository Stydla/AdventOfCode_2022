using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_22
{
  public class FieldDirection
  {

    public Field Field {get; set;}
    public ERotation Rotation { get; set;}

    public FieldDirection(Field field, ERotation rotation)
    {
      Field = field;
      Rotation = rotation;
    } 
  }
}
