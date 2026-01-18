using System;

// ===== BANKING ACCOUNT CLASS =====

// Includes PIN authentication, deposit, withdrawal, and balance inquiry
// Demonstrates input validation, error handling, and state management

class BankingAccount
{
    
    private static double balance = 0.0;           // tracks account balance
    private static string correctPin = "1234";     // hardcoded PIN for authentication
    private static bool loggedIn = false;          // store the user's logged in stage

    // ===== LOGIN SYSTEM WITH PIN VALIDATION =====
    // Implements secure login with 3 attempts maximum
    // Prevents brute force attacks by locking system after failed attempts
    // Returns true if authentication successful, false if all attempts exhausted

    public static bool AuthenticateUser()
    {
        int attempts = 0;                    // Counter to track failed login attempts
        const int maxAttempts = 3;           // Maximum allowed attempts before system locks
        bool loginSuccessful = false;

        // Loop continues until user succeeds or exhausts all attempts
        while (attempts < maxAttempts && !loginSuccessful)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter PIN: ");
            Console.ResetColor();
            string enteredPin = Console.ReadLine();

            // Check if entered PIN matches the correct PIN
            if (enteredPin == correctPin)
            {
                loggedIn = true;             // Set login to true
                loginSuccessful = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Login successful!");
                Console.ResetColor();
            }
            else
            {
                // Increment attempts counter for each failed login
                attempts++;

                // Calculate remaining attempts and display to user
                int remainingAttempts = maxAttempts - attempts;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Incorrect PIN. Attempts remaining: {remainingAttempts}");
                Console.ResetColor();
            }
        }

        // Check if user exceeded maximum attempts
        if (!loginSuccessful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Too many failed attempts. System locked.");
            Console.ResetColor();
            return false;
        }

        return true;
    }

    // ===== DEPOSIT OPERATION =====


    // Allows users to add money to their account

    // Validates that the amount is positive before processing deposit

    // Uses try-parse pattern to safely convert user input to numeric value
    public static void Deposit()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Enter amount to deposit: $");
        Console.ResetColor();
        string input = Console.ReadLine();

        // Try to convert input string to double for validation

        if (double.TryParse(input, out double amount))
        {
            // Validate that amount is positive,cannot deposit zero or negative amounts

            if (amount > 0)
            {
                // Add amount to account balance
                balance += amount;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Successfully deposited: ${amount:F2}");
                Console.WriteLine($"New balance: ${balance:F2}");
                Console.ResetColor();
            }
            else
            {
                // Handle invalid amounts (zero or negative)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Deposit amount must be greater than zero.");
                Console.ResetColor();
            }
        }
        else
        {
            // Handle non-numeric input
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: Please enter a valid number.");
            Console.ResetColor();
        }
    }

    // ===== WITHDRAWAL OPERATION =====

    // Allows users to withdraw money from their account

    // Validates amount is positive AND does not exceed current balance

    // Prevents overdraft by checking balance before allowing withdrawal


    public static void Withdraw()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("Enter amount to withdraw: $");
        Console.ResetColor();
        string input = Console.ReadLine();

        // Try to convert input string to double for validation
        if (double.TryParse(input, out double amount))
        {
            // Validate that amount is positive - cannot withdraw zero or negative amounts
            if (amount <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Withdrawal amount must be greater than zero.");
                Console.ResetColor();
            }
            // Check if user has sufficient balance for withdrawal
            else if (amount > balance)
            {
                // Prevent overdraft - user cannot withdraw more than available balance
                // This conditional logic prevents financial inconsistency
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: Insufficient funds. Current balance: ${balance:F2}");
                Console.ResetColor();
            }
            else
            {
                // Deduct amount from balance for valid withdrawal
                balance -= amount;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Successfully withdrawn: ${amount:F2}");
                Console.WriteLine($"New balance: ${balance:F2}");
                Console.ResetColor();
            }
        }
        else
        {
            // Handle non-numeric input
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: Please enter a valid number.");
            Console.ResetColor();
        }
    }

    // ===== BALANCE INQUIRY =====

    // Displays the current account balance to the user
    // Simple operation that doesn't modify account state
    public static void CheckBalance()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\nCurrent balance: ${balance:F2}");
        Console.ResetColor();
    }

    // ===== MAIN BANKING SYSTEM LOOP =====

    // Entry point for the banking system

    public static void RunBankingSystem()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n===== WELCOME TO SIMPLE BANKING SYSTEM =====");
        Console.ResetColor();

        // First, authenticate user with PIN before allowing any transactions
        if (!AuthenticateUser())
        {
            // Exit if authentication fails (3 failed attempts)
            return;
        }

        // Main banking loop - continues until user chooses to exit
        bool exitBanking = false;
        while (!exitBanking && loggedIn)
        {
            MenuManager.ShowBankingMenu();
            string choice = Console.ReadLine();

            // Switch statement handles user's choice of operation
            // Each case calls the corresponding banking function
            switch (choice)
            {
                case "1":
                    // User selected deposit operation
                    Deposit();
                    break;
                case "2":
                    // User selected withdrawal operation
                    Withdraw();
                    break;
                case "3":
                    // User selected balance inquiry operation
                    CheckBalance();
                    break;
                case "4":
                    // User selected exit - set flag to break loop and return to main menu
                    exitBanking = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Thank you for using Simple Banking System!");
                    Console.ResetColor();
                    break;
                default:
                    // Handle invalid menu choices
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please select 1-4.");
                    Console.ResetColor();
                    break;
            }
        }

        // Reset login flag when exiting banking system to allow re-login on next access
        loggedIn = false;
    }
}