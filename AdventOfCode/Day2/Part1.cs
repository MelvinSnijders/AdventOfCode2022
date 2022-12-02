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
        int totalPoints = 0;
        foreach (string game in _games)
        {
            string[] strategy = game.Split(" ");
            Strategy opponent = LetterToStrategy(strategy[0]);
            Strategy player = LetterToStrategy(strategy[1]);

            Console.WriteLine(opponent + " v " + player);
            
            totalPoints += (int) player;
            totalPoints += DeterminePoints(player, opponent);
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

    private int DeterminePoints(Strategy player, Strategy opponent)
    {
        if (player == opponent)
        {
            return 3;
        }

        switch (player)
        {
            case Strategy.Rock when opponent == Strategy.Scissors:
            case Strategy.Paper when opponent == Strategy.Rock:
            case Strategy.Scissors when opponent == Strategy.Paper:
                return 6;
            default:
                return 0;
        }
    }
}