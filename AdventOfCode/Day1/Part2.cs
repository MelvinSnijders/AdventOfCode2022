namespace AdventOfCode.Day1;

public class Part2
{
    private readonly string[] _elvesCalories;

    public Part2()
    {
        string input = File.ReadAllText("Day1/input.txt");
        _elvesCalories = input.Split("\n\n");
    }

    public int Solve()
    {
        return _elvesCalories
            .Select(x => x.Split("\n"))
            .Select(x => x.Sum(int.Parse))
            .OrderBy(x => x)
            .TakeLast(3)
            .Sum();
    }

}
   