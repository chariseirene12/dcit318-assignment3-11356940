namespace WarehouseInventorySystem
{
    /// <summary>
    /// Marker interface for all inventory items
    /// </summary>
    public interface IInventoryItem
    {
        int Id { get; }
        string Name { get; }
        int Quantity { get; set; }
    }
}
