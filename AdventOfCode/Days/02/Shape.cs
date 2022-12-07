namespace AdventOfCode.Days._02;

public class Shape
{
    public char[] Letters { get; }
    public Shape? Beats { get; set; }
    public int Points { get; }

    public Shape(char[] letters, int points)
    {
        Letters = letters;
        Points = points;
    }
}