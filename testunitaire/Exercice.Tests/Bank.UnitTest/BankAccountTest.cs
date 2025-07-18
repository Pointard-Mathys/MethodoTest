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
    public void Constructor_ZeroInitialBalance_CreatesAccountCorrectly()
    {
        // Arrange & Act
        var bank = new BankAccount("ABC123", 0);

        // Assert
        Assert.Equal("ABC123", bank.AccountNumber);
        Assert.Equal(0, bank.Balance);
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
    public void Constructor_NegativeInitialBalance_ThrowsArgumentException()
    {
        // Arrange & Act
        var exception = Assert.Throws<ArgumentException>(() =>
            new BankAccount("ABC123", -150));

        // Assert
        Assert.Equal("Initial balance cannot be negative", exception.Message);
    }
    
/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////////////////////////
/// depot
/// </summary>

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
    
    [Fact]
    public void Deposit_ZeroAmount_ThrowsArgumentException()
    {
        // Arrange
        var account = new BankAccount("123", 100);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => account.Deposit(0));
        Assert.Equal("Deposit amount must be positive.", exception.Message);
        Assert.Equal(100, account.Balance); // le solde n'a pas changé
    }
    
    [Fact]
    public void Deposit_PositiveAmount_IncreasesBalance_startZero()
    {
        // Arrange
        var account = new BankAccount("123", 0);

        // Act
        account.Deposit(150);

        // Assert
        Assert.Equal(150, account.Balance);
    }
    
/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////////////////////////
/// retrait
/// </summary>

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

    //Si on retire un montant negatif on aura une erreur
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
    
/// <summary>
/// /////////////////////////////////////////////////////////////////////////////////////////////////////
/// transfer
/// </summary>

    // Transfert valide entre 2 comptes
    [Fact]
    public void Transfer_ValidAmount_UpdatesBothAccounts()
    {
        // Arrange
        var sender = new BankAccount("123", 100);
        var receiver = new BankAccount("456", 50);

        // Act
        sender.Transfer(receiver, 30);

        // Assert
        Assert.Equal(70, sender.Balance);
        Assert.Equal(80, receiver.Balance);
    }

    //Transfert avec un montant negatif : erreur
    [Fact]
    public void Transfer_NegativeAmount_ThrowsArgumentException()
    {
        // Arrange
        var sender = new BankAccount("123", 100);
        var receiver = new BankAccount("456", 50);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => sender.Transfer(receiver, -20));
        Assert.Contains("positive", ex.Message);
    }
    
    //Transfert avec un montant nul = 0
    [Fact]
    public void Transfer_ZeroAmount_ThrowsArgumentException()
    {
        // Arrange
        var sender = new BankAccount("123", 100);
        var receiver = new BankAccount("456", 50);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => sender.Transfer(receiver, 0));
        Assert.Contains("positive", ex.Message);
    }
    
    //Transfert superieur au solde
    [Fact]
    public void Transfer_AmountGreaterThanBalance_ThrowsInvalidOperationException()
    {
        // Arrange
        var sender = new BankAccount("123", 50);
        var receiver = new BankAccount("456", 100);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => sender.Transfer(receiver, 75));
        Assert.Contains("Insufficient", ex.Message);
    }
    
    [Fact]
    public void Transfer_MyAccount_AmountGreaterThanBalance_ThrowsInvalidOperationException()
    {
        // Arrange
        var sender = new BankAccount("123", 10);
        var receiver = new BankAccount("123", 50);

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => sender.Transfer(receiver, 25)); // 25 > 10
        Assert.Contains("Insufficient", ex.Message);
    }

    //Transfert qui ajoute une entree dans l'historique du compte 
    [Fact]
    public void Transfer_ValidAmount_AddsToTransactionHistory()
    {
        // Arrange
        var sender = new BankAccount("123", 100);
        var receiver = new BankAccount("456", 50);

        // Act
        sender.Transfer(receiver, 25);

        // Assert
        Assert.Contains("Transfert", sender.TransactionHistory[0]);
        Assert.Contains("-25", sender.TransactionHistory[0]);
    }
    
    [Fact]
    public void Transfer_NullAmount_ThrowsArgumentException()
    {
        // Arrange
        var sender = new BankAccount("123", 100);

        // Act & Assert
        var ex = Assert.Throws<ArgumentNullException>(() => sender.Transfer(null, 50));
        Assert.Contains("destinationAccount", ex.Message); 
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void Transfer_InvalidAmount_ThrowsArgumentException(decimal invalidAmount)
    {
        // Arrange
        var sender = new BankAccount("123", 100);
        var receiver = new BankAccount("456", 50);

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => sender.Transfer(receiver, invalidAmount));
        Assert.Contains("positive", ex.Message);
    }
}