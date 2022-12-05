using System.Text;

namespace AdventOfCode.Day5;

public class Part1
{
    private readonly string[] _moves;

    public Part1()
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
        
         _moves.Select(
                move => move.Split(new []{ "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray()
            ).ToList().ForEach(move => 
             Enumerable.Range(0, move[0])
                 .ToList()
                 .ForEach(i => stacks[move[2] - 1].Push(
                     stacks[move[1] - 1].Pop()
                     )
                 )
             );

         return new string(stacks.Select(stack => stack.Peek()).ToArray());
  
    }
}