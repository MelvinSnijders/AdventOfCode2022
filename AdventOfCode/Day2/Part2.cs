namespace AdventOfCode.Day2;

public class Part2
{
    private readonly string[] _games;

    public Part2()
    {
        string input = File.ReadAllText("Day2/input.txt");
        _games = input.Split("\n");
    }

    public int Solve()
    {
        
        List<Shape> shapes = new List<Shape>();

        Shape rock = new Shape(new[] { 'A' }, 1);
        Shape paper = new Shape(new[] { 'B', }, 2);
        Shape scissors = new Shape(new[] { 'C' }, 3);
        
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

            Shape toWin = shapes.Find(shape => shape.Beats == player1)!;
            switch (strategy[1])
            {
                case "X": // Lose
                    return shapes.Find(shape => shape != player1 && shape != toWin)!.Points;
                case "Y": // Draw
                    return player1.Points + 3;
                case "Z": // Win
                    return shapes.Find(shape => shape.Beats == player1)!.Points + 6;
            }

            return 0;

        }).Sum();
    }
    
}