namespace AdventOfCode.Day1;

public class Part1
{

    static string input = File.ReadAllText("Day1/input.txt");
    static string[] elvesCalories = input.Split("\n\n");

    public int solve()
    {
        return elvesCalories
            .Select(x => x.Split("\n"))
            .Max(x => x.Sum(int.Parse));
    }

}