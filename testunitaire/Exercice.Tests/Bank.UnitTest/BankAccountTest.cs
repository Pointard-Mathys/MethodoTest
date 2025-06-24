namespace Bank.UnitTest;

public class BankAccountTest
{
    [Fact]
    public void Constructor_NullAccountNumber_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new BankAccount(null));
        Assert.Equal("Account number cannot be null or empty", exception.Message);
    }
    
    [Fact]
    public void Constructor_ValidAccount_CreatesAccountCorrectly()
    {
        // Arrange & Act
        var bank = new BankAccount("ABC123", 150);

        // Assert
        Assert.Equal("ABC123", bank.AccountNumber);
        Assert.Equal(150, bank.Balance);
        Assert.NotNull(bank.TransactionHistory);
        Assert.Empty(bank.TransactionHistory);
    }
    
    [Fact]
    public void Constructor_EmptyAccountNumber_ThrowsArgumentException()
    {
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new BankAccount("", 100));
        Assert.Equal("Account number cannot be null or empty", exception.Message);
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