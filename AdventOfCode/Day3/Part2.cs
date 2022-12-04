namespace AdventOfCode.Day3;

public class Part2
{
    private readonly string[] _rucksacks;

    public Part2()
    {
        string input = File.ReadAllText("Day3/input.txt");
        _rucksacks = input.Split("\n");
    }

    public int Solve()
    {

        return _rucksacks
            .Chunk(3)
            .Select(group => 
                group[0].Intersect(group[1]).Intersect(group[2])
                    .First())
            .Select(ch => Char.IsUpper(ch) ? ch - 38 : ch - 96)
            .Sum();

    }
}