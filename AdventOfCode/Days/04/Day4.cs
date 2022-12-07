using AoCHelper;

namespace AdventOfCode.Days._04;

public sealed class Day4 : BaseDay
{
    private readonly string[] _assignmentPairs;

    public Day4()
    {
        _assignmentPairs = File
            .ReadAllText(InputFilePath)
            .Split("\n");
    }

    private (int, int, int, int) ParsePair(string pair)
    {
        string[] pairs = pair.Split(",");

        string sectionsAssigned1 = pairs[0];
        string sectionsAssigned2 = pairs[1];

        string[] sections1 = sectionsAssigned1.Split('-');
        int from1 = int.Parse(sections1[0]);
        int to1 = int.Parse(sections1[1]);

        string[] sections2 = sectionsAssigned2.Split('-');
        int from2 = int.Parse(sections2[0]);
        int to2 = int.Parse(sections2[1]);

        return (from1, to1, from2, to2);
    }

    public override ValueTask<string> Solve_1()
    {
        int solution = _assignmentPairs
            .Select(ParsePair)
            .Where(pairs =>
            {
                (int from1, int to1, int from2, int to2) = pairs;
                return from2 <= from1 && to2 >= to1 || from1 <= from2 && to1 >= to2;
            }).Count();

        return new ValueTask<string>(solution.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int solution = _assignmentPairs
            .Select(ParsePair)
            .Where(pairs =>
            {
                (int from1, int to1, int from2, int to2) = pairs;
                return from1 <= to2 && from2 <= to1;
            }).Count();

        return new ValueTask<string>(solution.ToString());
    }
}