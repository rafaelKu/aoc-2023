namespace AoC;

public static class Day5
{
    private static readonly string FileName = nameof(Day5).ToLower();

    public class Mapping
    {
        public long DestinationStart;
        public long SourceRangeStart;
        public long Range;
        public Mapping(long destinationStart, long sourceRangeStart, long range)
        {
            DestinationStart = destinationStart;
            SourceRangeStart = sourceRangeStart;
            Range = range;
        }
    }

    public static string ExecutePart1()
    {
        var lines = File.ReadAllText(@$".\data\{FileName}.txt").SplitLines().ToArray();

        var seeds = new List<long>();
        var mappingDestinationSource = new List<Mapping>[7];

        var indexOfMappings = 0;
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            if (line.Contains("seeds"))
            {
                seeds = line.Split(':')[1].GetNumbersAsLong().ToList();
                continue;
            }
            if (line.Contains(':'))
            {
                indexOfMappings++;
                mappingDestinationSource[indexOfMappings - 1] = new List<Mapping>();
                continue;
            }

            var numbers = line.GetNumbersAsLong().ToList();
            var destinationRangeStart = numbers[0];
            var sourceRangeStart = numbers[1];
            var rangeSize = numbers[2];

            mappingDestinationSource[indexOfMappings - 1].Add(new Mapping(destinationRangeStart, sourceRangeStart, rangeSize));
        }

        var lowestLocation = long.MaxValue;
        foreach (var seed in seeds)
        {
            var lastSeedNumber = seed;
            for (long i = 0; i < 7; i++)
            {
                Console.WriteLine(lastSeedNumber);

                var availableMapping = mappingDestinationSource[i]
                    .Where(mapping => lastSeedNumber >= mapping.SourceRangeStart && lastSeedNumber <= mapping.SourceRangeStart + mapping.Range)
                    .FirstOrDefault();

                if (availableMapping is not null)
                {
                    var deviation = availableMapping.DestinationStart - availableMapping.SourceRangeStart;
                    lastSeedNumber += deviation;
                }
            }
            Console.WriteLine("");
            if (lowestLocation > lastSeedNumber)
            {
                lowestLocation = lastSeedNumber;
            }
        }

        return lowestLocation.ToString();
    }
}