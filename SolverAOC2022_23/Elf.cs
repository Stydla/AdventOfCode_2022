using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_23
{
  public class Elf
  {

    public Field Position { get; set; }

    public List<EDirection> ConsiderOrder = new List<EDirection>();

    public Elf(int x, int y)
    {
      Position = new Field(x, y);
      ConsiderOrder.Add(EDirection.North);
      ConsiderOrder.Add(EDirection.South);
      ConsiderOrder.Add(EDirection.West);
      ConsiderOrder.Add(EDirection.East);
    }

    public void Move(Field position, EDirection direction)
    {
      Position = new Field(position.X, position.Y);
    }

    public Consideration Consider(Map map)
    {

      bool AllEmpty = true;
      foreach(EDirection dir in Enum.GetValues(typeof(EDirection)))
      {
        Field fTmp = Position.GetField(dir);
        if(map.ElvesByPosition.ContainsKey(fTmp))
        {
          AllEmpty = false;
        }
      }
      if(AllEmpty)
      {
        return null;
      }

      Consideration consideration = null;
      for(int i = 0; i < ConsiderOrder.Count; i++)
      {
        EDirection dir = ConsiderOrder[i];
        if(dir == EDirection.North)
        {
          if (!map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.North)) &&
              !map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.NorthWest)) &&
              !map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.NorthEast)))
          {
            consideration = new Consideration(Position.GetField(EDirection.North), this, EDirection.North);
            break;
          }
        } 
        else if (dir == EDirection.South)
        {
          if (!map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.South)) &&
            !map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.SouthWest)) &&
            !map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.SouthEast)))
          {
            consideration = new Consideration(Position.GetField(EDirection.South), this, EDirection.South);
            break;
          } 
        } 
        else if (dir == EDirection.West)
        {
          if (!map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.West)) &&
              !map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.SouthWest)) &&
              !map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.NorthWest)))
          {
            consideration = new Consideration(Position.GetField(EDirection.West), this, EDirection.West);
            break;
          }
        }
        else if(dir == EDirection.East)
        {
          if (!map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.East)) &&
             !map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.SouthEast)) &&
             !map.ElvesByPosition.ContainsKey(Position.GetField(EDirection.NorthEast)))
          {
            consideration = new Consideration(Position.GetField(EDirection.East), this, EDirection.East);
            break;
          }
        } else
        {
          throw new Exception($"Unknow consider state: {dir}");
        }  
      }
      if(consideration == null)
      {
        consideration = new Consideration(null, null, EDirection.East);
      }
      return consideration;
    }

    public override string ToString()
    {
      return Position.ToString();
    }

    internal void UpdateConsiderOrder()
    {
      EDirection dir = ConsiderOrder[0];
      ConsiderOrder.Remove(dir);
      ConsiderOrder.Add(dir);
    }
  }
}
