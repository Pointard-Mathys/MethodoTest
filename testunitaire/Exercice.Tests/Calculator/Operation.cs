namespace Calculator;

public class Operation : IOperation
{
    public int Add(int a, int b) => a + b;

    public int Subtract(int a, int b)
    {
        throw new NotImplementedException();
    }

    public int Multiply(int a, int b)
    {
        throw new NotImplementedException();
    }

    public double Divide(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Division by zero is not allowed");
        return (double)a / b;
    }

    public int Power(int a, int b)
    {
        throw new NotImplementedException();
    }

    public int Square(int a)
    {
        throw new NotImplementedException();
    }

    public int Cube(int a)
    {
        throw new NotImplementedException();
    }

    public int Factorial(int a)
    {
        throw new NotImplementedException();
    }

    public int SquareRoot(int a)
    {
        throw new NotImplementedException();
    }

    public int CubeRoot(int a)
    {
        throw new NotImplementedException();
    }

    public bool IsEven(int number)
    {
        throw new NotImplementedException();
    }
}