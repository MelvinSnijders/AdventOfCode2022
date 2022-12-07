namespace AdventOfCode.Day7;

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