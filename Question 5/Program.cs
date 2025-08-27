using System;
using System.IO;

namespace InventoryManagementSystem
{
    class Program
    {
        private const string DataFileName = "inventory_data.json";
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Inventory Management System";
            
            try
            {
                // Initialize the application
                var app = new InventoryApp(DataFileName);
                
                // Display welcome message
                Console.Clear();
                Console.WriteLine("=== INVENTORY MANAGEMENT SYSTEM ===\n");
                
                // Check if data file exists
                bool dataFileExists = File.Exists(DataFileName);
                
                if (dataFileExists)
                {
                    Console.WriteLine("Loading existing inventory data...");
                    app.LoadData();
                    Console.WriteLine("Data loaded successfully!");
                }
                else
                {
                    Console.WriteLine("No existing inventory data found.");
                    Console.Write("Would you like to seed with sample data? (Y/N): ");
                    
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        Console.WriteLine("\n\nSeeding with sample data...");
                        app.SeedSampleData();
                        app.SaveData();
                        Console.WriteLine("Sample data has been created and saved.");
                    }
                    else
                    {
                        Console.WriteLine("\n\nStarting with an empty inventory.");
                    }
                }
                
                // Main menu loop
                bool exitRequested = false;
                while (!exitRequested)
                {
                    DisplayMenu();
                    
                    Console.Write("\nEnter your choice (1-5): ");
                    var key = Console.ReadKey();
                    Console.WriteLine();
                    
                    try
                    {
                        switch (key.KeyChar)
                        {
                            case '1': // View all items
                                Console.Clear();
                                app.PrintAllItems();
                                break;
                                
                            case '2': // Add sample data
                                Console.Clear();
                                Console.WriteLine("Adding sample data...");
                                app.SeedSampleData();
                                app.SaveData();
                                Console.WriteLine("Sample data has been added and saved.");
                                break;
                                
                            case '3': // Save data
                                Console.Clear();
                                Console.WriteLine("Saving data...");
                                app.SaveData();
                                Console.WriteLine("Data saved successfully!");
                                break;
                                
                            case '4': // Reload data
                                Console.Clear();
                                Console.WriteLine("Reloading data...");
                                app.LoadData();
                                Console.WriteLine("Data reloaded successfully!");
                                break;
                                
                            case '5': // Exit
                                Console.WriteLine("Exiting...");
                                exitRequested = true;
                                break;
                                
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\n❌ Error: {ex.Message}");
                        if (ex.InnerException != null)
                        {
                            Console.WriteLine($"Details: {ex.InnerException.Message}");
                        }
                    }
                    
                    if (!exitRequested)
                    {
                        Console.WriteLine("\nPress any key to continue...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ A critical error occurred: {ex.Message}");
                Console.WriteLine("The application will now exit.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        
        private static void DisplayMenu()
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1. View all inventory items");
            Console.WriteLine("2. Add sample data");
            Console.WriteLine("3. Save data to file");
            Console.WriteLine("4. Reload data from file");
            Console.WriteLine("5. Exit");
        }
    }
}
