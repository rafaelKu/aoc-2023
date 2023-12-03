using System.Collections.Generic;

namespace AoC;
public static class Extensions
{
    public static IEnumerable<string> SplitLines(this string str)
    {
        return str.Split("\r\n");
    }

    public static string ReplaceWithRockPaperScissor(this char chr)
    {
        return chr switch
        {
            'A' or 'X' => "Rock",
            'B' or 'Y' => "Paper",
            'C' or 'Z' => "Scissor",
            _ => throw new System.Exception(),
        };
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