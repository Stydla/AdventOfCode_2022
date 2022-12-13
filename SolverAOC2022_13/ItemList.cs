using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_13
{
  internal class ItemList : Item
  {

    public List<Item> Items { get; set; } = new List<Item>();

    public ItemList(List<string> line, ref int index)
    {

      while(index < line.Count)
      {
        string sTmp = line[index];
        if(sTmp == "[")
        {
          index++;
          ItemList il = new ItemList(line, ref index);
          Items.Add(il);
        }
        else if(sTmp == "]")
        {
          return;
        } else if (sTmp == ",")
        {

        } else
        {
          int val = int.Parse(sTmp);
          Items.Add(new ItemInt(val));
        }
        index++;

      }
      
    }

    public ItemList(ItemInt itemInt)
    {
      Items.Add(itemInt);
    }

    public int Compare(ItemInt it2)
    {
      return this.Compare(new ItemList(it2));
      
    }

    public int Compare(ItemList il2)
    {
      
      for (int i = 0; i < Items.Count && i < il2.Items.Count; i++)
      {
        Item it1 = Items[i];
        Item it2 = il2.Items[i];
        int cmpRes = it1.Compare(it2);
        if (cmpRes != 0)
        {
          return cmpRes;
        }
      }

      if(Items.Count == il2.Items.Count)
      {
        return 0;
      } else if (Items.Count < il2.Items.Count)
      {
        return 1;
      } else
      {
        return -1;
      }
    }

    public override int Compare(Item item)
    {
      if(item is ItemInt ii)
      {
        return Compare(ii);
      } else if(item is ItemList il)
      {
        return Compare(il);
      }
      throw new Exception("invalit item");
    }
  }
}
