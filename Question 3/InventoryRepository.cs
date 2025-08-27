using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseInventorySystem
{
    /// <summary>
    /// Generic repository for managing inventory items
    /// </summary>
    /// <typeparam name="T">Type of inventory item (must implement IInventoryItem)</typeparam>
    public class InventoryRepository<T> where T : IInventoryItem
    {
        private readonly Dictionary<int, T> _items = new();

        /// <summary>
        /// Adds a new item to the inventory
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <exception cref="ArgumentNullException">Thrown when item is null</exception>
        /// <exception cref="DuplicateItemException">Thrown when item with same ID already exists</exception>
        public void AddItem(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (_items.ContainsKey(item.Id))
                throw new DuplicateItemException(item.Id);

            _items[item.Id] = item;
        }

        /// <summary>
        /// Retrieves an item by its ID
        /// </summary>
        /// <param name="id">ID of the item to retrieve</param>
        /// <returns>The item with the specified ID</returns>
        /// <exception cref="ItemNotFoundException">Thrown when item is not found</exception>
        public T GetItemById(int id)
        {
            if (!_items.TryGetValue(id, out var item))
                throw new ItemNotFoundException(id);

            return item;
        }

        /// <summary>
        /// Removes an item from the inventory by ID
        /// </summary>
        /// <param name="id">ID of the item to remove</param>
        /// <returns>True if item was removed, false otherwise</returns>
        /// <exception cref="ItemNotFoundException">Thrown when item is not found</exception>
        public bool RemoveItem(int id)
        {
            if (!_items.ContainsKey(id))
                throw new ItemNotFoundException(id);

            return _items.Remove(id);
        }

        /// <summary>
        /// Gets all items in the inventory
        /// </summary>
        /// <returns>List of all items</returns>
        public List<T> GetAllItems()
        {
            return _items.Values.ToList();
        }

        /// <summary>
        /// Updates the quantity of an item
        /// </summary>
        /// <param name="id">ID of the item to update</param>
        /// <param name="newQuantity">New quantity value</param>
        /// <exception cref="ItemNotFoundException">Thrown when item is not found</exception>
        /// <exception cref="InvalidQuantityException">Thrown when quantity is negative</exception>
        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0)
                throw new InvalidQuantityException("Quantity cannot be negative.");

            var item = GetItemById(id); // This will throw ItemNotFoundException if not found
            item.Quantity = newQuantity;
        }
    }
}
