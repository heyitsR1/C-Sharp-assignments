using System;

namespace LibrarySystemWeek4;

class Program
{
    static void Main()
    {
        try
        {
            // Initialize library service which loads existing data from file
            var library = new LibraryService();
            library.LoadData();

            // Start the interactive menu with loaded library data
            var menu = new MenuSystem(library);
            menu.Start();

            // Save any final changes made during the session
            library.SaveData();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\u001b[31mFatal error: {ex.Message}\u001b[0m");
            Environment.Exit(1);
        }
    }
}
