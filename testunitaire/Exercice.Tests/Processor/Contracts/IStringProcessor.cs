namespace Processor.Contracts;

public interface IStringProcessor
{
    /// <summary>
    /// Reverses the characters in the provided string.
    /// </summary>
    /// <param name="input">The input string to be reversed.</param>
    /// <returns>A new string where the characters of the input are in reverse order.</returns>
    public string Reverse(string input);

    /// <summary>
    /// Determines whether the provided string is a palindrome.
    /// </summary>
    /// <param name="input">The input string to check for palindrome property.</param>
    /// <returns>True if the input is a palindrome; otherwise, false.</returns>
    public bool IsPalindrome(string input);

    /// <summary>
    /// Counts the number of words in the provided string.
    /// </summary>
    /// <param name="input">The input string from which words will be counted.</param>
    /// <returns>The total count of words in the input string.</returns>
    public int CountWords(string input);

    /// <summary>
    /// Capitalizes the first character of the provided string while maintaining the rest of the string's case.
    /// </summary>
    /// <param name="input">The input string to capitalize.</param>
    /// <returns>A new string with the first character converted to uppercase, if applicable.</returns>
    public string Capitalize(string input);
}