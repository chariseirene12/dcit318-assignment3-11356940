using System;

namespace WarehouseInventorySystem
{
    /// <summary>
    /// Exception thrown when attempting to add an item with an ID that already exists
    /// </summary>
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(int id) 
            : base($"An item with ID {id} already exists in the inventory.")
        {
        }

        public DuplicateItemException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Exception thrown when an item is not found in the inventory
    /// </summary>
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(int id) 
            : base($"Item with ID {id} was not found in the inventory.")
        {
        }

        public ItemNotFoundException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Exception thrown when an invalid quantity is provided
    /// </summary>
    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException() 
            : base("Quantity cannot be negative.")
        {
        }

        public InvalidQuantityException(string message) : base(message)
        {
        }
    }
}
