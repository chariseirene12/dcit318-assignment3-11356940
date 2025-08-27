using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Main application class for managing inventory operations
    /// </summary>
    public class InventoryApp
    {
        private readonly InventoryLogger<InventoryItem> _logger;
        private const string DefaultFileName = "inventory.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryApp"/> class.
        /// </summary>
        /// <param name="filePath">Optional file path for storing inventory data. Defaults to 'inventory.json' in the application directory.</param>
        public InventoryApp(string? filePath = null)
        {
            _logger = new InventoryLogger<InventoryItem>(filePath ?? DefaultFileName);
        }

        /// <summary>
        /// Seeds the inventory with sample data.
        /// </summary>
        public void SeedSampleData()
        {
            try
            {
                // Clear any existing data
                if (_logger.Count > 0)
                {
                    Console.WriteLine("Clearing existing inventory data...");
                }

                // Add sample items
                var items = new List<InventoryItem>
                {
                    new(1, "Laptop", 10, DateTime.Now.AddDays(-30)),
                    new(2, "Smartphone", 25, DateTime.Now.AddDays(-15)),
                    new(3, "Headphones", 50, DateTime.Now.AddDays(-7)),
                    new(4, "Tablet", 15, DateTime.Now.AddDays(-3)),
                    new(5, "Smartwatch", 30, DateTime.Now.AddDays(-1))
                };

                // Clear existing items
                _logger.GetAll().ToList().ForEach(item => _logger.RemoveItem(item.Id));
                
                // Add new items
                foreach (var item in items)
                {
                    _logger.Add(item);
                }

                Console.WriteLine($"Added {items.Count} sample items to inventory.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding sample data: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Saves the current inventory to a file.
        /// </summary>
        public void SaveData()
        {
            try
            {
                _logger.SaveToFile();
                Console.WriteLine($"Successfully saved {_logger.Count} items to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Loads inventory data from a file.
        /// </summary>
        public void LoadData()
        {
            try
            {
                int countBefore = _logger.Count;
                _logger.LoadFromFile();
                int countAfter = _logger.Count;
                
                if (countAfter > countBefore)
                {
                    Console.WriteLine($"Loaded {countAfter - countBefore} items from file.");
                }
                else if (countAfter == 0 && countBefore == 0)
                {
                    Console.WriteLine("No items found in the inventory file.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No existing inventory file found. Starting with an empty inventory.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Prints all items in the inventory to the console.
        /// </summary>
        public void PrintAllItems()
        {
            var items = _logger.GetAll().ToList();
            
            if (items.Count == 0)
            {
                Console.WriteLine("No items in inventory.");
                return;
            }

            Console.WriteLine("\n=== INVENTORY ITEMS ===");
            Console.WriteLine($"{"ID",-5} | {"Name",-15} | {"Quantity",-8} | {"Added On",-15} | In Stock");
            Console.WriteLine(new string('-', 60));
            
            foreach (var item in items.OrderBy(i => i.Id))
            {
                Console.WriteLine($"{item.Id,-5} | {item.Name,-15} | {item.Quantity,-8} | {item.DateAdded:yyyy-MM-dd} | {(item.IsInStock ? "Yes" : "No")}");
            }
            
            // Print summary
            Console.WriteLine("\n=== SUMMARY ===");
            Console.WriteLine($"Total Items: {items.Count}");
            Console.WriteLine($"Total Quantity: {items.Sum(i => i.Quantity)}");
            Console.WriteLine($"In Stock: {items.Count(i => i.IsInStock)} of {items.Count}");
        }

        // Helper method to remove an item by ID (for internal use)
        private void RemoveItem(int id)
        {
            _logger.RemoveItem(id);
        }
    }
}
