using System;

namespace LibrarySystem;

// handles input from user with validation
public class InputValidator
{
    public static int GetValidYear(string prompt)
    {
        while (true)
        {
            try
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";
                
                if (!int.TryParse(input, out int year))
                    throw new InvalidItemDataException("year must be a valid number");
                
                if (year < 1000 || year > DateTime.Now.Year)
                    throw new InvalidItemDataException($"year must be between 1000 and {DateTime.Now.Year}");
                
                return year;
            }
            catch (InvalidItemDataException ex)
            {
                Console.WriteLine($"  \u001b[31m✗ {ex.Message}. try again\u001b[0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  \u001b[31m✗ something went wrong: {ex.Message}\u001b[0m");
            }
        }
    }

    public static int GetValidNumber(string prompt, int minValue = 1)
    {
        while (true)
        {
            try
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";
                
                if (!int.TryParse(input, out int num))
                    throw new InvalidItemDataException("must be a valid number");
                
                if (num < minValue)
                    throw new InvalidItemDataException($"must be at least {minValue}");
                
                return num;
            }
            catch (InvalidItemDataException ex)
            {
                Console.WriteLine($"  \u001b[31m✗ {ex.Message}. try again\u001b[0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  \u001b[31m✗ error: {ex.Message}\u001b[0m");
            }
        }
    }

    public static string GetNonEmptyString(string prompt)
    {
        while (true)
        {
            try
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";
                
                if (string.IsNullOrWhiteSpace(input))
                    throw new InvalidItemDataException("cannot be empty");
                
                return input.Trim();
            }
            catch (InvalidItemDataException ex)
            {
                Console.WriteLine($"  \u001b[31m✗ {ex.Message}. try again\u001b[0m");
            }
        }
    }
}

// interactive menu for the library system
public class MenuSystem
{
    private LibraryCatalog _library;

    public MenuSystem()
    {
        _library = new LibraryCatalog();
    }

    public void Start()
    {
        Console.WriteLine("\u001b[36m╔════════════════════════════════════╗\u001b[0m");
        Console.WriteLine("\u001b[36m║   Library Management System v1.0   ║\u001b[0m");
        Console.WriteLine("\u001b[36m╚════════════════════════════════════╝\u001b[0m\n");

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
                        _library.DisplayAllItems();
                        break;
                    case "4":
                        SearchItem();
                        break;
                    case "5":
                        ShowStats();
                        break;
                    case "6":
                        running = false;
                        Console.WriteLine("\u001b[32m\nThanks for using Library System!\u001b[0m");
                        break;
                    default:
                        Console.WriteLine("\u001b[31m✗ Invalid choice. please try again\u001b[0m\n");
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
        Console.WriteLine("\u001b[36m3. View All Items\u001b[0m");
        Console.WriteLine("\u001b[36m4. Search Item\u001b[0m");
        Console.WriteLine("\u001b[36m5. Library Stats\u001b[0m");
        Console.WriteLine("\u001b[36m6. Exit\u001b[0m");
        Console.Write("\u001b[35mSelect option: \u001b[0m");
    }

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

    private void SearchItem()
    {
        Console.WriteLine("\u001b[33m\n─ Search ─\u001b[0m");
        try
        {
            string title = InputValidator.GetNonEmptyString("\u001b[36mEnter title to search: \u001b[0m");
            var found = _library.FindByTitle(title);

            if (found != null)
            {
                Console.WriteLine("\u001b[32m\nFound:\u001b[0m");
                found.DisplayInfo();
            }
            else
            {
                Console.WriteLine($"\u001b[31m✗ No item found with title '{title}'\u001b[0m");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31m✗ Error: {ex.Message}\u001b[0m");
        }
        Console.WriteLine();
    }

    private void ShowStats()
    {
        Console.WriteLine($"\u001b[33m\n─ Library Statistics ─\u001b[0m");
        Console.WriteLine($"\u001b[36mTotal items: \u001b[32m{_library.TotalItems}\u001b[0m");
        Console.WriteLine($"\u001b[36mBooks: \u001b[32m{_library.CountByType("Book")}\u001b[0m");
        Console.WriteLine($"\u001b[36mMagazines: \u001b[32m{_library.CountByType("Magazine")}\u001b[0m");
        Console.WriteLine();
    }
}
