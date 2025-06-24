namespace Calculator;

public class Operation : IOperation
{
    public int Add(int a, int b) => a + b;
    
    public int Subtract(int a, int b) => a - b;

    public int Multiply(int a, int b) => a * b;

    public int Power(int a, int b) => (int)Math.Pow(a, b);

    public int Square(int a) => a * a;

    public int Cube(int a) => a * a * a;

    public double Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Division by zero is not allowed");
        return (double)a / b;
    }

    public int Factorial(int a)
    {
        if (a < 0)
            throw new ArgumentOutOfRangeException(nameof(a), "Le nombre doit être positif ou nul");

        int result = 1;
        for (int i = 2; i <= a; i++)
            result *= i;
        return result;
    }

    public int SquareRoot(int a)
    {
        if (a < 0)
            throw new ArgumentOutOfRangeException(nameof(a), "Le nombre doit être positif ou nul");
        return (int)Math.Sqrt(a);
    }

    public int CubeRoot(int a)
    {
        return (int)Math.Round(Math.Cbrt(a));
    }

    public bool IsEven(int number) => number % 2 == 0;
}