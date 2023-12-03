using System.Globalization;
using System.Text.RegularExpressions;

namespace AoC;

public static class Day3
{
    private static readonly string FileName = nameof(Day3).ToLower();

    public static string ExecutePart1()
    {
        var sum = 0;
        var lines = File.ReadAllText(@$".\data\{FileName}.txt").SplitLines().ToArray();

        for (int i = 0; i < lines.Length; i++)
        {
            var previousLine = i == 0 ? string.Empty : lines[i - 1];
            var actualLine = lines[i];
            var nextLine = i == lines.Length - 1 ? string.Empty : lines[i + 1];

            var numberAsStringAndIndexes = Regex.Matches(actualLine, @"\d+").Select(x => (x.ToString(), x.Index));
            foreach (var (numberAsString, index) in numberAsStringAndIndexes)
            {
                var positionEnd = index + numberAsString.Length - 1;

                var hasNumberTouchings =
                    HasNumberPositionTouchingsToLine(previousLine, index, positionEnd) ||
                    HasNumberPositionTouchingsToLine(nextLine, index, positionEnd) ||
                    HasNumberPositionTouchingsToLine(actualLine, index, positionEnd);

                if (hasNumberTouchings)
                {
                    Console.WriteLine(numberAsString);
                    sum += int.Parse(numberAsString);
                }
            }
        }

        return sum.ToString();
    }

    public static string ExecutePart2()
    {
        var sum = 0;
        var lines = File.ReadAllText(@$".\data\{FileName}.txt").SplitLines().ToArray();

        for (int i = 0; i < lines.Length; i++)
        {
            var previousLine = i == 0 ? string.Empty : lines[i - 1];
            var actualLine = lines[i];
            var nextLine = i == lines.Length - 1 ? string.Empty : lines[i + 1];

            var indexesOfGear = actualLine.AllIndexesOf("*");

            foreach (var indexOfGear in indexesOfGear)
            {
                var overlappingNumbers = new List<int>();
                AddOverlappingNumbers(overlappingNumbers, actualLine, indexOfGear);
                AddOverlappingNumbers(overlappingNumbers, nextLine, indexOfGear);
                AddOverlappingNumbers(overlappingNumbers, previousLine, indexOfGear);

                if(overlappingNumbers.Count == 2)
                {
                    sum += overlappingNumbers[0] * overlappingNumbers[1];
                }
            }
        }
        return sum.ToString();
    }

    private static void AddOverlappingNumbers(List<int> overlappingNumbers, string line, int IndexOfGear)
    {
        var numberAsStringAndIndexes = Regex.Matches(line, @"\d+").Select(x => (x.ToString(), x.Index));
        foreach (var (numberAsString, startPositionNumber) in numberAsStringAndIndexes)
        {
            var positionEnd = startPositionNumber + numberAsString.Length - 1;

            if (startPositionNumber - 1 <= IndexOfGear && positionEnd + 1 >= IndexOfGear)
            {
                overlappingNumbers.Add(int.Parse(numberAsString));
            }
        }
    }

    private static bool HasNumberPositionTouchingsToLine(string line, int positionStart, int positionEnd)
    {
        var hasTouchingSign = false;
        var signsOfLine = Regex.Matches(line, "([^(.)(0-9)]*)")
            .Select(x => x.ToString())
            .Where(x => x != string.Empty)
            .Distinct();

        foreach (var signs in signsOfLine)
        {
            var anyIndexOfSign = line.AllIndexesOf(signs);
            foreach (var index in anyIndexOfSign)
            {
                if (positionStart - 1 <= index && positionEnd + 1 >= index)
                {
                    hasTouchingSign = true;
                    break;
                }
            }
        }
        return hasTouchingSign;
    }
}