using AoCHelper;

namespace AdventOfCode.Days._01;

public sealed class Day1 : BaseDay
{
    private readonly string[] _elvesCalories;

    public Day1()
    {
        _elvesCalories = File
            .ReadAllText(InputFilePath)
            .Split("\n\n");
    }

    public override ValueTask<string> Solve_1()
    {
        int solution = _elvesCalories
            .Select(x => x.Split("\n"))
            .Max(x => x.Sum(int.Parse));

        return new ValueTask<string>($"{solution}");
    }

    public override ValueTask<string> Solve_2()
    {
        int solution = _elvesCalories
            .Select(x => x.Split("\n"))
            .Select(x => x.Sum(int.Parse))
            .OrderBy(x => x)
            .TakeLast(3)
            .Sum();

        return new ValueTask<string>(solution.ToString());
    }
}