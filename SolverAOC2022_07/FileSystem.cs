using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_07
{
  class FileSystem
  {

    public Folder Root = new Folder(null, "/");

    public FileSystem()
    {

    }

    internal int SumFileSize(int maxSize)
    {
      Root.CalculateFolderSize();
      int totalSize = 0;
      Root.Solve1(maxSize, ref totalSize);
      return totalSize;
    }

    internal int FindSmallest(int needToFree)
    {
      int currentSmallest = int.MaxValue;
      Root.Solve2(needToFree, ref currentSmallest);
      return currentSmallest;
    }
  }
}
