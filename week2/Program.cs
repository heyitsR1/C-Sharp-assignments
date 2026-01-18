using System;

// ===== MAIN ENTRY POINT =====
// This application serves as a launcher for multiple systems:
// 1. Simple Banking System - PIN-secured banking with deposit, withdrawal, balance inquiry
// 2. Task Management System - Create, manage, and track tasks by priority and due date
// 3. Student Grade Management - Track student grades and calculate averages
// The user is presented with a main menu to choose which system to run

class Program
{
    static void Main(string[] args)
    {
        // Flag to control main menu loop - continues until user selects exit
        bool exitMainWindow = false;
        
        // Main loop - continuously displays menu until user exits
        while (!exitMainWindow)
        {
            MenuManager.ShowMainMenu();

            string mainChoice = Console.ReadLine();

            // Route to appropriate system based on user selection
            switch (mainChoice)
            {
                case "1":
                    // Launch the Simple Banking System
                    BankingAccount.RunBankingSystem();
                    break;
                case "2":
                    // Launch the Task Management System
                    TaskSystemManager.RunTaskManagementSystem();
                    break;
                case "3":
                    // Launch the Student Grade Management System
                    StudentSystemManager.RunStudentGradeManagementSystem();
                    break;
                case "4":
                    // Exit the application
                    exitMainWindow = true;
                    MenuManager.ExitSystem();
                    break;
                default:
                    // Handle invalid menu choices
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please select 1-4.");
                    Console.ResetColor();
                    break;
            }
        }
    }
}