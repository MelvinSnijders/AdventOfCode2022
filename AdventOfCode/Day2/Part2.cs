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
        int totalPoints = 0;
        foreach (string game in _games)
        {
            string[] strategy = game.Split(" ");
            Strategy opponent = LetterToStrategy(strategy[0]);
            totalPoints += DeterminePoints(opponent, strategy[1]);
        }

        return totalPoints;
    }

    private Strategy LetterToStrategy(string letter)
    {
        switch (letter)
        {
            case "A":
            case "X":
                return Strategy.Rock;
            case "B":
            case "Y":
                return Strategy.Paper;
            case "C":
            case "Z":
                return Strategy.Scissors;
            default:
                return Strategy.Rock;
        }
    }

    private int DeterminePoints(Strategy opponent, string result)
    {
        switch (result)
        {
            case "X":
                // Lose
                if (opponent == Strategy.Paper) return (int)Strategy.Rock;
                if (opponent == Strategy.Rock) return (int)Strategy.Scissors;
                if (opponent == Strategy.Scissors) return (int)Strategy.Paper;
                break;
            case "Y":
                // Draw
                return (int)opponent + 3;
            case "Z":
                // Win
                int points = 6;
                if (opponent == Strategy.Paper) points += (int)Strategy.Scissors;
                if (opponent == Strategy.Rock) points += (int)Strategy.Paper;
                if (opponent == Strategy.Scissors) points += (int)Strategy.Rock;
                return points;
            default:
                return 0;
        }

        return 0;
    }
}