namespace Calculator;

public interface IOperation
{
    /// <summary>
    /// Adds two integers and returns the result.
    /// </summary>
    /// <param name="a">The first integer to be added.</param>
    /// <param name="b">The second integer to be added.</param>
    /// <returns>The sum of the two integers.</returns>
    public int Add(int a, int b);

    /// <summary>
    /// Subtracts the second integer from the first integer and returns the result.
    /// </summary>
    /// <param name="a">The integer from which the second integer will be subtracted.</param>
    /// <param name="b">The integer to subtract from the first integer.</param>
    /// <returns>The result of the subtraction.</returns>
    public int Subtract(int a, int b);

    /// <summary>
    /// Multiplies two integers and returns the result.
    /// </summary>
    /// <param name="a">The first integer to be multiplied.</param>
    /// <param name="b">The second integer to be multiplied.</param>
    /// <returns>The product of the two integers.</returns>
    public int Multiply(int a, int b);

    /// <summary>
    /// Divides one integer by another and returns the result.
    /// </summary>
    /// <param name="a">The numerator in the division operation.</param>
    /// <param name="b">The denominator in the division operation.</param>
    /// <returns>The result of dividing the numerator by the denominator as a double.</returns>
    /// <exception cref="DivideByZeroException">Thrown when attempting to divide by zero.</exception>
    public double Divide(int a, int b);

    /// <summary>
    /// Raises an integer to the power of another integer and returns the result.
    /// </summary>
    /// <param name="a">The base integer to be raised to a power.</param>
    /// <param name="b">The exponent to which the base integer will be raised.</param>
    /// <returns>The result of raising the base integer to the given power.</returns>
    public int Power(int a, int b);

    /// <summary>
    /// Calculates the square of an integer and returns the result.
    /// </summary>
    /// <param name="a">The integer to be squared.</param>
    /// <returns>The square of the provided integer.</returns>
    public int Square(int a);

    /// <summary>
    /// Calculates the cube of an integer and returns the result.
    /// </summary>
    /// <param name="a">The integer to be cubed.</param>
    /// <returns>The cube of the specified integer.</returns>
    public int Cube(int a);

    /// <summary>
    /// Calculates the factorial of a non-negative integer.
    /// </summary>
    /// <param name="a">The non-negative integer for which the factorial is to be calculated.</param>
    /// <returns>The factorial of the given integer.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the input integer is negative.</exception>
    public int Factorial(int a);

    /// <summary>
    /// Calculates the square root of the specified integer when appropriate.
    /// </summary>
    /// <param name="a">The integer for which the square root is to be calculated.</param>
    /// <returns>The square root of the input integer as an integer value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the input integer is negative.</exception>
    public int SquareRoot(int a);

    /// <summary>
    /// Calculates the cube root of the specified integer.
    /// </summary>
    /// <param name="a">The integer whose cube root needs to be calculated.</param>
    /// <returns>The cube root of the specified integer.</returns>
    public int CubeRoot(int a);

    /// <summary>
    /// Determines whether a given integer is even.
    /// </summary>
    /// <param name="number">The integer to be evaluated.</param>
    /// <returns>True if the integer is even; otherwise, false.</returns>
    public bool IsEven(int number);
}