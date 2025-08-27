using System;
using System.Collections.Generic;

namespace WarehouseInventorySystem
{
    /// <summary>
    /// Manages warehouse operations for both electronic and grocery items
    /// </summary>
    public class WareHouseManager
    {
        private readonly InventoryRepository<ElectronicItem> _electronics;
        private readonly InventoryRepository<GroceryItem> _groceries;

        public WareHouseManager()
        {
            _electronics = new InventoryRepository<ElectronicItem>();
            _groceries = new InventoryRepository<GroceryItem>();
        }

        /// <summary>
        /// Seeds the system with sample data
        /// </summary>
        public void SeedData()
        {
            try
            {
                // Add sample electronic items
                _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Dell", 24));
                _electronics.AddItem(new ElectronicItem(2, "Smartphone", 25, "Samsung", 12));
                _electronics.AddItem(new ElectronicItem(3, "Headphones", 50, "Sony", 6));

                // Add sample grocery items
                _groceries.AddItem(new GroceryItem(101, "Milk", 100, DateTime.Today.AddDays(7)));
                _groceries.AddItem(new GroceryItem(102, "Bread", 75, DateTime.Today.AddDays(3)));
                _groceries.AddItem(new GroceryItem(103, "Eggs", 200, DateTime.Today.AddDays(14)));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Prints all items of a specific type
        /// </summary>
        /// <typeparam name="T">Type of items to print</typeparam>
        /// <param name="repo">Repository containing the items</param>
        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            var items = repo.GetAllItems();
            Console.WriteLine($"\n=== {typeof(T).Name.ToUpper()} INVENTORY (Total: {items.Count}) ===");
            
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// Increases the stock quantity of an item
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="repo">Repository containing the item</param>
        /// <param name="id">ID of the item</param>
        /// <param name="quantity">Quantity to add</param>
        public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id);
                int newQuantity = item.Quantity + quantity;
                repo.UpdateQuantity(id, newQuantity);
                Console.WriteLine($"Updated {typeof(T).Name} ID {id}: New quantity = {newQuantity}");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        /// <summary>
        /// Removes an item by ID
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="repo">Repository containing the item</param>
        /// <param name="id">ID of the item to remove</param>
        public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
        {
            try
            {
                bool removed = repo.RemoveItem(id);
                if (removed)
                {
                    Console.WriteLine($"{typeof(T).Name} with ID {id} was successfully removed.");
                }
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        // Public access to repositories for the main program
        public InventoryRepository<ElectronicItem> Electronics => _electronics;
        public InventoryRepository<GroceryItem> Groceries => _groceries;
    }
}
