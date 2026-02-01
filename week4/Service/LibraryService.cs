using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LibrarySystemWeek4;


/// Manages the library's catalog, file persistence, and data operations.
/// Handles adding, removing, searching, and sorting items while maintaining data consistency.

public class LibraryService
{
    private List<ILibraryItem> items;
    private const string FileName = "libraryFile.json";

    public LibraryService()
    {
        items = new List<ILibraryItem>();
    }

    /// Adds a new item to the library after validation.
    /// Checks if file is empty on first addition, then checks for duplicates before persisting.
    public void AddItem(ILibraryItem item)
    {
        try
        {
            // If this is the first item being added, skip duplicate check and add directly
            if (items.Count == 0)
            {
                items.Add(item);
                Console.WriteLine($"\u001b[32m✓ Added '{item.Title}' to library\u001b[0m");
            }
            else
            {
                // Verify item doesn't already exist before allowing addition
                if (CheckForDuplicates(item))
                {
                    throw new DuplicateItemException($"'{item.Title}' already exists in the library");
                }
                items.Add(item);
                Console.WriteLine($"\u001b[32m✓ Added '{item.Title}' to library\u001b[0m");
            }
            SaveData();
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

    /// Removes an item by title and persists the change.
    public void RemoveItem(string title)
    {
        try
        {
            var item = items.FirstOrDefault(i => i.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                items.Remove(item);
                Console.WriteLine($"\u001b[32m✓ Removed '{title}' from library\u001b[0m");
                SaveData();
            }
            else
            {
                Console.WriteLine($"\u001b[31m✗ Item '{title}' not found\u001b[0m");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Error: {ex.Message}\u001b[0m");
        }
    }

    /// Displays all items in the library with formatted output.
    public void DisplayAllItems()
    {
        if (items.Count == 0)
        {
            Console.WriteLine("\u001b[33mLibrary is empty\u001b[0m");
            return;
        }

        Console.WriteLine($"\u001b[36m\n=== Library Inventory ({items.Count} items) ===\u001b[0m");
        foreach (var item in items)
        {
            item.DisplayInfo();
        }
        Console.WriteLine();
    }

    /// Searches for items by title (case-insensitive substring match).
    public void SearchByTitle(string title)
    {
        try
        {
            var results = items.Where(i => i.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            
            if (results.Count == 0)
            {
                Console.WriteLine($"\u001b[31m✗ No items found with title containing '{title}'\u001b[0m\n");
                return;
            }

            Console.WriteLine($"\u001b[32m\nFound {results.Count} item(s):\u001b[0m");
            foreach (var item in results)
            {
                item.DisplayInfo();
            }
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Search error: {ex.Message}\u001b[0m\n");
        }
    }

    /// Searches for items by author (only applies to Books, case-insensitive).
    public void SearchByAuthor(string author)
    {
        try
        {
            var results = items.OfType<Book>()
                .Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase))
                .Cast<ILibraryItem>()
                .ToList();
            
            if (results.Count == 0)
            {
                Console.WriteLine($"\u001b[31m✗ No books found by author containing '{author}'\u001b[0m\n");
                return;
            }

            Console.WriteLine($"\u001b[32m\nFound {results.Count} book(s):\u001b[0m");
            foreach (var item in results)
            {
                item.DisplayInfo();
            }
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Search error: {ex.Message}\u001b[0m\n");
        }
    }

    /// Sorts and displays all items by title in alphabetical order.
    public void SortByTitle()
    {
        try
        {
            if (items.Count == 0)
            {
                Console.WriteLine("\u001b[33mLibrary is empty\u001b[0m");
                return;
            }

            var sorted = items.OrderBy(i => i.Title).ToList();
            Console.WriteLine($"\u001b[36m\n=== Items Sorted by Title ({sorted.Count} items) ===\u001b[0m");
            foreach (var item in sorted)
            {
                item.DisplayInfo();
            }
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Sort error: {ex.Message}\u001b[0m\n");
        }
    }

    /// Sorts and displays all items by publication year in ascending order.
    public void SortByYear()
    {
        try
        {
            if (items.Count == 0)
            {
                Console.WriteLine("\u001b[33mLibrary is empty\u001b[0m");
                return;
            }

            var sorted = items.OrderBy(i => i.PublicationYear).ToList();
            Console.WriteLine($"\u001b[36m\n=== Items Sorted by Publication Year ({sorted.Count} items) ===\u001b[0m");
            foreach (var item in sorted)
            {
                item.DisplayInfo();
            }
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Sort error: {ex.Message}\u001b[0m\n");
        }
    }

    /// Displays library statistics (total count by type).
    public void DisplayStats()
    {
        Console.WriteLine($"\u001b[33m\n─ Library Statistics ─\u001b[0m");
        Console.WriteLine($"\u001b[36mTotal items: \u001b[32m{items.Count}\u001b[0m");
        Console.WriteLine($"\u001b[36mBooks: \u001b[32m{items.Count(i => i.GetItemType() == "Book")}\u001b[0m");
        Console.WriteLine($"\u001b[36mMagazines: \u001b[32m{items.Count(i => i.GetItemType() == "Magazine")}\u001b[0m");
        Console.WriteLine($"\u001b[36mNewspapers: \u001b[32m{items.Count(i => i.GetItemType() == "Newspaper")}\u001b[0m");
        Console.WriteLine();
    }

    /// Iterates through inventory to find duplicate items using refactored comparison logic.
    private bool CheckForDuplicates(ILibraryItem newItem)
    {
        foreach (var existing in items)
        {
            if (IsDuplicate(existing, newItem))
                return true;
        }
        return false;
    }

    /// Compares two items for duplication by type and properties.
    /// Returns immediately if items are different types (prevents false positives).
    /// Then validates base properties common to all items.
    /// Finally checks type-specific properties unique to each item class.
    private bool IsDuplicate(ILibraryItem existing, ILibraryItem newItem)
    {
        // Different types cannot be duplicates
        if (existing.GetItemType() != newItem.GetItemType())
            return false;

        // Check base properties common to all library items
        bool basePropertiesMatch = 
            existing.Title.Equals(newItem.Title, StringComparison.OrdinalIgnoreCase) &&
            existing.Publisher.Equals(newItem.Publisher, StringComparison.OrdinalIgnoreCase) &&
            existing.PublicationYear == newItem.PublicationYear;

        if (!basePropertiesMatch)
            return false;

        // Check type-specific properties to ensure complete match
        if (existing is Book existingBook && newItem is Book newBook)
        {
            return existingBook.Author.Equals(newBook.Author, StringComparison.OrdinalIgnoreCase);
        }

        if (existing is Magazine existingMag && newItem is Magazine newMag)
        {
            return existingMag.IssueNumber == newMag.IssueNumber;
        }

        if (existing is Newspaper existingNews && newItem is Newspaper newNews)
        {
            return existingNews.Editor.Equals(newNews.Editor, StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

    /// <summary>
    /// Persists library data to JSON file with polymorphic type information.
    /// Uses TypeNameHandling to preserve derived type information (Book, Magazine, Newspaper).
    /// </summary>
    public void SaveData()
    {
        try
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };

            string json = JsonConvert.SerializeObject(items, settings);
            File.WriteAllText(FileName, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Error saving data: {ex.Message}\u001b[0m");
        }
    }

    /// <summary>
    /// Loads library data from JSON file on application startup.
    /// Uses polymorphic deserialization to reconstruct Book, Magazine, and Newspaper objects.
    /// </summary>
    public void LoadData()
    {
        try
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };

                // Deserialize to object array first, then cast each item to ILibraryItem
                var loadedItems = JsonConvert.DeserializeObject<List<object>>(json, settings);
                if (loadedItems != null && loadedItems.Count > 0)
                {
                    items = loadedItems.Cast<ILibraryItem>().ToList();
                    Console.WriteLine($"\u001b[32m✓ Loaded {items.Count} item(s) from library file\u001b[0m\n");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Error loading library data: {ex.Message}\u001b[0m\n");
        }
    }

    public int TotalItems => items.Count;
}
