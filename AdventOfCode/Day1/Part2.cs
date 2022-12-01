namespace AdventOfCode.Day1;

public class Part2
{

    static string input = File.ReadAllText("Day1/input.txt");
    static string[] elvesCalories = input.Split("\n\n");

    public int solve()
    {
        return elvesCalories
            .Select(x => x.Split("\n"))
            .Select(x => x.Sum(int.Parse))
            .OrderBy(x => x)
            .TakeLast(3)
            .Sum();
    }

}
   