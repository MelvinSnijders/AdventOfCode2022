using AoCHelper;

namespace AdventOfCode.Days._03;

public sealed class Day3 : BaseDay
{
    private readonly string[] _rucksacks;

    public Day3()
    {
        _rucksacks = File
            .ReadAllText(InputFilePath)
            .Split("\n");
    }

    public override ValueTask<string> Solve_1()
    {
        int solution = _rucksacks.Select(rucksack =>
                rucksack[..(rucksack.Length / 2)]
                    .Intersect(rucksack[(rucksack.Length / 2)..])
                    .First())
            .Select(ch => char.IsUpper(ch) ? ch - 38 : ch - 96)
            .Sum();

        return new ValueTask<string>(solution.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int solution = _rucksacks
            .Chunk(3)
            .Select(group =>
                group[0].Intersect(group[1]).Intersect(group[2])
                    .First())
            .Select(ch => char.IsUpper(ch) ? ch - 38 : ch - 96)
            .Sum();

        return new ValueTask<string>(solution.ToString());
    }
}