using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_17
{
  internal class Chamber
  {


    public List<List<Point>> Lines { get; set; } = new List<List<Point>>();

    public long HeightOffset = 10;
    public long Height = 0;

    private int LR_Index = 0;
    private int Block_Index = 0;
    private string LR_Sequence;

    public Chamber(string lr_sequence)
    {
      LR_Sequence = lr_sequence;
      for(int i = 0; i < HeightOffset; i++)
      {
        Lines.Add(new List<Point>());
      }
    }

    public bool IsInCollision(Block b)
    {
      foreach(Point p in b.MovedPoints)
      {
        if (p.X < 0) return true;
        if (p.X >= 7) return true;
        if (p.Y < 0) return true;

        if (Lines[p.Y].Any(x => x.X == p.X))
        {
          return true;
        }
      }
      return false;
    }

    public void AddBlock(Block b)
    {
      int maxY = b.MovedPoints.Max(x => x.Y);

      long newHeight = Math.Max(maxY + 1, Height);
      if(newHeight > 0)
      {
        long diff = newHeight - Height;
        for(long i = 0; i < diff; i++)
        {
          Lines.Add(new List<Point>());
        }
        Height = newHeight;
      }

      foreach(Point p in b.MovedPoints)
      {
        Lines[p.Y].Add(p);
      }
    }



    private bool ExecuteRound(Block b)
    {
      MoveLR(b);
      bool res = MoveDown(b);
      return res;
    }

    private Block SpawnBlock()
    {
      Block b = new Block(Block_Index++ % 5);
      b.InitLocation((int)Height);
      return b;
    }

    private bool MoveDown(Block b)
    {
      b.Move(EDirection.DOWN);
      if(IsInCollision(b))
      {
        b.Move(EDirection.UP);
        AddBlock(b);
        return true;
      }
      return false;
    }

    private void MoveLR(Block b)
    {
      char symbol = LR_Sequence[LR_Index++ % LR_Sequence.Length];
      switch (symbol)
      {
        case '<':
          b.Move(EDirection.LEFT);
          if(IsInCollision(b))
          {
            b.Move(EDirection.RIGHT);
          }
          break;
        case '>':
          b.Move(EDirection.RIGHT);
          if (IsInCollision(b))
          {
            b.Move(EDirection.LEFT);
          }
          break;
      }
    }

    internal void Simulate(long rockCount)
    {
      
      for(long i = 0; i < rockCount; i++)
      {
        Block b = SpawnBlock();
        while(!ExecuteRound(b))
        {
        }
      }
    }

    private Dictionary<string, DiffData> History = new Dictionary<string, DiffData>();

    private class DiffData
    {
      public long Height { get; set; }
      public long RockCount { get; set; }
    }

    internal long Simulate2(long rockCount)
    {
      long computedRock = -1;
      long computedHeight = -1;
      long needToFinishRock = -1;
      int hashLenght = 50;


      for (long i = 0; i < rockCount; i++)
      {
        Block b = SpawnBlock();


        if(computedHeight == -1)
        {

        if(Height >= hashLenght)
        {
          int inputHash = CreateHash();
          string chamberHash = CreateChamberHash(hashLenght);
          if (chamberHash != null)
          {
            string hash = $"{inputHash}_{chamberHash}";
            if (History.ContainsKey(hash))
            {
              DiffData dd = History[hash];
              long diffHeight = Height - dd.Height;
              long diffRock = i - dd.RockCount;

              computedRock = (rockCount - i) / diffRock;
              needToFinishRock = (rockCount - i) % diffRock;
              computedHeight = computedRock * diffHeight;

              i = rockCount - needToFinishRock;
            }
            else
            {
              DiffData dd = new DiffData();
              dd.Height = Height;
              dd.RockCount = i;
              History.Add(hash, dd);
            }
          }
        }
        }
        

        while (!ExecuteRound(b))
        {
        }
        
      }
      return computedHeight;
      
    }

    private int CreateHash()
    {
      int a = Block_Index % 5;
      int b = (LR_Index % LR_Sequence.Length) * 5;
      return a + b;
    }

    private string CreateChamberHash(int count)
    {
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < count; i++)
      {
        List<Point> points = Lines[Lines.Count - 1 - count + i];
        foreach(Point p in points)
        {
          sb.Append($"[{p.X},{i}]");
        }
      }
      return sb.ToString();
      
    }

    private void Print(Block b)
    {
      StringBuilder sb = new StringBuilder();
      for(int i = Lines.Count - 1; i >= 0; i--)
      {
        List<char> line = new List<char>("|.......|".ToArray());
        foreach (Point p in Lines[i])
        {
          line[(int)p.X+1] = '#';
        }
        if(b != null)
        {
          foreach (Point p in b.MovedPoints.Where(x => x.Y == i))
          {
            line[(int)p.X + 1] = '@';
          }
        }
        sb.AppendLine(String.Join("", line));
      }
      sb.AppendLine();
      Console.Write(sb.ToString());
    }
  }
}
