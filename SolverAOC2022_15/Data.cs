using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SolverAOC2022_15
{
  internal class Data
  {
    private string inputData;

    public long Y_Line { get; set; }

    public List<Sensor> Sensors = new List<Sensor>();

    public List<Point> UniqueBeacons = new List<Point>();

    public Data(string inputData)
    {
      this.inputData = inputData;

      using (StringReader sr = new StringReader(inputData))
      {
        Y_Line = long.Parse(sr.ReadLine());

        string line;
        while ((line = sr.ReadLine()) != null)
        {
          Sensor s = new Sensor(line);
          Sensors.Add(s);
        }
      }

      foreach(Sensor s in Sensors)
      {
        if(!UniqueBeacons.Any(x=>x.X == s.BeaconLocation.X && x.Y == s.BeaconLocation.Y))
        {
          UniqueBeacons.Add(s.BeaconLocation);
        }
      }

    }

    internal long Solve1()
    {
      List<Interval> intervals = GetIntervalsFor(Y_Line);

      long res = 0;
      foreach(Interval interval in intervals)
      {
        res += interval.GetLength();
      }

      foreach(Sensor s in Sensors.Where(x => x.Location.Y == Y_Line))
      {
        if(IsInIntevals(intervals, s.Location))
        {
          res--;
        }
      }
      foreach (Point p in UniqueBeacons.Where(x => x.Y == Y_Line))
      {
        if (IsInIntevals(intervals, p))
        {
          res--;
        }
      }
      
      return res;
    }

    private bool IsInIntevals(List<Interval> intervals, Point p)
    {
      foreach(Interval interval in intervals)
      {
        if(interval.ContainsX(p))
        {
          return true;
        }
      }
      return false;
    }

    private List<Interval> GetIntervalsFor(long y)
    {

      List<Interval> intervals = new List<Interval>();
      foreach(Sensor s in Sensors)
      {
        Interval interval = s.GetInterval(y);
        if(interval != null)
        {
          intervals.Add(interval);
        }
      }

      return MergeIntervals(intervals);

    }

    private List<Interval> MergeIntervals(List<Interval> intervals)
    {
      List<IntervalPoint> ips = new List<IntervalPoint>();
      foreach(Interval interval in intervals)
      {
        ips.AddRange(interval.Points);
      }
      ips.Sort((x, y) =>
      {
        if (x.Value < y.Value)
        {
          return -1;
        } else if(x.Value > y.Value)
        {
          return 1;
        } else
        {
          if(x.Type == EIntervalPointType.Start)
          {
            if(y.Type == EIntervalPointType.Start)
            {
              return 0;
            }else
            {
              return -1;
            }
          } else
          {
            if (y.Type == EIntervalPointType.Start)
            {
              return 1;
            }
            else
            {
              return 0;
            }
          }
        }
      });

      List<Interval> ret = new List<Interval>();

      int depth = 0;
      long start = long.MinValue;
      long end = long.MaxValue;
      foreach(IntervalPoint ip in ips)
      {
        if(depth == 0)
        {
          start = ip.Value;
        }
        if(ip.Type == EIntervalPointType.Start)
        {
          depth++;
        } else
        {
          depth--;
        }
        if(depth == 0)
        {
          end = ip.Value;
          ret.Add(new Interval(start, end));
        }

      }

      return ret;
    }


    internal long Solve2()
    {
      throw new NotImplementedException();
    }
  }
}
