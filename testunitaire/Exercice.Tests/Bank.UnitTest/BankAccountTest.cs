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
    

    //Si on fait un depot negatif ca ne fonctionne pas
    [Fact]
    public void Deposit_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var account = new BankAccount("123", 100);
        
        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => account.Deposit(-50));
        Assert.Contains("positive", exception.Message);
    }


    // Si on fait un depot positif, on ajoute le montant a notre compte
    [Fact]
    public void Deposit_PositiveAmount_IncreasesBalance()
    {
        // Arrange
        var account = new BankAccount("123", 100);

        // Act
        account.Deposit(50);

        // Assert
        Assert.Equal(150, account.Balance);
    }


    // pour retirer si on retire 40 euros sur 100 il nous reste 60 euros
    [Fact]
    public void Withdraw_ValidAmount_DecreasesBalance()
    {
        // Arrange
        var account = new BankAccount("123", 100);

        // Act
        account.Withdraw(40);

        // Assert
        Assert.Equal(60, account.Balance);
    }

    //Si on retire un montant négatif on aura une erreur
    [Fact]
    public void Withdraw_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var account = new BankAccount("123", 100);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => account.Withdraw(-10));
        Assert.Contains("positive", exception.Message);
    }

    // on ne peut pas retirer 0 euro
    [Fact]
    public void Withdraw_ZeroAmount_ThrowsArgumentException()
    {
        // Arrange
        var account = new BankAccount("123", 100);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => account.Withdraw(0));
        Assert.Contains("positive", exception.Message);
    }

    //On ne peut pas retirer un montant superieur a notre solde
    [Fact]
    public void Withdraw_AmountGreaterThanBalance_ThrowsInvalidOperationException()
    {
        // Arrange
        var account = new BankAccount("123", 50);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => account.Withdraw(100));
        Assert.Contains("Insufficient", exception.Message);
    }

    //Si on retire exactement le montant de notre solde il nous reste 0 euro
    [Fact]
    public void Withdraw_ExactBalance_SetsBalanceToZero()
    {
        // Arrange
        var account = new BankAccount("123", 75);

        // Act
        account.Withdraw(75);

        // Assert
        Assert.Equal(0, account.Balance);
    }
}