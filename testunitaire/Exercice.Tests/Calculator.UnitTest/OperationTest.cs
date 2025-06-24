namespace Calculator.Test;

public class OperationTest
{
    /// <summary>
    /// Vérifie que la méthode Add retourne la somme de deux entiers positifs.
    /// </summary>
    [Fact]
    public void Add_WithTwoPositiveNumbers_ShouldReturnSum()
    {
        //Assert
        var operation = new Operation();
        var a = 5;
        var b = 3;
        
        //Act
        int result = operation.Add(a, b);
        
        //Assert
        Assert.Equal(8, result);
    }

    /// <summary>
    /// Vérifie que la méthode Subtract retourne la différence correcte entre deux entiers positifs.
    /// </summary>
    [Fact]
    public void Subtract_WithTwoPositiveNumbers_ShouldReturnSubtraction()
    {
        //Arrange
        var operation = new Operation();
        var a = 5;
        var b = 3;
        
        //Act
        int result = operation.Subtract(a, b);
        
        //Assert
        Assert.Equal(2, result);
    }

    /// <summary>
    /// Vérifie que la méthode Multiply retourne le produit de deux entiers.
    /// </summary>
    [Fact]
    public void Multiply_WithTwoPositiveNumbers_ShouldReturnProduct()
    {
        //Arrange
        var operation = new Operation();
        var a = 4;
        var b = 3;
        
        //Act
        int result = operation.Multiply(a, b);
        
        //Assert
        Assert.Equal(12, result);
    }

    /// <summary>
    /// Vérifie que la méthode Divide lève une exception si le diviseur est zéro.
    /// </summary>
    [Fact]
    public void Divide_WithNumberByZero_ShouldThrowException()
    {
        //Arrange
        var operation = new Operation();
        var a = 5;
        var b = 0;
        
        //Act & Assert
        Assert.Throws<DivideByZeroException>(() => operation.Divide(a, b));
    }

    /// <summary>
    /// Vérifie que la méthode Power élève un entier à une puissance donnée.
    /// </summary>
    [Fact]
    public void Power_WithBaseAndExponent_ShouldReturnCorrectResult()
    {
        //Arrange
        var operation = new Operation();
        var a = 2;
        var b = 3;
        
        //Act
        int result = operation.Power(a, b);
        
        //Assert
        Assert.Equal(8, result);
    }

    /// <summary>
    /// Vérifie que Square retourne le carré d’un entier.
    /// </summary>
    [Fact]
    public void Square_WithPositiveNumber_ShouldReturnSquare()
    {
        //Arrange
        var operation = new Operation();
        var a = 4;
        
        //Act
        int result = operation.Square(a);
        
        //Assert
        Assert.Equal(16, result);
    }

    /// <summary>
    /// Vérifie que Cube retourne le cube d’un entier.
    /// </summary>
    [Fact]
    public void Cube_WithPositiveNumber_ShouldReturnCube()
    {
        //Arrange
        var operation = new Operation();
        var a = 3;
        
        //Act
        int result = operation.Cube(a);
        
        //Assert
        Assert.Equal(27, result);
    }

    /// <summary>
    /// Vérifie que Factorial retourne 1 pour 0 (cas de base).
    /// </summary>
    [Fact]
    public void Factorial_Zero_ShouldReturnOne()
    {
        //Arrange
        var operation = new Operation();
        var a = 0;
        
        //Act
        int result = operation.Factorial(a);
        
        //Assert
        Assert.Equal(1, result);
    }

    /// <summary>
    /// Vérifie que IsEven retourne true pour un nombre pair.
    /// </summary>
    [Fact]
    public void IsEven_WithEvenNumber_ShouldReturnTrue()
    {
        //Arrange
        var operation = new Operation();
        var a = 4;
        
        //Act
        bool result = operation.IsEven(a);
        
        //Assert
        Assert.True(result);
    }

    /// <summary>
    /// Vérifie que SquareRoot retourne correctement la racine carrée entière.
    /// </summary>
    [Fact]
    public void SquareRoot_OfPerfectSquare_ShouldReturnRoot()
    {
        //Arrange
        var operation = new Operation();
        var a = 49;
        
        //Act
        int result = operation.SquareRoot(a);
        
        //Assert
        Assert.Equal(7, result);
    }

    /// <summary>
    /// Vérifie que CubeRoot retourne correctement la racine cubique entière.
    /// </summary>
    [Fact]
    public void CubeRoot_OfPerfectCube_ShouldReturnRoot()
    {
        //Arrange
        var operation = new Operation();
        var a = 27;
        
        //Act
        int result = operation.CubeRoot(a);
        
        //Assert
        Assert.Equal(3, result);
    }
}
