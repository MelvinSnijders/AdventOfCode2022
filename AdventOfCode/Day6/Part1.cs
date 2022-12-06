namespace AdventOfCode.Day6;

public class Part1
{

    private readonly string input;
    
    public Part1()
    {
        input = File.ReadAllText("Day6/input.txt");
    }

    public int Solve()
    {
        
        int count = 0;
        string possibleStart = "";
        while (possibleStart.Distinct().Count() != 4)
        {
            possibleStart = input.Substring(count, 4);
            count++;
        }

        Console.WriteLine("Found it!! " + possibleStart);
        
        return count + 3;
        
    }
}