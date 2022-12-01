namespace AdventOfCode.Day1;

public class Part2
{
    private static readonly string Input = File.ReadAllText("Day1/input.txt");
    private static readonly string[] ElvesCalories = Input.Split("\n\n");

    public int solve()
    {
        return ElvesCalories
            .Select(x => x.Split("\n"))
            .Select(x => x.Sum(int.Parse))
            .OrderBy(x => x)
            .TakeLast(3)
            .Sum();
    }

}
   