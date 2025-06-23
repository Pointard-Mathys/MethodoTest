namespace Bank.UnitTest;

public class BankAccountTest
{
    [Fact]
    public void Constructor_NullAccountNumber_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BankAccount(null));
    }
    
    [Fact]
    public void Constructor_EmptyAccountNumber_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new BankAccount(""));
    }
    
    [Fact]
    public void Deposit_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var account = new BankAccount("123", 100);
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => account.Deposit(-50));
        Assert.Contains("positive", exception.Message);
    }

}