namespace Primes

{
  internal class Tester
  {
    private ITask task;
    private string path;
    private string[] data = {};
    private string expect = "";
    private string actual = "";
    public Tester(ITask task, string path)
    {
      this.task = task;
      this.path = path;
    }
    public void RunAllTests()
    {
      int count = 0;
      while(true)
      {
        bool result = RunTest(count);
        if(!result)
        {
          break;
        }
        count++;
      }
    }

    public bool RunTest(int count)
    {
      string inFile = $"{path}test.{count}.in";
      string outFile = $"{path}test.{count}.out";
      if(!File.Exists(inFile) || !File.Exists(outFile))
      {
        return false;
      }
      RunTest(inFile, outFile, count);
      return true;
    }
    bool RunTest(string inFile, string outFile, int count)
    {
      try
      {
        data = File.ReadAllLines(inFile);
        expect = File.ReadAllText(outFile).Trim();
        actual = task.Run(data).Trim();
        bool result = (actual == expect);
        if(result)
        {
          Console.WriteLine($"Test #{count} - PASS");
        }
        else
        {
          Console.WriteLine($"Test #{count} - FAIL (" + expect.Trim() + " != " + actual + ")");
        }
        return actual == expect;
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return false;
      }
    }
  }
}