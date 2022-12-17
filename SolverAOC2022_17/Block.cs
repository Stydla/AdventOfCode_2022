using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_17
{
  internal class Block
  {

    public Point Location { get; set; }
    public int Type { get; }
    public List<Point> Points { get; set; } = new List<Point>();
    public IEnumerable<Point> MovedPoints
    {
      get
      {
        return Points.Select(x => new Point(x.X + Location.X, x.Y + Location.Y));
      }
    }

    public Block(int type)
    {
      Type = type;

      switch (type)
      {
        case 0:
          {
            //####
            for (int i = 0; i < 4; i++)
            {
              Points.Add(new Point(i, 0));
            }
          }
          break;
        case 1:
          {
            //.#.
            //###
            //.#.
            Points.Add(new Point(1, 0));
            Points.Add(new Point(0, 1));
            Points.Add(new Point(1, 1));
            Points.Add(new Point(2, 1));
            Points.Add(new Point(1, 2));

          }
          break;
        case 2:
          {
            //..#
            //..#
            //###
            Points.Add(new Point(0, 0));
            Points.Add(new Point(1, 0));
            Points.Add(new Point(2, 0));
            Points.Add(new Point(2, 1));
            Points.Add(new Point(2, 2));
          }
          break;
        case 3:
          {
            //#
            //#
            //#
            //#
            Points.Add(new Point(0, 0));
            Points.Add(new Point(0, 1));
            Points.Add(new Point(0, 2));
            Points.Add(new Point(0, 3));
          }
          break;
        case 4:
          {
            //##
            //##
            Points.Add(new Point(0, 0));
            Points.Add(new Point(1, 0));
            Points.Add(new Point(0, 1));
            Points.Add(new Point(1, 1));
          }
          break;
        default:
          throw new Exception($"Invalid block type {type}");
      }
    }

    public void Move(EDirection dir)
    {
      switch (dir)
      {
        case EDirection.UP:
          Location.Y++;
          break;
        case EDirection.DOWN:
          Location.Y--;
          break;
        case EDirection.LEFT:
          Location.X--;
          break;
        case EDirection.RIGHT:
          Location.X++;
          break;
      }
    }

    internal void InitLocation(int height)
    {
      Location = new Point(2, height + 3);
    }
  }

}
