using AoCHelper;

namespace AdventOfCode.Days._12;

public sealed class Day12 : BaseDay
{
    private char[][] _unparsed;
    private Dictionary<(int, int), Vertex> _vertices = new();

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

    // Solution can be improved by searching from the end to all A's instead of performing a lot of BFS's.
    public override ValueTask<string> Solve_2()
    {
        List<int> distances = new();
        foreach (Vertex vertex in _vertices.Where(pair => pair.Value.Name == 'a').Select(pair => pair.Value))
        {
            Search(vertex);
            distances.Add(_vertices.First(pair => pair.Value.Name == 'E').Value.Distance);
        }

        return new ValueTask<string>(distances.Min().ToString());
    }

    private void Search(Vertex start)
    {
        _vertices.Values.ToList().ForEach(v => v.Reset());

        Queue<Vertex> queue = new();
        queue.Enqueue(start);
        start.Distance = 0;

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