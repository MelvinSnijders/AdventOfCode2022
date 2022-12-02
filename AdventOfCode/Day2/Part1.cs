namespace AdventOfCode.Day2;

public class Part1
{
    private readonly string[] _games;

    public Part1()
    {
        string input = File.ReadAllText("Day2/input.txt");
        _games = input.Split("\n");
    }

    public int Solve()
    {
        
        List<Shape> shapes = new List<Shape>();

        Shape rock = new Shape(new[] { 'A', 'X' }, 1);
        Shape paper = new Shape(new[] { 'B', 'Y' }, 2);
        Shape scissors = new Shape(new[] { 'C', 'Z' }, 3);
        
        rock.Beats = scissors;
        paper.Beats = rock;
        scissors.Beats = paper;
        
        shapes.Add(rock);
        shapes.Add(paper);
        shapes.Add(scissors);

        return _games.Select(game =>
        {
            string[] strategy = game.Split(" ");
            Shape player1 = shapes.FirstOrDefault(s => s.Letters.ToList().Contains(char.Parse(strategy[0]))) ??
                            throw new FormatException("Invalid shape letter");
            Shape player2 = shapes.FirstOrDefault(s => s.Letters.ToList().Contains(char.Parse(strategy[1]))) ??
                            throw new FormatException("Invalid shape letter");

            if (player1 == player2) return player2.Points + 3; // Draw
            if (player2.Beats == player1) return player2.Points + 6; // Win
            return player2.Points; // Lose

        }).Sum();
        
    }
}