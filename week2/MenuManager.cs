using System;

// ===== MENU MANAGER CLASS =====
// Centralizes all menu UI displays for the application
// Provides consistent styling and formatting across all menus
// This modular approach makes it easy to update UI globally

class MenuManager
{
    // ===== MAIN MENU DISPLAY =====
    // Shows the list of available projects/systems to the user
    // Uses colored text for better UI appearance and clarity
    public static void ShowMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n==========================================================");
        Console.WriteLine("     MULTI-SYSTEM APPLICATION - Main Menu     ");
        Console.WriteLine("==========================================================");
        Console.WriteLine("1. Simple Banking System");
        Console.WriteLine("2. Task Management System");
        Console.WriteLine("3. Student Grade Management System");
        Console.WriteLine("4. Exit Application");
        Console.Write("Choose an option: ");
        Console.ResetColor();
    }

    // ===== BANKING MENU DISPLAY =====
    // Displays the banking system menu options
    public static void ShowBankingMenu()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n===== SIMPLE BANKING SYSTEM =====");
        Console.WriteLine("1. Deposit");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Check Balance");
        Console.WriteLine("4. Exit Banking");
        Console.Write("Choose an option: ");
        Console.ResetColor();
    }

    // ===== TASK MANAGEMENT MENU DISPLAY =====
    // Displays all task management options
    public static void ShowTaskMenu()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n*****************************");
        Console.WriteLine("  TASK MANAGEMENT SYSTEM  ");
        Console.WriteLine("*****************************");
        Console.WriteLine("1. Add Task");
        Console.WriteLine("2. View All Tasks");
        Console.WriteLine("3. Mark Task as Completed");
        Console.WriteLine("4. Delete Task");
        Console.WriteLine("5. Filter Tasks by Priority");
        Console.WriteLine("6. Sort Tasks by Due Date");
        Console.WriteLine("7. Save Tasks to File");
        Console.WriteLine("8. Exit");
        Console.Write("Choose an option: ");
        Console.ResetColor();
    }

    // ===== STUDENT MANAGEMENT MENU DISPLAY =====
    // Displays all student grade management options
    public static void ShowStudentMenu()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\n*****************************");
        Console.WriteLine("  STUDENT GRADE MANAGEMENT  ");
        Console.WriteLine("*****************************");
        Console.WriteLine("1. Add Student");
        Console.WriteLine("2. View All Students");
        Console.WriteLine("3. Add Grade to Student");
        Console.WriteLine("4. Calculate Average Grade for Student");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option: ");
        Console.ResetColor();
    }

    // ===== SYSTEM EXIT =====
    // Displays exit message when user closes the application
    public static void ExitSystem()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nExiting the entire system. Thank you for using our application!");
        Console.ResetColor();
    }
}