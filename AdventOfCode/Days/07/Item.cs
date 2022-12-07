namespace AdventOfCode.Days._07;

public abstract class Item
{
    
    public string Name { get; }
    public Folder? Parent { get; set; }
    
    protected Item(string name)
    {
        Name = name;
    }

    public abstract int GetSize();

}