using AoCHelper;

namespace AdventOfCode.Days._07;

public sealed class Day7 : BaseDay
{
    private readonly Folder _rootFolder;

    public Day7()
    {
        string input = System.IO.File.ReadAllText(InputFilePath);
        string[] lines = input.Split("\n");
        _rootFolder = ParseCommands(lines);
    }

    public override ValueTask<string> Solve_1()
    {
        IEnumerable<Folder> allowedDirs = CheckSize(_rootFolder);
        int solution = allowedDirs.Select(dir => dir.GetSize()).Sum();
        return new ValueTask<string>(solution.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        int usedSpace = _rootFolder.GetSize();
        int freeSpace = 70000000 - usedSpace;
        int deletionNeeded = 30000000 - freeSpace;

        int solution = FindSizes(_rootFolder).Where(size => size >= deletionNeeded).Min();
        return new ValueTask<string>(solution.ToString());
    }

    private Folder ParseCommands(IEnumerable<string> lines)
    {
        Folder rootNode = new Folder("/");
        Folder currentDir = rootNode;

        foreach (string line in lines)
        {
            if (line.StartsWith("$ cd"))
            {
                string dir = line.Split(" ")[2];

                currentDir = dir switch
                {
                    ".." => currentDir.Parent ?? currentDir,
                    "/" => rootNode,
                    _ => currentDir.SearchItem(dir) as Folder ?? currentDir
                };
            }
            else if (!line.StartsWith("$"))
            {
                string[] splitLine = line.Split(" ");
                Item toAdd;
                if (line.StartsWith("dir")) toAdd = new Folder(splitLine[1]);
                else toAdd = new File(splitLine[1], int.Parse(splitLine[0]));
                currentDir.AddItem(toAdd);
            }
        }

        return rootNode;
    }

    private IEnumerable<Folder> CheckSize(Folder fileObject)
    {
        List<Folder> allowed = new();

        if (fileObject.GetSize() <= 100_000) allowed.Add(fileObject);

        fileObject.Items
            .OfType<Folder>()
            .Where(child => child.Items.Count > 0)
            .ToList()
            .ForEach(child => allowed.AddRange(CheckSize(child)));

        return allowed;
    }

    private IEnumerable<int> FindSizes(Folder node)
    {
        List<int> sizes = new() { node.GetSize() };

        node.Items
            .OfType<Folder>()
            .Where(child => child.Items.Count > 0)
            .ToList()
            .ForEach(child => sizes.AddRange(FindSizes(child)));

        return sizes;
    }
}