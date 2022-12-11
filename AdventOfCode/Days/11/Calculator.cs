namespace AdventOfCode.Days._11;

public class Calculator
{
    private long Left { get; }
    private char Operation { get; }
    private long Right { get; }


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
            '*' => Left * Right,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
}