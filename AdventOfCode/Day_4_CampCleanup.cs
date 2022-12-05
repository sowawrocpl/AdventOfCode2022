namespace AdventOfCode;

public class Day_4_CampCleanup
{
    public void Execute()
    {
        string filePath = InputPathProvider.GetInputPath("day_4.txt");
        int containingSections = 0;

        using (var reader = new StreamReader(filePath))
        {
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine().Split(',');
                var elf1 = new SectionRange(line[0]);
                var elf2 = new SectionRange(line[1]);


                if (SectionsOverlap(elf1, elf2))
                {
                    containingSections++;
                }
            }
        }

        Console.WriteLine(containingSections);
    }

    private class SectionRange
    {
        public SectionRange(string rangeString)
        {
            var startAndEnd = rangeString.Split('-');
            Start = int.Parse(startAndEnd[0]);
            End = int.Parse(startAndEnd[1]);
        }

        public int Start { get; set; }

        public int End { get; set; }
    }

    private bool SectionsOverlap(SectionRange first, SectionRange second)
    {
        if (first.Start > second.End)
        {
            return false;
        }

        if (second.Start > first.End)
        {
            return false;
        }

        return true;
    }
}
