namespace AdventOfCode.Day6;

public class Day6
{
    
    private readonly string _input;
    
    public Day6()
    {
        _input = File.ReadAllText("Day6/input.txt");
    }

    public int Part1()
    {
        return Solve(4);
    }

    public int Part2()
    {
        return Solve(14);
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