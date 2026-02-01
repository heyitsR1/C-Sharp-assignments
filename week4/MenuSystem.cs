using System;

namespace LibrarySystemWeek4;

/// <summary>
/// Provides the interactive command-line interface for library operations.
/// Orchestrates all user interactions and delegates business logic to LibraryService.
/// </summary>
public class MenuSystem
{
    private LibraryService _library;

    public MenuSystem(LibraryService library)
    {
        _library = library;
    }

    /// <summary>
    /// Starts the main application loop with menu display and command processing.
    /// Continues until user selects exit option.
    /// </summary>
    public void Start()
    {
        Console.WriteLine("\u001b[36m╔════════════════════════════════════════════╗\u001b[0m");
        Console.WriteLine("\u001b[36m║   Library Management System - Week 4       ║\u001b[0m");
        Console.WriteLine("\u001b[36m╚════════════════════════════════════════════╝\u001b[0m\n");

        bool running = true;
        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine() ?? "";

            try
            {
                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        AddMagazine();
                        break;
                    case "3":
                        AddNewspaper();
                        break;
                    case "4":
                        _library.DisplayAllItems();
                        break;
                    case "5":
                        SearchMenu();
                        break;
                    case "6":
                        SortMenu();
                        break;
                    case "7":
                        RemoveItem();
                        break;
                    case "8":
                        _library.DisplayStats();
                        break;
                    case "9":
                        running = false;
                        Console.WriteLine("\u001b[32m\nThanks for using Library Management System!\u001b[0m");
                        break;
                    default:
                        Console.WriteLine("\u001b[31m✗ Invalid choice. Please try again\u001b[0m\n");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ An error occurred: {ex.Message}\n");
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("\u001b[33m─ Main Menu ─\u001b[0m");
        Console.WriteLine("\u001b[36m1. Add Book\u001b[0m");
        Console.WriteLine("\u001b[36m2. Add Magazine\u001b[0m");
        Console.WriteLine("\u001b[36m3. Add Newspaper\u001b[0m");
        Console.WriteLine("\u001b[36m4. View All Items\u001b[0m");
        Console.WriteLine("\u001b[36m5. Search Items\u001b[0m");
        Console.WriteLine("\u001b[36m6. Sort Items\u001b[0m");
        Console.WriteLine("\u001b[36m7. Remove Item\u001b[0m");
        Console.WriteLine("\u001b[36m8. Library Statistics\u001b[0m");
        Console.WriteLine("\u001b[36m9. Exit\u001b[0m");
        Console.Write("\u001b[35mSelect option: \u001b[0m");
    }

    /// <summary>
    /// Prompts user for book details and adds it to the library.
    /// Validates all input before attempting to add.
    /// </summary>
    private void AddBook()
    {
        Console.WriteLine("\u001b[33m\n─ Add New Book ─\u001b[0m");
        try
        {
            string title = InputValidator.GetNonEmptyString("\u001b[36mBook title: \u001b[0m");
            string author = InputValidator.GetNonEmptyString("\u001b[36mAuthor name: \u001b[0m");
            string publisher = InputValidator.GetNonEmptyString("\u001b[36mPublisher: \u001b[0m");
            int year = InputValidator.GetValidYear("\u001b[36mPublication year: \u001b[0m");

            var book = new Book(title, author, publisher, year);
            _library.AddItem(book);
        }
        catch (InvalidItemDataException ex)
        {
            Console.WriteLine($"\u001b[31m✗ Cannot add book: {ex.Message}\u001b[0m");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Unexpected error: {ex.Message}\u001b[0m");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Prompts user for magazine details and adds it to the library.
    /// Validates all input before attempting to add.
    /// </summary>
    private void AddMagazine()
    {
        Console.WriteLine("\u001b[33m\n─ Add New Magazine ─\u001b[0m");
        try
        {
            string title = InputValidator.GetNonEmptyString("\u001b[36mMagazine title: \u001b[0m");
            int issueNum = InputValidator.GetValidNumber("\u001b[36mIssue number: \u001b[0m");
            string publisher = InputValidator.GetNonEmptyString("\u001b[36mPublisher: \u001b[0m");
            int year = InputValidator.GetValidYear("\u001b[36mPublication year: \u001b[0m");

            var magazine = new Magazine(title, issueNum, publisher, year);
            _library.AddItem(magazine);
        }
        catch (InvalidItemDataException ex)
        {
            Console.WriteLine($"\u001b[31m✗ Cannot add magazine: {ex.Message}\u001b[0m");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Unexpected error: {ex.Message}\u001b[0m");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Prompts user for newspaper details and adds it to the library.
    /// New item type - validates all input before attempting to add.
    /// </summary>
    private void AddNewspaper()
    {
        Console.WriteLine("\u001b[33m\n─ Add New Newspaper ─\u001b[0m");
        try
        {
            string title = InputValidator.GetNonEmptyString("\u001b[36mNewspaper title: \u001b[0m");
            string editor = InputValidator.GetNonEmptyString("\u001b[36mEditor name: \u001b[0m");
            string publisher = InputValidator.GetNonEmptyString("\u001b[36mPublisher: \u001b[0m");
            int year = InputValidator.GetValidYear("\u001b[36mPublication year: \u001b[0m");

            var newspaper = new Newspaper(title, editor, publisher, year);
            _library.AddItem(newspaper);
        }
        catch (InvalidItemDataException ex)
        {
            Console.WriteLine($"\u001b[31m✗ Cannot add newspaper: {ex.Message}\u001b[0m");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Unexpected error: {ex.Message}\u001b[0m");
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Provides submenu for different search options (title or author).
    /// </summary>
    private void SearchMenu()
    {
        Console.WriteLine("\u001b[33m\n─ Search Options ─\u001b[0m");
        Console.WriteLine("\u001b[36m1. Search by Title\u001b[0m");
        Console.WriteLine("\u001b[36m2. Search by Author\u001b[0m");
        Console.WriteLine("\u001b[36m3. Back to Main Menu\u001b[0m");
        Console.Write("\u001b[35mSelect option: \u001b[0m");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                SearchByTitle();
                break;
            case "2":
                SearchByAuthor();
                break;
            case "3":
                break;
            default:
                Console.WriteLine("\u001b[31m✗ Invalid choice\u001b[0m\n");
                break;
        }
    }

    /// <summary>
    /// Prompts for title and searches the library.
    /// </summary>
    private void SearchByTitle()
    {
        try
        {
            string title = InputValidator.GetNonEmptyString("\u001b[36mEnter title to search: \u001b[0m");
            _library.SearchByTitle(title);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Error: {ex.Message}\u001b[0m\n");
        }
    }

    /// <summary>
    /// Prompts for author name and searches for books by that author.
    /// </summary>
    private void SearchByAuthor()
    {
        try
        {
            string author = InputValidator.GetNonEmptyString("\u001b[36mEnter author name to search: \u001b[0m");
            _library.SearchByAuthor(author);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Error: {ex.Message}\u001b[0m\n");
        }
    }

    /// <summary>
    /// Provides submenu for different sorting options (title or year).
    /// </summary>
    private void SortMenu()
    {
        Console.WriteLine("\u001b[33m\n─ Sort Options ─\u001b[0m");
        Console.WriteLine("\u001b[36m1. Sort by Title\u001b[0m");
        Console.WriteLine("\u001b[36m2. Sort by Publication Year\u001b[0m");
        Console.WriteLine("\u001b[36m3. Back to Main Menu\u001b[0m");
        Console.Write("\u001b[35mSelect option: \u001b[0m");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                _library.SortByTitle();
                break;
            case "2":
                _library.SortByYear();
                break;
            case "3":
                break;
            default:
                Console.WriteLine("\u001b[31m✗ Invalid choice\u001b[0m\n");
                break;
        }
    }

    /// <summary>
    /// Prompts for item title and removes it from the library.
    /// </summary>
    private void RemoveItem()
    {
        Console.WriteLine("\u001b[33m\n─ Remove Item ─\u001b[0m");
        try
        {
            string title = InputValidator.GetNonEmptyString("\u001b[36mEnter title of item to remove: \u001b[0m");
            _library.RemoveItem(title);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Error: {ex.Message}\u001b[0m");
        }
        Console.WriteLine();
    }
}
