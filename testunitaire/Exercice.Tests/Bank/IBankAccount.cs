namespace Bank;

public interface IBankAccount
{
    /// <summary>
    /// Adds a specified amount to the account balance.
    /// </summary>
    /// <param name="amount">The amount to deposit into the account. Must be a positive value.</param>
    public void Deposit(decimal amount);

    /// <summary>
    /// Deducts a specified amount from the account balance.
    /// </summary>
    /// <param name="amount">The amount to withdraw from the account. Must be a positive value and not exceed the available balance.</param>
    public void Withdraw(decimal amount);

    /// <summary>
    /// Transfers a specified amount from the current account to a destination account.
    /// </summary>
    /// <param name="destinationAccount">The account to which the amount will be transferred. Cannot be null.</param>
    /// <param name="amount">The amount to transfer. Must be a positive value and not exceed the current account balance.</param>
    public void Transfer(BankAccount destinationAccount, decimal amount);
}