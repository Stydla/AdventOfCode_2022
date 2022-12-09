using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_09
{
  class Rope
  {

    public Location Head { get; } = new Location(0, 0);
    //public Location Tail { get; } = new Location(0, 0);

    public List<Location> TailHistory = new List<Location>();

    public List<Location> Knots = new List<Location>();

    public Rope(int knotCount)
    {
      for(int i = 0; i < knotCount; i++)
      {
        Knots.Add(new Location(0, 0));
      }
    }

    public void UpdateKnot(Location first, Location next)
    {
      if(first.X - 2 == next.X && first.Y - 2 == next.Y)
      {
        next.X++;
        next.Y++;
      } 
      else if (first.X + 2 == next.X && first.Y - 2 == next.Y)
      {
        next.X--;
        next.Y++;
      } 
      else if (first.X - 2 == next.X && first.Y + 2 == next.Y)
      {
        next.X++;
        next.Y--;
      } 
      else if (first.X + 2 == next.X && first.Y + 2 == next.Y)
      {
        next.X--;
        next.Y--;
      } 
      else if (first.X - 2 == next.X)
      {
        next.X++;
        next.Y = first.Y;
      }
      else if (first.X + 2 == next.X)
      {
        next.X--;
        next.Y = first.Y;
      }
      else if (first.Y - 2 == next.Y)
      {
        next.Y++;
        next.X = first.X;
      }
      else if (first.Y + 2 == next.Y)
      {
        next.Y--;
        next.X = first.X;
      }
     

      
    }

    public void ExecuteMove(Move move)
    {
      for(int i = 0; i < move.Count; i++)
      {
        switch (move.Direction)
        {
          case Direction.Up:
            Head.Y++;
            break;
          case Direction.Down:
            Head.Y--;
            break;
          case Direction.Left:
            Head.X--;
            break;
          case Direction.Right:
            Head.X++;
            break;
          default:
            throw new Exception("Direction not found");
        }
        UpdateTail();
      }
    
    }

    private void UpdateTail()
    {
      Location first = Head;
      foreach(Location next in Knots)
      {
        UpdateKnot(first, next);
        first = next;
      }
      TailHistory.Add(new Location(Knots.Last()));
    }

    internal int GetTailLocationsCount()
    {
      return TailHistory.GroupBy(x => $"{x.X} {x.Y}").Count();
    }
  }
}
