using System;

namespace Primes
{
    class Program
    {
        static void Main(string[] args)
        {
          ITask primes = new Primes();
          Tester tester = new Tester(primes, @".\tests\");
          tester.RunAllTests();
          // tester.RunTest(6);
        }
    }
}
