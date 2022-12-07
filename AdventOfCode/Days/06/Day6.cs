using AoCHelper;

namespace AdventOfCode.Days._06;

public sealed class Day6 : BaseDay
{
    
    private readonly string _input;
    
    public Day6()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        int solution = Solve(4);
        return new ValueTask<string>(solution.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int solution = Solve(14);
        return new ValueTask<string>(solution.ToString());
    }
    private int Solve(int packetSize)
    {

        int count = 0;
        string possibleStart = "";
        while (possibleStart.Distinct().Count() != packetSize)
        {
            possibleStart = _input.Substring(count, packetSize);
            count++;
        }
        
        return count + (packetSize - 1);
        
    }
    
}