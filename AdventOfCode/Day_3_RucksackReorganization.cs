namespace AdventOfCode;

public class Day_3_RucksackReorganization
{
    public void Execute()
    {
        string filePath = InputPathProvider.GetInputPath("day_3.txt");
        int prioritySum = 0;

        using (var reader = new StreamReader(filePath))
        {
            while (reader.Peek() >= 0)
            {
                var elf1 = reader.ReadLine();
                var elf2 = reader.ReadLine();
                var elf3 = reader.ReadLine();

                var intersection = elf1.Intersect(elf2).Intersect(elf3).ToList();
                if (intersection.Count != 1)
                {
                    throw new InvalidDataException();
                }

                int priority = GetPriority(intersection[0]);
                prioritySum += priority;
            }
        }

        Console.WriteLine(prioritySum);
    }


    private int GetPriority(char letter)
    {
        if (char.IsLower(letter))
        {
            return letter - 96;
        }
        else
        {
            return letter - 38;
        }
    }
}
