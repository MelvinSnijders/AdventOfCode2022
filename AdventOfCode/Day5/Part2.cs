using System.Text;

namespace AdventOfCode.Day5;

public class Part2
{
    private readonly string[] _moves;
    private readonly string[] _boxes;

    public Part2()
    {
        string input = File.ReadAllText("Day5/input.txt");
        string[] parts = input.Split("\n\n");
        _boxes = parts[0].Split("\n");
        _moves = parts[1].Split("\n");
    }

    public string Solve()
    {
        List<Stack<char>> stacks = new List<Stack<char>>();

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
                        if (!String.IsNullOrEmpty(strings[i])) stacks[i].Push(strings[i][0]);
                    })
            );

        _moves.Select(
            move => move.Split(new []{ "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray()
        ).ToList().ForEach(move =>
            {
                List<char> popped = new List<char>();
                Enumerable.Range(0, move[0])
                    .ToList()
                    .ForEach(i => popped.Add(stacks[move[1] - 1].Pop()));
                popped.Reverse();
                popped.ForEach(stacks[move[2] - 1].Push);
            }
        );

        return new string(stacks.Select(stack => stack.Peek()).ToArray());
    }
}