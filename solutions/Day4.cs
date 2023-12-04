namespace AoC;

public static class Day4
{
    private static readonly string FileName = nameof(Day4).ToLower();

    public static string ExecutePart1()
    {
        var sum = 0;
        var lines = File.ReadAllText(@$".\data\{FileName}.txt").SplitLines().ToArray();

        foreach (var line in lines)
        {
            var lineWithoutPrefix = line.Split(":")[1];
            var bothSides = lineWithoutPrefix.Split("|");
            var numbersOfLeftSide = bothSides[0].GetNumbers();
            var numbersOfRightSide = bothSides[1].GetNumbers();

            var matches = numbersOfRightSide.Count(numberOfRightSide => numbersOfLeftSide.Contains(numberOfRightSide));

            if (matches > 0)
            {
                var powerOf = Math.Pow(2, matches - 1);
                sum += (int)powerOf;
            }
        }

        return sum.ToString();
    }

    public static string ExecutePart2()
    {
        var sum = 0;
        var lines = File.ReadAllText(@$".\data\{FileName}.txt").SplitLines().ToArray();

        var scratchCards = new int[lines.Length];
        for (int cardNumber = 0; cardNumber < lines.Length; cardNumber++)
        {
            var amountOfActualCard = scratchCards[cardNumber] + 1;
            scratchCards[cardNumber] = amountOfActualCard;

            var lineWithoutPrefix = lines[cardNumber].Split(":")[1];
            var bothSides = lineWithoutPrefix.Split("|");
            var numbersOfLeftSide = bothSides[0].GetNumbers();
            var numbersOfRightSide = bothSides[1].GetNumbers();

            var matches = numbersOfRightSide.Count(numberOfRightSide => numbersOfLeftSide.Contains(numberOfRightSide));
            for (int copies = 0; copies < matches; copies++)
            {
                scratchCards[cardNumber + copies + 1] += amountOfActualCard;
            }

            sum += amountOfActualCard;
        }
        return sum.ToString();
    }
}