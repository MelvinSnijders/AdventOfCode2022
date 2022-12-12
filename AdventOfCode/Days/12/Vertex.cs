namespace AdventOfCode.Days._12;

public class Vertex
{
    public char Name { get; }
    public LinkedList<Edge> Adjacent { get; }
    public int Distance { get; set; }
    public Vertex? Previous { get; set; }

    public Vertex(char name)
    {
        Name = name;
        Adjacent = new LinkedList<Edge>();
        Distance = int.MaxValue;
    }

    public void Reset()
    {
        Distance = int.MaxValue;
        Previous = null;
    }
    
}