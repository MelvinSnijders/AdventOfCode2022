namespace AdventOfCode.Days._11;

public class Calculator
{
    public long Left { get; }
    public char Operation { get; }
    public long Right { get; }


    public Calculator(long left, char operation, long right)
    {
        Left = left;
        Operation = operation;
        Right = right;
    }
    
    public long Calculate()
    {
        return Operation switch
        {
            '+' => Left + Right,
            '-' => Left - Right,
            '*' => Left * Right,
            '/' => Left / Right,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
}