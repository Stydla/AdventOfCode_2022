using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolverAOC2022_18
{
  internal class Data
  {
    private string inputData;

    public Dictionary<int, Block> CubeBlocks = new Dictionary<int, Block>();
    public Dictionary<int, Block> AllBlocks = new Dictionary<int, Block>();

    private int MinX { get; set; }
    private int MaxX { get; set; }
    private int MinY { get; set; }
    private int MaxY { get; set; }
    private int MinZ { get; set; }
    private int MaxZ { get; set; }


    public Data(string inputData)
    {
      this.inputData = inputData;

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          Block b = new Block(line);
          b.Type = EType.Cube;
          int hash = Block.GetHash(b);
          CubeBlocks.Add(hash, b);
          AllBlocks.Add(hash, b);
        }
      }

      MinX = CubeBlocks.Values.Min(x => x.X) - 1;
      MaxX = CubeBlocks.Values.Max(x => x.X) + 1;
      MinY = CubeBlocks.Values.Min(x => x.Y) - 1;
      MaxY = CubeBlocks.Values.Max(x => x.Y) + 1;
      MinZ = CubeBlocks.Values.Min(x => x.Z) - 1;
      MaxZ = CubeBlocks.Values.Max(x => x.Z) + 1;

      for(int i = MinX; i <= MaxX; i++)
      {
        for(int j = MinY; j <= MaxY; j++)
        {
          for(int k = MinZ; k <= MaxZ; k++)
          {
            Block b = GetOrCreateBlock(i, j, k);
          }
        }
      }

      InitNeighbours();
    }

    private Block GetOrCreateBlock(int x, int y, int z)
    {
      int hash = Block.GetHash(x, y, z);
      if (AllBlocks.ContainsKey(hash))
      {
        return AllBlocks[hash];
      }

      Block b = new Block(x, y, z);
      AllBlocks.Add(hash, b);
      return b;
    }

    private void InitNeighbours()
    {
      foreach(Block b in AllBlocks.Values)
      {
        List<Block> neighbours = GetNeighbours(b);
        foreach(Block neighbour in neighbours)
        {
          b.AddNeighbour(neighbour);
        }
      }
    }

    private List<Block> GetNeighbours(Block b)
    {
      List<Block> ret = new List<Block>();
      List<int> neighboursHashes = GetNeighboursHashes(b);
      foreach (int hash in neighboursHashes)
      {
        if(AllBlocks.ContainsKey(hash))
        {
          ret.Add(AllBlocks[hash]);
        }
      }
      return ret;
    }

    private List<int> GetNeighboursHashes(Block b)
    {
      List<int> ret = new List<int>();

      ret.Add(Block.GetHash(b.X - 1, b.Y, b.Z));
      ret.Add(Block.GetHash(b.X + 1, b.Y, b.Z));
      ret.Add(Block.GetHash(b.X, b.Y - 1, b.Z));
      ret.Add(Block.GetHash(b.X, b.Y + 1, b.Z));
      ret.Add(Block.GetHash(b.X, b.Y, b.Z - 1));
      ret.Add(Block.GetHash(b.X, b.Y, b.Z + 1));

      return ret;

    }



    internal int Solve1()
    {
      int res = 0;
      foreach(Block b in CubeBlocks.Values)
      {
        res += b.EmptySideCount();
      }
      return res;
    }

    internal int Solve2()
    {
      int startWaterBlockHash = Block.GetHash(MinX, MinY, MinZ);
      Block startWaterBlock = AllBlocks[startWaterBlockHash];
      SpreadWater(startWaterBlock);

      int res = 0;
      foreach (Block b in CubeBlocks.Values)
      {
        res += b.WaterSideCount();
      }
      return res;
    }

    private void SpreadWater(Block b)
    {
      b.Type = EType.Water;
      foreach(Block nextBlock in  b.Neightbours.Where(x=>x.Type == EType.Unknown))
      {
        SpreadWater(nextBlock);
      }
    }
  }
}