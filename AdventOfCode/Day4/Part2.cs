namespace AdventOfCode.Day4;

public class Part2
{
    private readonly string[] _assignmentPairs;

    public Part2()
    {
        string input = File.ReadAllText("Day4/input.txt");
        _assignmentPairs = input.Split("\n");
    }

    public int Solve()
    {

        int overlapCount = 0;
        
        foreach (string assignmentPair in _assignmentPairs)
        {
            string[] pairs = assignmentPair.Split(",");

            string sectionsAssigned1 = pairs[0];
            string sectionsAssigned2 = pairs[1];

            string[] sections1 = sectionsAssigned1.Split('-');
            int from1 = int.Parse(sections1[0]);
            int to1 = int.Parse(sections1[1]);
            
            string[] sections2 = sectionsAssigned2.Split('-');
            int from2 = int.Parse(sections2[0]);
            int to2 = int.Parse(sections2[1]);
            
            if (from1 <= to2 && from2 <= to1)
            {
                overlapCount++;
            }

        }

        return overlapCount;

    }
}