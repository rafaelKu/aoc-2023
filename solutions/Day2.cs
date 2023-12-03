namespace AoC;

public static class Day2
{
    private static readonly string FileName = nameof(Day2).ToLower();

    public static string ExecutePart1()
    {
        var sum = 0;
        foreach (string line in File.ReadAllText(@$".\data\{FileName}.txt").SplitLines())
        {
            var colorsAndMax = new Dictionary<string, int>
            {
                {"red", 12},
                {"green", 13},
                {"blue", 14},
            };

            var gamePartAndColorsPart = line.Split(": ");
            var areAllStonesAvailable = true;
            foreach (var splittedLine in gamePartAndColorsPart[1].Split("; "))
            {
                var list = new Dictionary<string, int>();
                var splitByColors = splittedLine.Split(" ").Select(x => x.Replace(" ", "").Replace(",", "")).ToList();
                for (int i = 0; i < splitByColors.Count(); i += 2)
                {
                    list.Add(splitByColors[i + 1], int.Parse(splitByColors[i]));
                }

                foreach (var colorAndMax in colorsAndMax)
                {
                    if (list.ContainsKey(colorAndMax.Key))
                    {
                        if (colorsAndMax[colorAndMax.Key] < list[colorAndMax.Key])
                        {
                            areAllStonesAvailable = false;
                            break;
                        }
                    }
                }
            }
            if (areAllStonesAvailable)
            {
                var numberAsString = gamePartAndColorsPart[0].Replace("Game ", "");
                sum += int.Parse(numberAsString);
            }
        }
        return sum.ToString();
    }
    public static string ExecutePart2()
    {
        var sum = 0;
        foreach (string line in File.ReadAllText(@$".\data\{FileName}.txt").SplitLines())
        {
            var colorsAndNeeds = new Dictionary<string, int>
            {
                {"red", 0},
                {"green", 0},
                {"blue", 0},
            };

            var gamePartAndColorsPart = line.Split(": ");
            foreach (var splittedLine in gamePartAndColorsPart[1].Split("; "))
            {
                var list = new Dictionary<string, int>();
                var splitByColors = splittedLine.Split(" ").Select(x => x.Replace(" ", "").Replace(",", "")).ToList();
                for (int i = 0; i < splitByColors.Count(); i += 2)
                {
                    list.Add(splitByColors[i + 1], int.Parse(splitByColors[i]));
                }

                foreach (var colorAndMax in colorsAndNeeds)
                {
                    if (list.ContainsKey(colorAndMax.Key))
                    {
                        if (colorsAndNeeds[colorAndMax.Key] < list[colorAndMax.Key])
                        {
                            colorsAndNeeds[colorAndMax.Key] = list[colorAndMax.Key];
                        }
                    }
                }
            }
            var number = colorsAndNeeds["red"] * colorsAndNeeds["blue"] * colorsAndNeeds["green"];
            Console.WriteLine(number);

            sum += number;
        }
        return sum.ToString();
    }
}