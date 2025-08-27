using System;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Represents an immutable inventory item with an ID, name, quantity, and date added.
    /// </summary>
    /// <param name="Id">The unique identifier of the item.</param>
    /// <param name="Name">The name of the item.</param>
    /// <param name="Quantity">The quantity of the item in stock.</param>
    /// <param name="DateAdded">The date and time when the item was added to inventory.</param>
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity
    {
        /// <summary>
        /// Gets a value indicating whether the item is in stock (quantity > 0).
        /// </summary>
        public bool IsInStock => Quantity > 0;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Quantity: {Quantity}, Added: {DateAdded:yyyy-MM-dd}, In Stock: {(IsInStock ? "Yes" : "No")}";
        }
    }
}
