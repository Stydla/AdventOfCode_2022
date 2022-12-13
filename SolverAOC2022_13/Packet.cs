using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_13
{
  internal class Packet
  {

    public bool IsDividerPacket { get; set;} = false;
    public ItemList Items { get; set; }

    public Packet(List<string> line)
    {
      int index = 1;
      Items = new ItemList(line, ref index);

    }

    internal bool Compare(Packet p2)
    {
      return Items.Compare(p2.Items) >= 0;
    }
  }
}
