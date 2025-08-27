using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InventoryManagementSystem
{
    /// <summary>
    /// A generic logger for inventory items that supports file-based persistence.
    /// </summary>
    /// <typeparam name="T">The type of inventory item, must implement IInventoryEntity</typeparam>
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private readonly List<T> _log = new();
        private readonly string _filePath;
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryLogger{T}"/> class.
        /// </summary>
        /// <param name="filePath">The path to the file where the inventory will be persisted.</param>
        public InventoryLogger(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            
            // Create directory if it doesn't exist
            string? directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        /// <summary>
        /// Gets the number of items in the log.
        /// </summary>
        public int Count => _log.Count;

        /// <summary>
        /// Adds an item to the inventory log.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <exception cref="ArgumentNullException">Thrown when the item is null.</exception>
        public void Add(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // Check for duplicate ID
            if (_log.Exists(i => i.Id == item.Id))
            {
                throw new InvalidOperationException($"An item with ID {item.Id} already exists in the inventory.");
            }

            _log.Add(item);
        }

        /// <summary>
        /// Gets all items in the inventory log.
        /// </summary>
        /// <returns>A read-only list of all items in the log.</returns>
        public IReadOnlyList<T> GetAll()
        {
            return _log.AsReadOnly();
        }

        /// <summary>
        /// Removes an item from the inventory by its ID.
        /// </summary>
        /// <param name="id">The ID of the item to remove.</param>
        /// <returns>True if the item was found and removed; otherwise, false.</returns>
        public bool RemoveItem(int id)
        {
            int index = _log.FindIndex(item => item.Id == id);
            if (index >= 0)
            {
                _log.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clears all items from the inventory.
        /// </summary>
        public void Clear()
        {
            _log.Clear();
        }

        /// <summary>
        /// Saves the current inventory to a file.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when serialization fails.</exception>
        /// <exception cref="IOException">Thrown when there's an I/O error while writing to the file.</exception>
        public void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(_log, _jsonOptions);
                File.WriteAllText(_filePath, json);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to serialize inventory data.", ex);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or NotSupportedException)
            {
                throw new IOException($"Failed to write to file: {_filePath}", ex);
            }
        }

        /// <summary>
        /// Loads inventory data from a file.
        /// </summary>
        /// <exception cref="FileNotFoundException">Thrown when the file doesn't exist.</exception>
        /// <exception cref="InvalidOperationException">Thrown when deserialization fails.</exception>
        /// <exception cref="IOException">Thrown when there's an I/O error while reading the file.</exception>
        public void LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException("Inventory file not found.", _filePath);
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                var items = JsonSerializer.Deserialize<List<T>>(json);
                
                if (items != null)
                {
                    _log.Clear();
                    _log.AddRange(items);
                }
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException("Failed to deserialize inventory data. The file may be corrupted.", ex);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or NotSupportedException)
            {
                throw new IOException($"Failed to read from file: {_filePath}", ex);
            }
        }
    }
}
