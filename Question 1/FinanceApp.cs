using System;
using System.Collections.Generic;

public class FinanceApp
{
    private readonly List<Transaction> _transactions = new();

    public void Run()
    {
        // Initialize account with initial balance
        var account = new SavingsAccount("SAV123456", 1000.00m);
        Console.WriteLine($"Account {account.AccountNumber} created with initial balance: {account.Balance:C}");

        // Create sample transactions
        var transactions = new[]
        {
            new Transaction { Id = 1, Date = DateTime.Now, Amount = 150.50m, Category = "Groceries" },
            new Transaction { Id = 2, Date = DateTime.Now, Amount = 75.25m, Category = "Utilities" },
            new Transaction { Id = 3, Date = DateTime.Now, Amount = 50.00m, Category = "Entertainment" },
            // Test insufficient funds
            new Transaction { Id = 4, Date = DateTime.Now, Amount = 1000.00m, Category = "Large Purchase" }
        };

        // Initialize processors
        ITransactionProcessor[] processors = 
        {
            new MobileMoneyProcessor(),
            new BankTransferProcessor(),
            new CryptoWalletProcessor(),
            new BankTransferProcessor() // For the fourth transaction
        };

        // Process each transaction
        for (int i = 0; i < transactions.Length; i++)
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine($"PROCESSING TRANSACTION {transactions[i].Id}");
            Console.WriteLine($"Date: {transactions[i].Date}");
            Console.WriteLine($"Category: {transactions[i].Category}");
            Console.WriteLine($"Amount: {transactions[i].Amount:C}");
            Console.WriteLine(new string('-', 30));
            
            processors[i].Process(transactions[i]);
            account.ApplyTransaction(transactions[i]);
            _transactions.Add(transactions[i]);
            
            Console.WriteLine($"Current Balance: {account.Balance:C}");
            Console.WriteLine(new string('=', 50));
        }

        // Display final summary
        Console.WriteLine("\n--- Transaction Summary ---");
        Console.WriteLine($"Total transactions processed: {_transactions.Count}");
        Console.WriteLine($"Final account balance: {account.Balance:C}");
    }

    public static void Main()
    {
        try
        {
            // Set console encoding and buffer size for better output
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WindowWidth = Math.Min(120, Console.LargestWindowWidth);
            Console.BufferWidth = Console.WindowWidth;
            Console.Title = "Finance Management System";
            
            var app = new FinanceApp();
            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
