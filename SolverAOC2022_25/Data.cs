using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Text;

namespace SolverAOC2022_25
{
  internal class Data
  {
    private string inputData;

    public Data(string inputData)
    {
      this.inputData = inputData;
    }

    internal string Solve1()
    { 
     long total = 0;
     using (StringReader sr = new StringReader(inputData))
      {
       
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          long val = Decode(line);
          total += val;
        }
      }
      string res = Encode(total);
    
      return res;
    }

    internal int Solve2()
    {
      throw new NotImplementedException();
    }

    private string Encode(long dec)
    {
      List<char> vals = new List<char>();

      while(dec > 2)
      {
        long modRes = dec % 5;
        if(modRes > 2)
        {
          modRes = modRes  - 5;
          dec += 5;
        }
        dec /= 5;

        char c = DecToEnc[modRes];

        vals.Insert(0, c);

      }
      if(dec + 2 > 0)
      {
        char first = DecToEnc[(dec + 2) % 5 -2];
        vals.Insert(0, first);
      }
      
      

      return string.Join("", vals);
    }

    private Dictionary<char, long> EncToDec { get; set; } = new Dictionary<char, long>()
    {
      { '2', 2 },
      { '1', 1 },
      { '0', 0 },
      { '-', -1 },
      { '=', -2 }
    };

    private Dictionary<long, char> DecToEnc { get; set; } = new Dictionary<long, char>()
    {
      { 2, '2' },
      { 1, '1' },
      { 0, '0' },
      { -1, '-' },
      { -2, '=' }
    };

    private long Decode(string encoded)
    {
      long res = 0;
      int exp = 0;

      for(int i = encoded.Length - 1; i >= 0; i--)
      {
        char c = encoded[i];

        long pow = 1;
        for(int j = 0; j < exp; j++)
        {
          pow *= 5;
        }

        long val = EncToDec[c];
        res = res + val * pow;

        exp++;
      }
      return res;
    }


  }
}