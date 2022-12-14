using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_07
{
  class Folder
  {
    public string Name { get; }

    Folder Parent;
    public List<Folder> SubFolders = new List<Folder>();
    public List<File> Files = new List<File>();

    public int Size;

    public Folder(Folder parent, string name)
    {
      Parent = parent;
      Name = name;
    }

    public void CalculateFolderSize()
    {
      foreach (Folder f in SubFolders)
      {
        f.CalculateFolderSize();
      }
      int FilesSum = Files.Sum(x => x.Size);
      int FoldersSum = SubFolders.Sum(x => x.Size);
      Size = FilesSum + FoldersSum;
    }

    internal string Print()
    {
      StringBuilder sb = new StringBuilder();
      string offset = BuildOffset();
      sb.AppendLine(offset + Name);
      foreach(Folder f in SubFolders)
      {
        sb.Append(f.Print());
      }
      return sb.ToString();
    }

    internal List<string> AllFolderNames()
    {
      List<string> ret = new List<string>();
      ret.Add(this.Name);
      foreach(Folder f in SubFolders)
      {
        ret.AddRange(f.AllFolderNames());
      }
      return ret;
    }

    private string BuildOffset()
    {
      List<string> offset = new List<string>();
      Folder parent = this.Parent;
      Folder tmp = this;
      bool L_used = false;
      while (parent != null)
      {
        if(parent.SubFolders.Last() == tmp)
        {
          if(L_used)
          {
            offset.Add("  ");
          } else
          {
            offset.Add("┗━");
            L_used = true;
          }
        } else
        {
          if(!L_used)
          {
            offset.Add("┣━");
            L_used = true;
          } else
          {
            offset.Add("┃ ");
          }
          
        }
        tmp = parent;
        parent = tmp.Parent;
      }
      offset.Reverse();
      return string.Join("", offset);
    }

    internal void Solve2(int needToFree, ref int currentSmallest)
    {
      foreach(Folder f in SubFolders)
      {
        f.Solve2(needToFree, ref currentSmallest);
      }
      if(Size > needToFree)
      {
        if(Size < currentSmallest)
        {
          currentSmallest = Size;
        }
      }
    }

    internal void Solve1(int maxSize, ref int totalSize)
    {
      foreach (Folder f in SubFolders)
      {
        f.Solve1(maxSize, ref totalSize);
      }
      if(Size < maxSize)
      {
        totalSize += Size;
      }
    }

    internal Folder GetFolder(string val)
    {
      if(val == "..")
      {
        return Parent;
      }

      Folder res = SubFolders.Where(x => x.Name == val).FirstOrDefault();
      if(res == null)
      {
        res = new Folder(this, val);
        SubFolders.Add(res);
      }
      return res;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine(Name);
      foreach(Folder f in SubFolders)
      {
        sb.Append(f.ToString());
      }
      return sb.ToString();
    }
  }
}
