using System;

namespace LibrarySystemWeek4;

/// <summary>
/// Centralizes user input validation across the application.
/// Provides reusable methods with built-in error handling and retry logic.
/// </summary>
public class InputValidator
{
    /// <summary>
    /// Validates and returns a publication year within acceptable range.
    /// Rejects values before 1000 or beyond current year.
    /// </summary>
    public static int GetValidYear(string prompt)
    {
        while (true)
        {
            try
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";
                
                if (!int.TryParse(input, out int year))
                    throw new InvalidItemDataException("Year must be a valid number");
                
                if (year < 1000 || year > DateTime.Now.Year)
                    throw new InvalidItemDataException($"Year must be between 1000 and {DateTime.Now.Year}");
                
                return year;
            }
            catch (InvalidItemDataException ex)
            {
                Console.WriteLine($"  \u001b[31m✗ {ex.Message}. Try again\u001b[0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  \u001b[31m✗ Something went wrong: {ex.Message}\u001b[0m");
            }
        }
    }

    /// <summary>
    /// Validates and returns a positive integer above minimum threshold.
    /// Used for fields like issue numbers and quantities.
    /// </summary>
    public static int GetValidNumber(string prompt, int minValue = 1)
    {
        while (true)
        {
            try
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";
                
                if (!int.TryParse(input, out int num))
                    throw new InvalidItemDataException("Must be a valid number");
                
                if (num < minValue)
                    throw new InvalidItemDataException($"Must be at least {minValue}");
                
                return num;
            }
            catch (InvalidItemDataException ex)
            {
                Console.WriteLine($"  \u001b[31m✗ {ex.Message}. Try again\u001b[0m");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  \u001b[31m✗ Error: {ex.Message}\u001b[0m");
            }
        }
    }

    /// <summary>
    /// Validates and returns a non-empty string with whitespace trimmed.
    /// Ensures required text fields always contain meaningful data.
    /// </summary>
    public static string GetNonEmptyString(string prompt)
    {
        while (true)
        {
            try
            {
                Console.Write(prompt);
                string input = Console.ReadLine() ?? "";
                
                if (string.IsNullOrWhiteSpace(input))
                    throw new InvalidItemDataException("Cannot be empty");
                
                return input.Trim();
            }
            catch (InvalidItemDataException ex)
            {
                Console.WriteLine($"  \u001b[31m✗ {ex.Message}. Try again\u001b[0m");
            }
        }
    }
}
