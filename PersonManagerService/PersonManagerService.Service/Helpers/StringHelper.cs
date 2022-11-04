using System.Text.RegularExpressions;

namespace PersonManagerService.Application.Helpers;

public static class StringHelper
{
    public static string GetReversed(this string input)
    {
        return new string(input.Reverse().ToArray());
    }

    public static int GetVovelsNumber(this string input)
    {
        string vowelsPattern = @"[aeiou]";
        return Regex.Matches(input, vowelsPattern, RegexOptions.IgnoreCase).Count;
    }
    public static int GetConstenantsNumber(this string input)
    {
        string consonantsPattern = @"[a-z-[aeiou]]";
        return Regex.Matches(input, consonantsPattern, RegexOptions.IgnoreCase).Count;
    }
}
