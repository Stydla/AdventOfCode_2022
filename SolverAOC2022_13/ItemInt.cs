using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_13
{
  internal class ItemInt : Item
  {
    

    public int Value { get; set; }

    public ItemInt(int val)
    {
      Value = val;
    }

    public int Compare(ItemInt it2)
    {
      return it2.Value - this.Value;

    }

    public int Compare(ItemList il2)
    {
      return new ItemList(this).Compare(il2);
    }

    public override int Compare(Item item)
    {
      if (item is ItemInt ii)
      {
        return Compare(ii);
      }
      else if (item is ItemList il)
      {
        return Compare(il);
      }
      throw new Exception("invalit item");
    }
  }
}
