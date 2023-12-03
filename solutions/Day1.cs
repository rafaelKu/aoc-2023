namespace AoC;

public static class Day1
{
    private static readonly string FileName = nameof(Day1).ToLower();
    public static string ExecutePart1()
    {
        var sum = 0;
        foreach (string line in File.ReadAllText(@$".\data\{FileName}.txt").SplitLines())
        {
            if (line != string.Empty)
            {
                var leftNumber = int.Parse(line.First(c => char.IsNumber(c)).ToString());
                var rightNumber = int.Parse(line.Reverse().First(c => char.IsNumber(c)).ToString());
                var number = leftNumber * 10 + rightNumber;
                Console.WriteLine(number);
                sum += number;
                continue;
            }
        }
        return sum.ToString();
    }

    public static string ExecutePart2()
    {
        var sum = 0;
        var numbersAsStringAndValues = new Dictionary<string, int>
        {
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9},
        };

        foreach (string line in File.ReadAllText(@$".\data\{FileName}.txt").SplitLines())
        {
            if (line != string.Empty)
            {
                var leftNumber = int.Parse(line.First(c => char.IsNumber(c)).ToString());
                var rightNumber = int.Parse(line.Reverse().First(c => char.IsNumber(c)).ToString());

                var lowestPositionOfASubstring = line.Length;
                var highestPositionOfASubstring = 0;
                int? numberOfLowest = null;
                int? numberOfHighest = null;
                foreach (var numbersAsStringAndValue in numbersAsStringAndValues)
                {
                    if (line.Contains(numbersAsStringAndValue.Key))
                    {
                        var positionOfString = line.IndexOf(numbersAsStringAndValue.Key);
                        if (positionOfString < lowestPositionOfASubstring)
                        {
                            lowestPositionOfASubstring = positionOfString;
                            numberOfLowest = numbersAsStringAndValue.Value;
                        }
                        positionOfString = line.LastIndexOf(numbersAsStringAndValue.Key);
                        if (positionOfString > highestPositionOfASubstring)
                        {
                            highestPositionOfASubstring = positionOfString;
                            numberOfHighest = numbersAsStringAndValue.Value;
                        }
                    }
                }

                if (lowestPositionOfASubstring < line.IndexOf(leftNumber.ToString()))
                {
                    leftNumber = numberOfLowest!.Value;
                }
                if (highestPositionOfASubstring > line.LastIndexOf(rightNumber.ToString()))
                {
                    rightNumber = numberOfHighest!.Value;
                }

                var number = leftNumber * 10 + rightNumber;
                Console.WriteLine(number);
                sum += number;
            }
        }
        return sum.ToString();
    }
}