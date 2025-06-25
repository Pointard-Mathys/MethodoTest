namespace Bank;

public class BankAccount : IBankAccount
{
    //Passer Balance en public mais pas sont set
    public decimal Balance { get; private set; }
    public string AccountNumber { get; set; }
    public List<string> TransactionHistory { get; private set; }


    public BankAccount(string accountNumber, decimal initialBalance = decimal.Zero)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
            throw new ArgumentException("Account number cannot be null or empty");
            
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative");
        
        AccountNumber = accountNumber;
        Balance = initialBalance;
        TransactionHistory = new();
    }

    // Il faut s'assurer que le montant que l'on souhaite deposer soit strictement superieur a zero
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive.");
        }

        Balance += amount;
    }

    // Il faut que le montant pour retirer soit positif et
    // que le montant que l'on souhaite retirer soit suffisant par rapport a notre solde
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive.");
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance -= amount;
        TransactionHistory.Add($"Withdraw: -{amount} (Remaining: {Balance})");
    }

    public void Transfer(BankAccount destinationAccount, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(destinationAccount);

        if (amount <= 0)
            throw new ArgumentException("Transfer amount must be positive");
            
        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds for transfer");
            
        Balance -= amount;
        destinationAccount.Balance += amount;
        TransactionHistory.Add($"Transfert: -{amount:C}");
    }
}