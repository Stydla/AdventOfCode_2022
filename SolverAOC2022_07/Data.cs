using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2022_07
{
  class Data
  {
    private string inputData;

    private FileSystem FileSystem;

    public Data(string inputData)
    {
      this.inputData = inputData;
      FileSystem = new FileSystem();
      Folder currentFolder = null;

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {

          if(line.StartsWith("$ cd"))
          {
            string val = line.Substring(5);
            if(val == "/")
            {
              currentFolder = FileSystem.Root;
            } else
            {
              currentFolder = currentFolder.GetFolder(val);
            }
          } else if(line.StartsWith("$ ls"))
          {
            // ignore
          } else if(line.StartsWith("dir "))
          {
            // ignore
          } else
          {
            //file
            //8504156 c.dat
            Regex r = new Regex("^(\\d*) (.*)$");
            Match m = r.Match(line);
            int size = int.Parse(m.Groups[1].Value);
            string name = m.Groups[2].Value;
            currentFolder.Files.Add(new File(name, size));

          }
        }
      }

      //string tree = FileSystem.Print();
      //List<string> allFolders = FileSystem.AllFolderNames().OrderBy(x => x).ToList();
    }

    internal int Solve1()
    {
      return FileSystem.SumFileSize(100000);
    }

    internal int Solve2()
    {
      FileSystem.Root.CalculateFolderSize();
      int needToFree = 30000000 - (70000000 - FileSystem.Root.Size);

      return FileSystem.FindSmallest(needToFree);

    }
  }
}
