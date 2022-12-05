namespace AdventOfCode;

public class Day_2_RockPaperScissors
{
    private enum RoundResultEnum
    {
        Loss = 0,
        Draw = 3,
        Win = 6,
    }

    private enum ShapeEnum
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3,
    }

    public void Execute()
    {
        string filePath = InputPathProvider.GetInputPath("day_2.txt");
        int myTotalScore = 0;

        using (var reader = new StreamReader(filePath))
        {
            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine();
                var choices = line.ToCharArray();

                ShapeEnum opponentShape = DecryptShape(choices[0]);
                RoundResultEnum desiredResult = DecryptResult(choices[2]);

                ShapeEnum myShape = GetMyShape(opponentShape, desiredResult);
                int roundScore = (int)myShape + (int)desiredResult;
                myTotalScore += roundScore;
            }
        }

        Console.WriteLine(myTotalScore);
    }

    private ShapeEnum DecryptShape(char encrypted)
    {
        switch (encrypted)
        {
            case 'A':
                return ShapeEnum.Rock;

            case 'B':
                return ShapeEnum.Paper;

            case 'C':
                return ShapeEnum.Scissors;

            default:
                throw new NotSupportedException();
        }
    }

    private RoundResultEnum DecryptResult(char encrypted)
    {
        switch (encrypted)
        {
            case 'X':
                return RoundResultEnum.Loss;

            case 'Y':
                return RoundResultEnum.Draw;

            case 'Z':
                return RoundResultEnum.Win;

            default:
                throw new NotSupportedException();
        }
    }

    private ShapeEnum GetMyShape(ShapeEnum opponentShape, RoundResultEnum result)
    {
        if (result == RoundResultEnum.Draw)
        {
            return opponentShape;
        }

        switch (opponentShape)
        {
            case ShapeEnum.Rock:
                return result == RoundResultEnum.Win
                    ? ShapeEnum.Paper
                    : ShapeEnum.Scissors;

            case ShapeEnum.Paper:
                return result == RoundResultEnum.Win
                    ? ShapeEnum.Scissors
                    : ShapeEnum.Rock;

            case ShapeEnum.Scissors:
                return result == RoundResultEnum.Win
                    ? ShapeEnum.Rock
                    : ShapeEnum.Paper;

            default:
                throw new NotSupportedException();
        }
    }
}
