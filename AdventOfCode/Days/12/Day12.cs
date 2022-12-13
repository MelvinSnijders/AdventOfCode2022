using AoCHelper;

namespace AdventOfCode.Days._12;

public sealed class Day12 : BaseDay
{
    private readonly char[][] _unparsed;
    private readonly Dictionary<(int, int), Vertex> _vertices = new();

    public Day12()
    {
        string unparsedGraph = File.ReadAllText(InputFilePath);
        _unparsed = unparsedGraph.Split("\n").Select(line => line.ToCharArray()).ToArray();
        ParseGraph();
    }

    private void ParseGraph()
    {
        for (int y = 0; y < _unparsed.Length; y++)
        {
            for (int x = 0; x < _unparsed[y].Length; x++)
            {
                char c = _unparsed[y][x];
                Vertex vertex = _vertices.GetValueOrDefault((y, x), new Vertex(c));
                _vertices[(y, x)] = vertex;
                
                if (x > 0) CheckAdjacent(vertex, y, x - 1);
                if (x < _unparsed[y].Length - 1) CheckAdjacent(vertex, y, x + 1);
                if (y > 0) CheckAdjacent(vertex, y - 1, x);
                if (y < _unparsed.Length - 1) CheckAdjacent(vertex, y + 1, x);

            }
            
        }

    }

    private void CheckAdjacent(Vertex current, int y, int x)
    {
        char c = current.Name;
        char tChar = _unparsed[y][x];
        if (Math.Abs(tChar - c) > 1 && (tChar >= c || tChar is 'E' or 'z') && (tChar != 'E' || c != 'z') &&
            (c != 'S' || tChar != 'a')) return;
        
        Vertex targetVertex = _vertices.GetValueOrDefault((y, x), new Vertex(tChar));
        _vertices[(y, x)] = targetVertex;
        Edge edge = new Edge(targetVertex);
        current.Adjacent.AddLast(edge);
    }
    
    public override ValueTask<string> Solve_1()
    {
        Vertex start = _vertices.First(pair => pair.Value.Name == 'S').Value;
        Search(start);
        
        int answer = _vertices.First(pair => pair.Value.Name == 'E').Value.Distance;
        return new ValueTask<string>(answer.ToString());
    }
    
    public override ValueTask<string> Solve_2()
    {
        Vertex[] vertices = _vertices.Where(pair => pair.Value.Name == 'a')
            .Select(pair => pair.Value)
            .ToArray();
        Search(vertices);
        
        int answer = _vertices.First(pair => pair.Value.Name == 'E').Value.Distance;
        return new ValueTask<string>(answer.ToString());
    }

    private void Search(params Vertex[] startVertices)
    {
        _vertices.Values.ToList().ForEach(v => v.Reset());

        Queue<Vertex> queue = new();
        foreach (Vertex vertex in startVertices)
        {
            queue.Enqueue(vertex);
            vertex.Distance = 0;
        }

        while (queue.Count > 0)
        {
            Vertex vertex = queue.Dequeue();

            foreach (var w in vertex.Adjacent.Select(edge => edge.Destination).Where(w => w.Distance == int.MaxValue))
            {
                w.Distance = vertex.Distance + 1;
                w.Previous = vertex;
                queue.Enqueue(w);
            }
        }
    }

}