using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolverAOC2022_13
{
  internal class Data
  {
    private string inputData;

    public Data(string inputData)
    {
      this.inputData = inputData;
    }

    internal int Solve1()
    {
      int result = 0;

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        int resIndex = 1;
        while ((line = sr.ReadLine()) != null)
        {
          List<string> data = LoadSimple(line);
          Packet p1 =  new Packet(data,line);

          line = sr.ReadLine();
          data = LoadSimple(line);
          Packet p2 = new Packet(data, line);
          line = sr.ReadLine();

          if(p1.Compare(p2))
          {
            result += resIndex;
          }
         
          resIndex++;
        }
      }

      return result;
    }

    private List<string> LoadSimple(string line)
    {
      List<string> res = new List<string>();
      MatchCollection mc = Regex.Matches(line, "(\\d+)|(\\[)|(\\])|(,)");
      foreach(Match m in mc)
      {
        res.Add(m.Value);
      }
      return res;
    }

    internal int Solve2()
    {

      List<Packet> Packets = new List<Packet>();

      using (StringReader sr = new StringReader(inputData))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          List<string> data = LoadSimple(line);
          Packet p1 = new Packet(data, line);

          line = sr.ReadLine();
          data = LoadSimple(line);
          Packet p2 = new Packet(data, line);
          line = sr.ReadLine();

          Packets.Add(p1);
          Packets.Add(p2);
        }
      }

      Packets.Add(new Packet(LoadSimple("[[2]]"), "[[2]]") { IsDividerPacket = true});
      Packets.Add(new Packet(LoadSimple("[[6]]"), "[[2]]") { IsDividerPacket = true });

      SortPackets(Packets);

      //StringBuilder sb = new StringBuilder();
      //foreach(Packet p in Packets)
      //{
      //  sb.AppendLine(p.Line);
      //}
      //Console.WriteLine(sb.ToString());
      //Console.WriteLine();
      //Console.WriteLine();

      var ps = Packets.Where(x => x.IsDividerPacket).ToList();
      int val1 = Packets.IndexOf(ps[0]) + 1;
      int val2 = Packets.IndexOf(ps[1]) + 1;

      return val1 * val2;
    }

    private void SortPackets(List<Packet> Packets)
    {
      Packets.Sort((x, y) => -x.Items.Compare(y.Items));
    }
  }
}
