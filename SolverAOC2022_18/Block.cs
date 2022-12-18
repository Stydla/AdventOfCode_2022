using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_18
{
  internal class Block
  {
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public List<Block> Neightbours { get; set; } = new List<Block>();

    public EType Type { get; set; } = EType.Unknown;

    public Block(int x, int y, int z)
    {
      X = x;
      Y = y;
      Z = z;
    }

    public Block(string input)
    {
      string[] coords = input.Split(',');
      X = int.Parse(coords[0]);
      Y = int.Parse(coords[1]);
      Z = int.Parse(coords[2]);
    }

    public void AddNeighbour(Block neighbour)
    {
      Neightbours.Add(neighbour);
    }

    public int EmptySideCount()
    {
      return Neightbours.Where(x=>x.Type== EType.Unknown).Count();
    }

    public static int GetHash(int x, int y, int z)
    {
      return x * 256 * 256 + y * 256 + z + 256*256*256;
    }

    internal static int GetHash(Block b)
    {
      return GetHash(b.X, b.Y, b.Z);
    }

    internal int WaterSideCount()
    {
      return Neightbours.Where(x => x.Type == EType.Water).Count();
    }
  }
}
