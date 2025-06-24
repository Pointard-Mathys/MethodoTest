using Processor.Contracts;

namespace Processor.Services;

public class StringProcessor : IStringProcessor
{
    public string Reverse(string input)
    {
        if (input == null) return null;
        return new string(input.Reverse().ToArray());
    }

    public bool IsPalindrome(string input)
    {
        if (input == null) return false;

        var normalized = new string(input
            .Where(char.IsLetterOrDigit)
            .Select(char.ToLower)
            .ToArray());

        return normalized == new string(normalized.Reverse().ToArray());
    }

    public int CountWords(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return 0;
        return input.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public string Capitalize(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return char.ToUpper(input[0]) + input.Substring(1);
    }
}