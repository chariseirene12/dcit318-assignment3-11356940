using System;
using System.Collections.Generic;
using System.Linq;

public class Repository<T> where T : class
{
    private readonly List<T> _items = new();

    /// <summary>
    /// Adds a new item to the repository.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void Add(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
            
        _items.Add(item);
    }

    /// <summary>
    /// Retrieves all items from the repository.
    /// </summary>
    /// <returns>A list of all items.</returns>
    public List<T> GetAll()
    {
        return new List<T>(_items);
    }

    /// <summary>
    /// Finds the first item that matches the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to match.</param>
    /// <returns>The first matching item, or null if no match is found.</returns>
    public T? GetById(Func<T, bool> predicate)
    {
        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));
            
        return _items.FirstOrDefault(predicate);
    }

    /// <summary>
    /// Removes all items that match the specified predicate.
    /// </summary>
    /// <param name="predicate">The condition to match.</param>
    /// <returns>True if any items were removed; otherwise, false.</returns>
    public bool Remove(Func<T, bool> predicate)
    {
        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));
            
        var itemsToRemove = _items.Where(predicate).ToList();
        
        if (itemsToRemove.Count == 0)
            return false;
            
        foreach (var item in itemsToRemove)
        {
            _items.Remove(item);
        }
        
        return true;
    }
}
