namespace AoC;

public static class Day6
{
    private static readonly string FileName = nameof(Day6).ToLower();

    public static string ExecutePart1()
    {
        var lines = File.ReadAllText(@$".\data\{FileName}.txt").SplitLines().ToArray();
        var times = lines[0].GetNumbers().ToList();
        var recordDistances = lines[1].GetNumbers().ToList();

        var chancesToBeatForRaces = new List<int>();
        for (int races = 0; races < times.Count; races++)
        {
            var timesFasterThanRecord = 0;
            for (int speed = 0; speed < recordDistances[races]; speed++)
            {
                var delayOfStart = speed;
                var myDistance = (times[races] - delayOfStart) * speed;
                if (myDistance < 0)
                    break;

                if (myDistance > recordDistances[races])
                    timesFasterThanRecord++;
            }
            chancesToBeatForRaces.Add(timesFasterThanRecord);
        }

        return chancesToBeatForRaces.Aggregate((a, x) => a * x).ToString();
    }

    public static string ExecutePart2()
    {
        var lines = File.ReadAllText(@$".\data\{FileName}.txt").SplitLines().ToArray();
        var raceTime = lines[0].Replace(" ", "").GetNumbersAsLong().Single();
        var recordDistance = lines[1].Replace(" ", "").GetNumbersAsLong().Single();
        var timesFasterThanRecord = 0;

        var speedWasOnceHigherThanRecord = false;
        for (long speed = 0; speed < recordDistance; speed++)
        {
            var delayOfStart = speed;
            var myDistance = (raceTime - delayOfStart) * speed;

            if (myDistance < 0 || ((myDistance < recordDistance) && speedWasOnceHigherThanRecord))
                break;

            if (myDistance > recordDistance)
            {
                timesFasterThanRecord++;
                speedWasOnceHigherThanRecord = true;
            }
        }

        return timesFasterThanRecord.ToString();
    }
}