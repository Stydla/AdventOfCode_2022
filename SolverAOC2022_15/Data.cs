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
    public long MaxSize { get; set; }

    public List<Sensor> Sensors = new List<Sensor>();

    public List<Point> UniqueBeacons = new List<Point>();

    public Data(string inputData)
    {
      this.inputData = inputData;

      using (StringReader sr = new StringReader(inputData))
      {
        Y_Line = long.Parse(sr.ReadLine());
        MaxSize = long.Parse(sr.ReadLine());

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
        long val1 = x.Type == EIntervalPointType.Start ? x.Value : x.Value + 1;
        long val2 = y.Type == EIntervalPointType.Start ? y.Value : y.Value + 1;
        if (val1 < val2)
        {
          return -1;
        } else if(val1 > val2)
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

      long y = long.MinValue;
      List<Interval> intervalsRes = new List<Interval>();

      Parallel.For(0, MaxSize, (i, loopstate) =>
      {
        List<Interval> intervals = GetIntervalsFor(i).Where(xx => xx.Points[1].Value >= 0).ToList();

        if (intervals.Count > 1)
        {
          y = i;
          intervalsRes = intervals;
          loopstate.Break();
        }
      });
    
      long x = intervalsRes[0].Points[1].Value + 1;

      return x * 4000000 + y;
    }
  }
}
