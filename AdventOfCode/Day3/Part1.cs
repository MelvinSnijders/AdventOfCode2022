namespace AdventOfCode.Day3;

public class Part1
{
    private readonly string[] _rucksacks;

    public Part1()
    {
        string input = File.ReadAllText("Day3/input.txt");
        _rucksacks = input.Split("\n");
    }

    public int Solve()
    {

        int prioritySum = 0;
        foreach (string rucksack in _rucksacks)
        {
            string compartment1 = rucksack.Substring(0, rucksack.Length / 2);
            string compartment2 = rucksack.Substring(rucksack.Length / 2);

            char duplicate = compartment1.ToList().Find(character => compartment2.Contains(character));

            int code = 0;
            if(char.IsUpper(duplicate)) code = duplicate - 38; // Uppercase = 27-52
            if (char.IsLower(duplicate)) code = duplicate - 96; // Lowercase = 1-26

            Console.WriteLine($"{duplicate} {code}");
            
            prioritySum += code;

        }
        
        return prioritySum;

    }
}