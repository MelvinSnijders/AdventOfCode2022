using System.Text;

namespace AdventOfCode.Day5;

public class Part2
{
    private readonly string[] _moves;

    public Part2()
    {
        string input = File.ReadAllText("Day5/input.txt");
        string[] parts = input.Split("\n\n");
        _moves = parts[1].Split("\n");
    }

    public string Solve()
    {
        // Setup the stacks, too lazy to read from input
        Stack<char>[] stacks = new Stack<char>[9];
        stacks[0] = new Stack<char>(new[] { 'R', 'S', 'L', 'F', 'Q' });
        stacks[1] = new Stack<char>(new[] { 'N', 'Z', 'Q', 'G', 'P', 'T' });
        stacks[2] = new Stack<char>(new[] { 'S', 'M', 'Q', 'B' });
        stacks[3] = new Stack<char>(new[] { 'T', 'G', 'Z', 'J', 'H', 'C', 'B', 'Q' });
        stacks[4] = new Stack<char>(new[] { 'P', 'H', 'M', 'B', 'N', 'F', 'S' });
        stacks[5] = new Stack<char>(new[] { 'P', 'C', 'Q', 'N', 'S', 'L', 'V', 'G' });
        stacks[6] = new Stack<char>(new[] { 'W', 'C', 'F' });
        stacks[7] = new Stack<char>(new[] { 'Q', 'H', 'G', 'Z', 'W', 'V', 'P', 'M' });
        stacks[8] = new Stack<char>(new[] { 'G', 'Z', 'D', 'L', 'C', 'N', 'R' });

        // Loop through the moves     
        foreach (string move in _moves)
        {
            string[] delimiter = { "move ", " from ", " to " };
            int[] moveNumbers = move.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                .ToArray();

            Console.WriteLine($"Move {moveNumbers[0]} from {moveNumbers[1]} to {moveNumbers[2]}");

            List<char> popped = new List<char>();
            for (int i = 0; i < moveNumbers[0]; i++)
            {
                char toMove = stacks[moveNumbers[1] - 1].Pop();
                popped.Add(toMove);
                
            }

            popped.Reverse();
            popped.ForEach(stacks[moveNumbers[2] - 1].Push);

        }

        // Get top of all stacks
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Stack<char> stack in stacks)
        {
            stringBuilder.Append(stack.Peek());
        }

        return stringBuilder.ToString();
    }
}