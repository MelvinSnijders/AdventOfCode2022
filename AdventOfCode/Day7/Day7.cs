namespace AdventOfCode.Day7;

public class Day7
{

    private readonly FileObject _rootNode;

    public Day7()
    {
        string input = File.ReadAllText("Day7/input.txt");
        string[] lines = input.Split("\n");
        _rootNode = ParseCommands(lines);
    }

    private FileObject ParseCommands(string[] lines)
    {
        FileObject rootNode = new FileObject(0, "/", null, true);
        FileObject currentDir = rootNode;

        // Convert commands into a tree structure
        foreach (string line in lines)
        {

            if (line.StartsWith("$ cd"))
            {
                // Move to the specified directory
                string dir = line.Split(" ")[2];

                if (dir.Equals(".."))
                {
                    currentDir = currentDir.Parent ?? currentDir;
                }
                else
                {
                    currentDir = currentDir.GetDirectory(dir);
                }
            } else {
                string[] splittedLine = line.Split(" ");
                if (line.StartsWith("dir"))
                {
                    currentDir.CreateDirectory(line.Split(" ")[1]);
                }
                else
                {
                    int fileSize = int.Parse(splittedLine[0]);
                    string fileName = splittedLine[1];
                    currentDir.CreateFile(fileSize, fileName);
                }
            }

        }

        return rootNode;

    }
    
    public int SolvePart1()
    {
        List<FileObject> allowedDirs = CheckSize(_rootNode);
        return allowedDirs.Select(dir => dir.Size).Sum();;
    }

    // WARNING: Recursive function ahead!
    private List<FileObject> CheckSize(FileObject fileObject)
    {

        List<FileObject> allowed = new List<FileObject>();
        
        if (fileObject is { Size: <= 100_000, IsDirectory: true }) allowed.Add(fileObject);

        fileObject.Children
            .Where(child => child.Children.Count > 0)
            .ToList()
            .ForEach(child => allowed.AddRange(CheckSize(child)));

        return allowed;

    }

    public int SolvePart2()
    {
        int usedSpace = _rootNode.Size;
        int freeSpace = 70000000 - usedSpace;
        int deletionNeeded = 30000000 - freeSpace;
        
        return FindSizes(_rootNode).Where(size => size >= deletionNeeded).Min();

    }

    private List<int> FindSizes(FileObject node)
    {
        
        List<int> sizes = new List<int> { node.Size };

        node.Children
            .Where(child => child.Children.Count > 0)
            .ToList()
            .ForEach(child => sizes.AddRange(FindSizes(child)));

        return sizes;
    }

    internal class FileObject
    {
     
        public int Size { get; private set; }
        public string Name { get; }
        public List<FileObject> Children { get; }
        public FileObject? Parent { get; }
        
        public bool IsDirectory { get; }

        public FileObject(int size, string name, FileObject? parent, bool isDirectory)
        {
            Size = size;
            Name = name;
            Children = new List<FileObject>();
            Parent = parent;
            IsDirectory = isDirectory;
        }

        public FileObject GetDirectory(string name)
        {
            return Children.Find(fileObject => fileObject.Name.Equals(name)) ?? this;
        }

        public void CreateDirectory(string name)
        {
            FileObject fileObject = new FileObject(0, name, this, true);
            Children.Add(fileObject);
        }

        public void CreateFile(int size, string name)
        {
            FileObject fileObject = new FileObject(size, name, this, false);
            Children.Add(fileObject);
            IncreaseSize(size);
        }

        private void IncreaseSize(int size)
        {
            Size += size;
            Parent?.IncreaseSize(size);
        }
        
    }
    
}