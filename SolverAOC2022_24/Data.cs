using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SolverAOC2022_24
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
      Map firstMap = new Map(inputData);

      return Solve(firstMap.StartField, firstMap.FinishField , firstMap).Round;

    }

   

    internal int Solve2()
    {
      Map current = new Map(inputData);
      int total = 0;

      Field startField = current.StartField;
      Field finishField = current.FinishField;

      current = Solve(startField, finishField, current);
      total += current.Round;

      current = Solve(finishField, startField, current);
      total += current.Round;

      current = Solve(startField, finishField, current);
      total += current.Round;

      return total;
    }

    private Map Solve(Field Start, Field End, Map map)
    {
      Map firstMap = new Map(map);

      //nastavit start/end field
      firstMap.SetStartField(Start);
      firstMap.SetFinishField(End);
      firstMap.Round = 0;

      int totalRound = 0;

      IEnumerable<Field> currentPlayerPositions = new List<Field>() { firstMap.Player.Position };

      HashSet<MapStateData> results = new HashSet<MapStateData>();
      List<Map> allMaps = new List<Map>() { firstMap };

      Map currentMap = firstMap;
      while (true)
      {
        if (currentPlayerPositions.Count() == 0)
        {
          break;
        }

        int nextMapIndex = (currentMap.Round + 1) % currentMap.MapRepeat;
        currentMap = GetNextMap(allMaps, nextMapIndex);
        allMaps.Add(currentMap);

        HashSet<Field> nextPlayerPositions = new HashSet<Field>();

        foreach (Field playerPosition in currentPlayerPositions)
        {
          currentMap.SetPlayerPosition(playerPosition);

          MapStateData msdTmp = new MapStateData(currentMap.Round, currentMap.IsPlayerAtField(currentMap.FinishField), playerPosition, totalRound);

          if (!results.Contains(msdTmp))
          {
            results.Add(msdTmp);
          }
          else
          {
            continue;
          }

          List<Field> tmpPlayerPositions = currentMap.GetNextPlayerPositions();
          foreach (Field f in tmpPlayerPositions)
          {
            if (!nextPlayerPositions.Contains(f))
            {

              nextPlayerPositions.Add(f);


            }
          }
        }
        currentPlayerPositions = nextPlayerPositions;
        totalRound++;
      }

      int res = results.Where(x => x.IsEnd).Min(x => x.TotalRound);
      allMaps[res].Round = res;
      return allMaps[res];
    }

    private Map GetNextMap(List<Map> allMaps, int index)
    {
      if(allMaps.Count <= index)
      {
        Map tmpMap = new Map(allMaps[index - 1]);
        tmpMap.Round++;
        tmpMap.SimulateBlizzards();
        return tmpMap;
      }
      return allMaps[index];
    }
  }
}