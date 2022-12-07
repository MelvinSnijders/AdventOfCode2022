namespace AdventOfCode.Day7;

public class Folder : Item
{

    public List<Item> Items { get; }
    
    public Folder(string name) : base(name)
    {
        Items = new List<Item>();
    }
    
    public override int GetSize()
    {
        return Items.Sum(i => i.GetSize());
    }

    public void AddItem(Item item)
    {
        item.Parent = this;
        Items.Add(item);
    }
    
    public Item? SearchItem(string name)
    {
        return Items.FirstOrDefault(item => item.Name.Equals(name));
    }


}