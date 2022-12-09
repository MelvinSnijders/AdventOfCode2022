using AoCHelper;

namespace AdventOfCode.Days._09;

public sealed class Day9 : BaseDay
{
    private readonly string[][] _moves;

    public Day9()
    {
        _moves = File.ReadAllText(InputFilePath)
            .Split("\n")
            .Select(line => line.Split(" ")
                .ToArray())
            .ToArray();
    }

    public override ValueTask<string> Solve_1()
    {

        HashSet<(int, int)> visited = CalculateVisited(_moves, 2);
        return new ValueTask<string>(visited.Count.ToString());
        
    }
    
    public override ValueTask<string> Solve_2()
    {
        HashSet<(int, int)> visited = CalculateVisited(_moves, 10);
        return new ValueTask<string>(visited.Count.ToString());
    }

    private HashSet<(int, int)> CalculateVisited(string[][] moves, int knots)
    {
        (int x, int y)[] positions = Enumerable.Repeat((0, 0), knots).ToArray();
        HashSet<(int x, int y)> visitedPositions = new();

        (string direction, int amount)[] parsedMoves = moves.Select(move => (move[0], int.Parse(move[1]))).ToArray();
        
        foreach ((string direction, int amount) in parsedMoves)
        {
            for (int i = 0; i < amount; i++)
            {
                Move(positions, direction);
                visitedPositions.Add(positions.Last());
            }

        }

        return visitedPositions;

    }

    private void Move((int x, int y)[] positions, string direction)
    {
        // Move the head
        positions[0] = direction switch
        {
            "U" => (positions[0].x, positions[0].y + 1),
            "D" => (positions[0].x, positions[0].y - 1),
            "L" => (positions[0].x - 1, positions[0].y),
            "R" => (positions[0].x + 1, positions[0].y),
            _ => positions[0]
        };

        // Move the tails: Skip first, only move knots in rope
        for (var i = 1; i < positions.Length; i++)
        {
            var xDiff = positions[i - 1].x - positions[i].x; 
            var yDiff = positions[i - 1].y - positions[i].y;
            
            if (Math.Abs(xDiff) <= 1 && Math.Abs(yDiff) <= 1) {
                // No move is necessary
                continue;
            }
            
            positions[i] = (positions[i].x + xDiff.CompareTo(0), positions[i].y + yDiff.CompareTo(0));
            
        }
    }
    

}