

public class BankAccount
{
    private string accountName;
    private int accountID;
    private decimal balance;
    private List<decimal> transactions;

    public BankAccount(string name, decimal initialBalance, int ID = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Error: Account name cannot be empty.");
            accountName = "Unknown";
        }
        else
        {
            accountName = name;
        }

        if (initialBalance < 0)
        {
            Console.WriteLine("Error: Initial balance cannot be negative. Setting balance to 0.");
            balance = 0;
        }
        else
        {
            balance = initialBalance;
        }

        accountID = ID;
        transactions = new List<decimal>();
    }

    public string AccountName { get { return accountName; } }
    public int AccountID { get { return accountID; } }
    public decimal Balance { get { return balance; } }
    public List<decimal> Transactions { get { return transactions; } }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Error: Deposit must be greater than 0.");
        }
        else
        {
            balance += amount;
            transactions.Add(amount);
            Console.WriteLine($"Deposited {amount:C}, New Balance: {balance:C}");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Error: Withdrawal must be greater than 0.");
        }
        else if (amount > balance)
        {
            Console.WriteLine("Error: Insufficient funds.");
        }
        else
        {
            balance -= amount;
            transactions.Add(-amount);
            Console.WriteLine($"Withdrew {amount:C}, New Balance: {balance:C}");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== TEST CASES ===\n");

        // CREATE ACCOUNT
        Console.WriteLine("Test 1: Create valid account with name 'Alice' and balance 50");
        var acct1 = new BankAccount("Alice", 50m, 1001);
        Console.WriteLine($"Created Account -> Name: {acct1.AccountName}, ID: {acct1.AccountID}, Balance: {acct1.Balance}\n");

        Console.WriteLine("Test 2: Create account with negative balance (-10)");
        var acct2 = new BankAccount("Bob", -10m);
        Console.WriteLine($"Created Account -> Name: {acct2.AccountName}, Balance: {acct2.Balance}\n");

        Console.WriteLine("Test 3: Create account with empty name");
        var acct3 = new BankAccount("   ", 0m);
        Console.WriteLine($"Created Account -> Name: {acct3.AccountName}, Balance: {acct3.Balance}\n");

        // DEPOSIT
        Console.WriteLine("Test 4: Deposit positive amount (25)");
        acct1.Deposit(25m);
        Console.WriteLine($"Balance After Deposit: {acct1.Balance}\n");

        Console.WriteLine("Test 5: Deposit zero (0)");
        acct1.Deposit(0m);
        Console.WriteLine($"Balance After Deposit: {acct1.Balance}\n");

        Console.WriteLine("Test 6: Deposit negative (-5)");
        acct1.Deposit(-5m);
        Console.WriteLine($"Balance After Deposit: {acct1.Balance}\n");

        // WITHDRAW
        Console.WriteLine("Test 7: Withdraw valid amount (20)");
        acct1.Withdraw(20m);
        Console.WriteLine($"Balance After Withdrawal: {acct1.Balance}\n");

        Console.WriteLine("Test 8: Withdraw zero (0)");
        acct1.Withdraw(0m);
        Console.WriteLine($"Balance After Withdrawal: {acct1.Balance}\n");

        Console.WriteLine("Test 9: Withdraw negative (-3)");
        acct1.Withdraw(-3m);
        Console.WriteLine($"Balance After Withdrawal: {acct1.Balance}\n");

        Console.WriteLine("Test 10: Withdraw more than balance (999)");
        acct1.Withdraw(999m);
        Console.WriteLine($"Balance After Withdrawal: {acct1.Balance}\n");

        // TRANSACTION HISTORY
        Console.WriteLine("Test 11: Multiple operations (Deposit 50, Withdraw 25, Deposit 10)");
        var acct4 = new BankAccount("Charlie", 100m, 1004);
        acct4.Deposit(50m);   // 150
        acct4.Withdraw(25m);  // 125
        acct4.Deposit(10m);   // 135
        Console.WriteLine($"Final Balance: {acct4.Balance}");
        Console.WriteLine("Transaction history:");
        foreach (var t in acct4.Transactions)
            Console.WriteLine($"  {t}");
        
        Console.WriteLine("\n=== END OF TESTS ===");
    }
}
