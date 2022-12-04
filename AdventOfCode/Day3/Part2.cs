namespace AdventOfCode.Day3;

public class Part2
{
    private readonly string[] _rucksacks;

    public Part2()
    {
        string input = File.ReadAllText("Day3/input.txt");
        _rucksacks = input.Split("\n");
    }

    public int Solve()
    {

        int prioritySum = 0;
        
        // Group into chunks of 3
        List<string[]> groups = new List<string[]>();
        for (int i = 0; i < _rucksacks.Length; i += 3)
        {
            string[] buffer = new string[3];
            Array.Copy(_rucksacks, i, buffer, 0, 3);
            groups.Add(buffer);
        }
        
        // Process
        foreach (string[] group in groups)
        {
            string rucksack1 = group[0];
            string rucksack2 = group[1];
            string rucksack3 = group[2];

            char intersect = rucksack1.Intersect(rucksack2).Intersect(rucksack3).First();
            Console.WriteLine(intersect);
            int code = 0;
            if(char.IsUpper(intersect)) code = intersect - 38; // Uppercase = 27-52
            if (char.IsLower(intersect)) code = intersect - 96; // Lowercase = 1-26
            prioritySum += code;
        }

        return prioritySum;
        
    }
}