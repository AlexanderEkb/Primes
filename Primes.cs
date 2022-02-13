using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primes

{
  class Primes : ITask
  {
    const long ArraySize = 50000000;
    private UInt32[] Buffer = new UInt32[ArraySize];
    public string Run(string[] data)
    {
      int N = Convert.ToInt32(data[0]);
      long answer = Eratosphene(N);
      return answer.ToString();
    }

    public long Eratosphene(long N)
    {
      if(N < 2)
        return 0;
      else if(N == 2)
        return 1;
      else
      {
        int ActualCount = IsEven((int)N)?((int)N - 1):(int)N;
        int answer = 2;
        int LastFound = 3;
        bool ExitCondition = false;
        ResetArray(N);
        while(!ExitCondition)
        {
          int From = LastFound * 3;
          int Step = LastFound * 2;
          for(int i=From;i<=ActualCount;i+=Step)
          {
            MarkNumber(i);
            // Console.WriteLine($"Marking {i}");
          }

          bool NewPrimeFound = false;
          for(int i=LastFound+2; i<=ActualCount; i+= 2)
          {
            NewPrimeFound = IsntMarked(i);
            if(NewPrimeFound)
            {
              answer++;
              LastFound = i;
              // Console.WriteLine($"Fonnd {i}, answer will be {answer}");
              break;
            }
          }
          if(!NewPrimeFound)
          {
            ExitCondition = true;
          }
        }
        return answer;
      }
    }

    public bool IsEven(int N)
    {
      return (N % 2) == 0;
    }
    public void ResetArray(long N)
    {
      const long WordSize = sizeof(UInt32);
      bool IsOdd          = ((N % 2) != 0);
      long OddCount       = IsOdd?(N/2+1):(N/2);
      bool AddExtraWord   = ((OddCount % WordSize) != 0);
      long WordCount      = AddExtraWord?(OddCount / WordSize + 1):(OddCount / WordSize);

      for(long i=0; i<WordCount; i++)
      {
        Buffer[i] = 0;
      }
    }

    void MarkNumber(long N)
    {
      if((N % 2) == 0)
        return;

      int Half      = (int)N / 2;
      int Index     = Half / sizeof(UInt32);
      int Offset    = (Half % sizeof(UInt32));
      UInt32 Mask   = 1U << Offset;
      
      Buffer[Index] |= Mask;
    }

    bool IsntMarked(int N)
    {
      if((N % 2) == 0)
        return false;
      else
      {
        int Half          = (int)N / 2;
        int Index         = Half / sizeof(UInt32);
        int Offset        = (Half % sizeof(UInt32));
        UInt32 Mask       = 1U << Offset;
        UInt32 BufferItem = Buffer[Index];

        return (Mask & BufferItem) == 0;
      }
    }
  }
}
