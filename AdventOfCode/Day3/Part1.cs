namespace AdventOfCode.Day3;

public class Part1
{
    private readonly string[] _rucksacks;

    public Part1()
    {
        string input = File.ReadAllText("Day3/input.txt");
        _rucksacks = input.Split("\n");
    }

    public int Solve()
    {

        return _rucksacks.Select(rucksack =>
                rucksack.Substring(0, rucksack.Length / 2)
                    .Intersect(rucksack.Substring(rucksack.Length / 2))
                    .First())
            .Select(ch => Char.IsUpper(ch) ? ch - 38 : ch - 96)
            .Sum();

    }
}