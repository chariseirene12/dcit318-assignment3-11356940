namespace InventoryManagementSystem
{
    /// <summary>
    /// Marker interface for inventory entities
    /// </summary>
    public interface IInventoryEntity
    {
        /// <summary>
        /// Gets the unique identifier of the inventory entity
        /// </summary>
        int Id { get; }
    }
}
