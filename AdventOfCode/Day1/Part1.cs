namespace AdventOfCode.Day1;

public class Part1
{
    private readonly string[] _elvesCalories;

    public Part1()
    {
        string input = File.ReadAllText("Day1/input.txt");
        _elvesCalories = input.Split("\n\n");
    }

    public int Solve()
    {
        return _elvesCalories
            .Select(x => x.Split("\n"))
            .Max(x => x.Sum(int.Parse));
    }

}