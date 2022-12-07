using AoCHelper;

namespace AdventOfCode.Days._02;

public sealed class Day2 : BaseDay
{
    private readonly string[] _games;
    private readonly List<Shape> _gameRules;

    public Day2()
    {
        _games = File
            .ReadAllText(InputFilePath)
            .Split("\n");
        _gameRules = InitGameRules();
    }

    private List<Shape> InitGameRules()
    {
        List<Shape> shapes = new();

        Shape rock = new Shape(new[] { 'A', 'X' }, 1);
        Shape paper = new Shape(new[] { 'B', 'Y' }, 2);
        Shape scissors = new Shape(new[] { 'C', 'Z' }, 3);

        rock.Beats = scissors;
        paper.Beats = rock;
        scissors.Beats = paper;

        shapes.Add(rock);
        shapes.Add(paper);
        shapes.Add(scissors);

        return shapes;
    }

    public override ValueTask<string> Solve_1()
    {
        int solution = _games.Select(game =>
        {
            string[] strategy = game.Split(" ");
            Shape player1 = _gameRules.FirstOrDefault(s => s.Letters.ToList().Contains(char.Parse(strategy[0]))) ??
                            throw new FormatException("Invalid shape letter");
            Shape player2 = _gameRules.FirstOrDefault(s => s.Letters.ToList().Contains(char.Parse(strategy[1]))) ??
                            throw new FormatException("Invalid shape letter");

            if (player1 == player2) return player2.Points + 3; // Draw
            if (player2.Beats == player1) return player2.Points + 6; // Win
            return player2.Points; // Lose
        }).Sum();

        return new ValueTask<string>(solution.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int solution = _games.Select(game =>
        {
            string[] strategy = game.Split(" ");
            Shape player1 = _gameRules.FirstOrDefault(s => s.Letters.ToList().Contains(char.Parse(strategy[0]))) ??
                            throw new FormatException("Invalid shape letter");

            Shape toWin = _gameRules.Find(shape => shape.Beats == player1)!;
            switch (strategy[1])
            {
                case "X": // Lose
                    return _gameRules.Find(shape => shape != player1 && shape != toWin)!.Points;
                case "Y": // Draw
                    return player1.Points + 3;
                case "Z": // Win
                    return _gameRules.Find(shape => shape.Beats == player1)!.Points + 6;
            }

            return 0;
        }).Sum();

        return new ValueTask<string>(solution.ToString());
    }
}