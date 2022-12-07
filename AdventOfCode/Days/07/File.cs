namespace AdventOfCode.Days._07;

public class File : Item
{

    private int Size { get; }

    public File(string name, int size) : base(name)
    {
        Size = size;
    }

    public override int GetSize()
    {
        return Size;
    }
    
}