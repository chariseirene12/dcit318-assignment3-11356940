namespace WarehouseInventorySystem
{
    /// <summary>
    /// Represents an electronic item in the inventory
    /// </summary>
    public class ElectronicItem : IInventoryItem
    {
        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public string Brand { get; }
        public int WarrantyMonths { get; }

        public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
        {
            if (quantity < 0)
                throw new InvalidQuantityException();
                
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity;
            Brand = brand ?? throw new ArgumentNullException(nameof(brand));
            WarrantyMonths = warrantyMonths;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Brand: {Brand}, Quantity: {Quantity}, Warranty: {WarrantyMonths} months";
        }
    }

    /// <summary>
    /// Represents a grocery item in the inventory
    /// </summary>
    public class GroceryItem : IInventoryItem
    {
        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; }

        public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
        {
            if (quantity < 0)
                throw new InvalidQuantityException();
                
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity;
            ExpiryDate = expiryDate;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Quantity: {Quantity}, Expires: {ExpiryDate:yyyy-MM-dd}";
        }
    }
}
