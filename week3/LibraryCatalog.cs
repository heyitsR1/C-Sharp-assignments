using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem;

// manages the library collection
public class LibraryCatalog
{
    private List<MediaItem> _inventory;
    private List<string> _registeredTitles; // track existing items to prevent duplicates

    public LibraryCatalog()
    {
        _inventory = new List<MediaItem>();
        _registeredTitles = new List<string>();
    }

    // adds item if it doesnt already exist
    public void AddItem(MediaItem item)
    {
        try
        {
            string itemKey = $"{item.Title.ToLower()}_{item.GetItemType()}";
            
            if (_registeredTitles.Contains(itemKey))
                throw new DuplicateItemException($"'{item.Title}' is already in the system");

            _inventory.Add(item);
            _registeredTitles.Add(itemKey);
            Console.WriteLine($"\u001b[32m✓ Added '{item.Title}' to library\u001b[0m");
        }
        catch (DuplicateItemException ex)
        {
            Console.WriteLine($"\u001b[31m✗ Error: {ex.Message}\u001b[0m");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Unexpected error: {ex.Message}\u001b[0m");
        }
    }

    // display all items in library
    public void DisplayAllItems()
    {
        if (_inventory.Count == 0)
        {
            Console.WriteLine("\u001b[33mLibrary is empty\u001b[0m");
            return;
        }

        Console.WriteLine($"\u001b[36m\n=== Library Inventory ({_inventory.Count} items) ===\u001b[0m");
        for (int i = 0; i < _inventory.Count; i++)
        {
            _inventory[i].DisplayInfo();
        }
        Console.WriteLine();
    }

    // find items by title
    public MediaItem? FindByTitle(string title)
    {
        return _inventory.FirstOrDefault(item => item.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    // count items by type
    public int CountByType(string itemType)
    {
        return _inventory.Count(item => item.GetItemType() == itemType);
    }

    public int TotalItems => _inventory.Count;
}
