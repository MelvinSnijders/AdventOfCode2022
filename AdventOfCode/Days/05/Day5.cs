using AoCHelper;

namespace AdventOfCode.Days._05;

public sealed class Day5 : BaseDay
{
    private readonly string[] _moves;
    private readonly string[] _boxes;

    public Day5()
    {
        string input = File.ReadAllText(InputFilePath);
        string[] parts = input.Split("\n\n");
        _boxes = parts[0].Split("\n");
        _moves = parts[1].Split("\n");
    }

    public override ValueTask<string> Solve_1()
    {
        List<Stack<char>> stacks = new();

        _boxes.Reverse()
            .Skip(1)
            .Select(line =>
                line.Replace("[", string.Empty)
                    .Replace("]", string.Empty)
                    .Split(new[] { "    ", " " }, StringSplitOptions.None))
            .ToList()
            .ForEach(strings =>
                Enumerable.Range(0, _boxes[^1].Trim().Split("   ").Length)
                    .ToList()
                    .ForEach(i =>
                    {
                        if (stacks.Count <= i) stacks.Add(new Stack<char>());
                        if (!string.IsNullOrEmpty(strings[i])) stacks[i].Push(strings[i][0]);
                    })
            );

        _moves.Select(
            move => move.Split(new[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray()
        ).ToList().ForEach(move =>
            Enumerable.Range(0, move[0])
                .ToList()
                .ForEach(_ => stacks[move[2] - 1].Push(
                        stacks[move[1] - 1].Pop()
                    )
                )
        );

        string solution = new string(stacks.Select(stack => stack.Peek()).ToArray());

        return new ValueTask<string>(solution);
    }

    public override ValueTask<string> Solve_2()
    {
        List<Stack<char>> stacks = new();

        _boxes.Reverse()
            .Skip(1)
            .Select(line =>
                line.Replace("[", string.Empty)
                    .Replace("]", string.Empty)
                    .Split(new[] { "    ", " " }, StringSplitOptions.None))
            .ToList()
            .ForEach(strings =>
                Enumerable.Range(0, _boxes[^1].Trim().Split("   ").Length)
                    .ToList()
                    .ForEach(i =>
                    {
                        if (stacks.Count <= i) stacks.Add(new Stack<char>());
                        if (!string.IsNullOrEmpty(strings[i])) stacks[i].Push(strings[i][0]);
                    })
            );

        _moves.Select(
            move => move.Split(new[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray()
        ).ToList().ForEach(move =>
            {
                List<char> popped = new();
                Enumerable.Range(0, move[0])
                    .ToList()
                    .ForEach(_ => popped.Add(stacks[move[1] - 1].Pop()));
                popped.Reverse();
                popped.ForEach(stacks[move[2] - 1].Push);
            }
        );

        string solution = new string(stacks.Select(stack => stack.Peek()).ToArray());

        return new ValueTask<string>(solution);
    }
}