namespace AdventOfCode.Day7;

public class Day7
{

    private readonly Folder _rootFolder;

    public Day7()
    {
        string input = System.IO.File.ReadAllText("Day7/input.txt");
        string[] lines = input.Split("\n");
        _rootFolder = ParseCommands(lines);
    }

    private Folder ParseCommands(string[] lines)
    {
        Folder rootNode = new Folder("/");
        Folder currentDir = rootNode;
        
        foreach (string line in lines)
        {

            if (line.StartsWith("$ cd"))
            {
                string dir = line.Split(" ")[2];
                
                if (dir.Equals("..")) currentDir = currentDir.Parent ?? currentDir ;
                else if(dir.Equals("/"))  currentDir = rootNode;
                else currentDir = currentDir.SearchItem(dir) as Folder ?? currentDir;

            } else if(!line.StartsWith("$")) {
                
                string[] splittedLine = line.Split(" ");
                Item toAdd;
                if (line.StartsWith("dir")) toAdd = new Folder(splittedLine[1]);
                else toAdd = new File(splittedLine[1], int.Parse(splittedLine[0]));
                currentDir.AddItem(toAdd);
                
            }

        }

        return rootNode;

    }
    
    public int SolvePart1()
    {
        List<Folder> allowedDirs = CheckSize(_rootFolder);
        return allowedDirs.Select(dir => dir.GetSize()).Sum();;
    }
    
    private List<Folder> CheckSize(Folder fileObject)
    {

        List<Folder> allowed = new List<Folder>();
        
        if (fileObject.GetSize() <= 100_000) allowed.Add(fileObject);

        fileObject.Items
            .OfType<Folder>()
            .Where(child => child.Items.Count > 0)
            .ToList()
            .ForEach(child => allowed.AddRange(CheckSize(child)));

        return allowed;

    }

    public int SolvePart2()
    {
        int usedSpace = _rootFolder.GetSize();
        int freeSpace = 70000000 - usedSpace;
        int deletionNeeded = 30000000 - freeSpace;
        
        return FindSizes(_rootFolder).Where(size => size >= deletionNeeded).Min();

    }

    private List<int> FindSizes(Folder node)
    {
        
        List<int> sizes = new List<int> { node.GetSize() };

        node.Items
            .OfType<Folder>()
            .Where(child => child.Items.Count > 0)
            .ToList()
            .ForEach(child => sizes.AddRange(FindSizes(child)));

        return sizes;
    }

}