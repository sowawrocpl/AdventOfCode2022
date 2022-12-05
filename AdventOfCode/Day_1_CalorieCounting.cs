namespace AdventOfCode;

public class Day_1_CalorieCounting
{
    readonly List<int> bestElves = new();

    public void Execute()
    {
        string filePath = InputPathProvider.GetInputPath("day_1.txt");
        using (var reader = new StreamReader(filePath))
        {
            int currentElfCalories = 0;
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrEmpty(line) == false)
                {
                    int calories = Int32.Parse(line);
                    currentElfCalories += calories;
                }
                else
                {
                    UpdatePodium(currentElfCalories);
                    currentElfCalories = 0;
                }
            }
        }

        Console.WriteLine(bestElves.Sum());
    }

    private void UpdatePodium(int currentElfCalories)
    {
        if (bestElves.Count < 3)
        {
            bestElves.Add(currentElfCalories);
            return;
        }

        var theWorstOfTheBest = bestElves.Min();
        if (currentElfCalories > theWorstOfTheBest)
        {
            bestElves.Remove(theWorstOfTheBest);
            bestElves.Add(currentElfCalories);
        }
    }
}
