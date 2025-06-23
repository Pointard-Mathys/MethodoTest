namespace Calculator.Test;

public class OperationTest
{
    [Fact]
    public void Add_WithTwoPositiveNumbers_ShouldReturnSum()
    {
        // Arrange
        var operation = new Operation();
        int a = 5;
        int b = 3;

        // Act
        int result = operation.Add(a, b);

        // Assert
        Assert.Equal(8, result);
    }
    
    [Fact]
    public void Subtract_WithTwoPositiveNumbers_ShouldReturnSubtraction()
    {
        // Arrange
        var operation = new Operation();
        int a = 5;
        int b = 3;
        
        //Act
        int result = operation.Subtract(a, b);
        
        //Assert
        Assert.Equal(2, result);
    }
    
    [Fact]
    public void Divide_WithNumberByZero_ShouldThrowException()
    {
        // Arrange
        var operation = new Operation();
        int a = 5;
        int b = 0;
        
        //Act
        Assert.Throws<DivideByZeroException>(() => operation.Divide(a, b));
    }

    // Ajoutez d'autres tests 
}