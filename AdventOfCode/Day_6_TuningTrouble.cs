namespace AdventOfCode;

public class Day_6_TuningTrouble
{
    public void Execute()
    {
        string filePath = InputPathProvider.GetInputPath("day_6.txt");
        const int markerSize = 14;
        int counter = 0;

        using (var reader = new StreamReader(filePath))
        {
            var buffer = new List<int>();
            while (reader.Peek() >= 0)
            {
                buffer.Add(reader.Read());
                counter++;

                if (buffer.Count > markerSize)
                {
                    buffer.RemoveAt(0);
                }

                if (buffer.Count == markerSize && buffer.Distinct().Count() == markerSize)
                {
                    break;
                }
            }
        }

        Console.WriteLine(counter);
    }
}
