using System;

namespace LibrarySystem;

class Program
{
    static void Main()
    {
        try
        {
            var menu = new MenuSystem();
            menu.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
            Environment.Exit(1);
        }
    }
}
