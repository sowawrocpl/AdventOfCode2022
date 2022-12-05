namespace AdventOfCode;

public class Day_5_SupplyStacks
{
    private Dictionary<int, Stack<char>> _stackDictionary;

    public void Execute()
    {
        string filePath = InputPathProvider.GetInputPath("day_5.txt");
        using (var reader = new StreamReader(filePath))
        {
            List<string> stateLines = new List<string>();
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();

                if (line.Contains('['))
                {
                    stateLines.Add(line);
                }
                else if (line.StartsWith(' '))
                {
                    BuildStackState(stateLines, line);
                }
                else if (line.Contains("move"))
                {
                    Move(line);
                }
            }
        }

        var result = _stackDictionary.Values.Select(x => x.Peek()).ToArray();
        Console.WriteLine(result);
    }

    private void BuildStackState(List<string> stateLines, string numbersString)
    {
        var splitted = numbersString.Split(' ').Where(x => string.IsNullOrWhiteSpace(x) == false);
        var stackNumbers = splitted.Select(x => int.Parse(x)).ToList();
        _stackDictionary = stackNumbers.ToDictionary(key => key, val => new Stack<char>());

        for (int i = stateLines.Count - 1; i >= 0; i--)
        {
            LoadCrates(stateLines[i], stackNumbers);
        }
    }

    private void LoadCrates(string levelString, List<int> stackNumbers)
    {
        foreach (var stack in stackNumbers)
        {
            int index = (stack * 4) - 3;
            char crate = levelString[index];
            if (crate != ' ')
            {
                _stackDictionary[stack].Push(crate);
            }
        }
    }

    private void Move(string moveString)
    {
        string cleaned = moveString.Replace("move ", "");
        cleaned = cleaned.Replace("from ", "");
        cleaned = cleaned.Replace("to ", "");
        List<int> numbers = cleaned.Split(' ').Select(x => int.Parse(x)).ToList();

        int count = numbers[0];
        int source = numbers[1];
        int destination = numbers[2];

        Stack<char> tempStack = new Stack<char>();

        for (int i = 0; i < count; i++)
        {
            char crate = _stackDictionary[source].Pop();
            tempStack.Push(crate);
        }

        for (int i = 0; i < count; i++)
        {
            char crate = tempStack.Pop();
            _stackDictionary[destination].Push(crate);
        }
    }
}
