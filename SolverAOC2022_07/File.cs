using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_07
{
  class File
  {

    public string Name { get; }
    public int Size { get; }

    public File(string name, int size)
    {
      Name = name;
      Size = size;
    }


  }
}
