using System.Text.RegularExpressions;

namespace AoC;
public static class Extensions
{
    public static IEnumerable<string> SplitLines(this string str)
    {
        return str.Split("\r\n");
    }

    public static IEnumerable<long> GetNumbersAsLong(this string str)
    {
        return Regex.Matches(str, @"\d+").Select(x => long.Parse(x.Value));
    }

    public static IEnumerable<int> GetNumbers(this string str)
    {
        return Regex.Matches(str, @"\d+").Select(x => int.Parse(x.Value));
    }

    public static string Reverse(this string str)
    {
        char[] charArray = str.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    public static IEnumerable<int> AllIndexesOf(this string str, string searchString)
    {
        int minIndex = str.IndexOf(searchString);
        while (minIndex != -1)
        {
            yield return minIndex;
            minIndex = str.IndexOf(searchString, minIndex + searchString.Length);
        }
    }

}