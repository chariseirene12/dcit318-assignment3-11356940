using System;
using WarehouseInventorySystem;

class Program
{
    static void Main()
    {
        try
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Warehouse Inventory Management System";
            Console.Clear();
            
            Console.WriteLine("=== Warehouse Inventory Management System ===\n");
            
            // Initialize the warehouse manager
            var warehouse = new WareHouseManager();
            
            // Seed with sample data
            Console.WriteLine("Loading sample data...\n");
            warehouse.SeedData();
            
            // Print all items
            Console.WriteLine("\n=== Current Inventory ===");
            warehouse.PrintAllItems(warehouse.Electronics);
            warehouse.PrintAllItems(warehouse.Groceries);
            
            // Demonstrate operations with exception handling
            Console.WriteLine("\n=== Testing Operations ===");
            
            // 1. Try to add a duplicate item
            Console.WriteLine("\n1. Attempting to add a duplicate electronic item...");
            try
            {
                warehouse.Electronics.AddItem(new ElectronicItem(1, "Smart Watch", 15, "Samsung", 12));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Expected error: {ex.Message}");
            }
            
            // 2. Try to remove a non-existent item
            Console.WriteLine("\n2. Attempting to remove a non-existent item...");
            try
            {
                warehouse.RemoveItemById(warehouse.Electronics, 999);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Expected error: {ex.Message}");
            }
            
            // 3. Try to update with invalid quantity
            Console.WriteLine("\n3. Attempting to update with invalid quantity...");
            try
            {
                warehouse.Electronics.UpdateQuantity(1, -5);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Expected error: {ex.Message}");
            }
            
            // 4. Perform valid operations
            Console.WriteLine("\n4. Performing valid operations...");
            
            // Add a new electronic item
            warehouse.Electronics.AddItem(new ElectronicItem(4, "Tablet", 30, "Apple", 12));
            Console.WriteLine("Added new tablet to electronics.");
            
            // Increase stock of an existing item
            warehouse.IncreaseStock(warehouse.Groceries, 101, 50);
            
            // Remove an item
            warehouse.RemoveItemById(warehouse.Electronics, 2);
            
            // Print final inventory
            Console.WriteLine("\n=== Final Inventory ===");
            warehouse.PrintAllItems(warehouse.Electronics);
            warehouse.PrintAllItems(warehouse.Groceries);
            
            Console.WriteLine("\n=== All operations completed successfully! ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nAn unexpected error occurred: {ex.Message}");
        }
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
