namespace Learning;

public class MathHelper
{
    /// <summary>
    /// Determines whether a given integer is even.
    /// </summary>
    /// <param name="number">The integer to check for evenness.</param>
    /// <returns>Returns true if the specified integer is even; otherwise, false.</returns>
    public bool IsEven(int number)
    {
        return number % 2 == 0;
    }
}