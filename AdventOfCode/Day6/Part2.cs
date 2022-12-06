namespace AdventOfCode.Day6;

public class Part2
{

    private readonly string input;
    
    public Part2()
    {
        input = File.ReadAllText("Day6/input.txt");
    }

    public int Solve()
    {
        
        int count = 0;
        string possibleStart = "";
        while (possibleStart.Distinct().Count() != 14)
        {
            possibleStart = input.Substring(count, 14);
            count++;
        }

        Console.WriteLine("Found it!! " + possibleStart);
        
        return count + 13;
        
    }
}