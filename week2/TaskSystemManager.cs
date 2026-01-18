using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ===== ENUM FOR TASK PRIORITY =====
// Defines three priority levels for tasks in the task management system
// Enum restricts values to predefined options for type safety
enum Priority
{
    Low,
    Medium,
    High
}

// ===== TASK SYSTEM MANAGER =====
// Manages all operations related to the Task Management System
// Handles the menu loop and delegates task operations to the TaskItem class

class TaskSystemManager
{
    // ===== TASK MANAGEMENT SYSTEM =====
    // Entry point for task management
    // Manages the menu loop and routes to appropriate task operations
    public static void RunTaskManagementSystem()
    {
        // Load existing tasks from file at startup for data persistence
        TaskItem.LoadTasksFromFile();
        bool exitTMS = false;

        // Task Management loop - continues until user exits this system
        while (!exitTMS)
        {
            MenuManager.ShowTaskMenu();

            string choiceTMS = Console.ReadLine();

            // Switch statement handles task operations based on user choice
            switch (choiceTMS)
            {
                case "1":
                    // Add a new task to the system
                    TaskItem.AddTask();
                    break;
                case "2":
                    // Display all tasks in the system
                    TaskItem.ViewAllTasks();
                    break;
                case "3":
                    // Mark a task as completed
                    TaskItem.MarkTaskCompleted();
                    break;
                case "4":
                    // Delete a task from the system
                    TaskItem.DeleteTask();
                    break;
                case "5":
                    // Filter and display tasks by priority level
                    TaskItem.FilterTasksByPriority();
                    break;
                case "6":
                    // Sort tasks by due date and display them
                    TaskItem.SortTasksByDueDate();
                    break;
                case "7":
                    // Save all tasks to a file for persistence across sessions
                    TaskItem.SaveTasksToFile();
                    break;
                case "8":
                    // Exit task management system and return to main menu
                    exitTMS = true;
                    Console.WriteLine("Exiting Task Management System. Goodbye!");
                    break;
                default:
                    // Handle invalid menu choices
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please select 1-8.");
                    Console.ResetColor();
                    break;
            }
        }
    }
}

// ===== TASK ITEM CLASS =====
// Represents a task with title, description, priority, and due date
// Provides static methods for task management operations
// Uses static list to maintain all tasks throughout program execution

class TaskItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime DueDate { get; set; } 
    public bool IsCompleted { get; set; }
    
    // Static list maintains all tasks throughout the program
    // Persists data as long as application is running
    static List<TaskItem> tasks = new List<TaskItem>();
    static string filePath = "tasks.txt";

    public TaskItem()
    {
        
    }

    public TaskItem(string title, string description, Priority priority, DateTime dueDate)
    {
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        IsCompleted = false;
    }

    // ===== TOSTRING OVERRIDE =====
    // Formats task information with colors for better UI presentation
    // Demonstrates custom ToString implementation with formatted output
    public override string ToString()
    {
        // Save color constants for consistent formatting
        var headerColor = ConsoleColor.Yellow;
        var defaultColor = ConsoleColor.White;

        // Title - use color for headers, white for values
        Console.ForegroundColor = headerColor;
        Console.Write("Title: ");
        Console.ForegroundColor = defaultColor;
        Console.Write($"{Title} | ");

        // Description
        Console.ForegroundColor = headerColor;
        Console.Write("Description: ");
        Console.ForegroundColor = defaultColor;
        Console.Write($"{Description} | ");

        // Priority
        Console.ForegroundColor = headerColor;
        Console.Write("Priority: ");
        Console.ForegroundColor = defaultColor;
        Console.Write($"{Priority} | ");

        // Due Date - formatted as yyyy-MM-dd
        Console.ForegroundColor = headerColor;
        Console.Write("Due Date: ");
        Console.ForegroundColor = defaultColor;
        Console.Write($"{DueDate:yyyy-MM-dd} | ");

        // Status - shows "Completed" or "Pending"
        Console.ForegroundColor = headerColor;
        Console.Write("Status: ");
        Console.ForegroundColor = defaultColor;
        Console.WriteLine(IsCompleted ? "Completed" : "Pending");

        // Reset colors to default after output
        Console.ResetColor();

        return string.Empty; // Since we write directly to console
    }

    // ===== ADD TASK =====
    // Allows user to create and add a new task to the system
    // Validates title and collects all task details from user
    public static void AddTask()
    {
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();

        // Validate that title is not empty or just whitespace
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Title cannot be empty.");
            Console.ResetColor();
            return;
        }

        Console.Write("Enter task description: ");
        string description = Console.ReadLine();

        // Get priority level from user with validation
        Priority priority = GetValidPriority();

        // Get due date from user with validation
        DateTime dueDate = GetValidDate("Enter due date (yyyy-MM-dd): ");

        // Add new task to the list with IsCompleted set to false by default
        tasks.Add(new TaskItem(title, description, priority, dueDate));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Task added successfully.");
        Console.ResetColor();
    }

    // ===== VIEW ALL TASKS =====
    // Displays all tasks in the system using multiple iteration methods
    public static void ViewAllTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\nAll Tasks:");
        Console.ResetColor();
        
        // First method: traditional for loop with index
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            tasks[i].ToString();
        }

        Console.WriteLine("\n--- Alternative Display with Index ---");
        // Second method: foreach with LINQ Select for index
        // Demonstrates advanced LINQ usage for more readable code
        foreach (var task in tasks.Select((task, index) => new { task, index }))
        {
            Console.Write($"{task.index + 1}. ");
            task.task.ToString();
        }
    }

    // ===== MARK TASK AS COMPLETED =====
    // Allows user to mark a task as completed
    public static void MarkTaskCompleted()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        ViewAllTasks();
        Console.Write("Enter the task number to mark as completed: ");
        string input = Console.ReadLine();

        // Validate task number and mark as completed if valid
        // TryParse safely converts string to integer without crashing
        if (int.TryParse(input, out int index) && index >= 1 && index <= tasks.Count)
        {
            tasks[index - 1].IsCompleted = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task marked as completed.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid task number.");
            Console.ResetColor();
        }
    }

    // ===== DELETE TASK =====
    // Allows user to remove a task from the system
    public static void DeleteTask()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        ViewAllTasks();
        Console.Write("Enter the task number to delete: ");
        string input = Console.ReadLine();

        // Validate task number and delete if valid
        // RemoveAt removes element at specified index
        if (int.TryParse(input, out int index) && index >= 1 && index <= tasks.Count)
        {
            tasks.RemoveAt(index - 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Task deleted.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid task number.");
            Console.ResetColor();
        }
    }

    // ===== FILTER TASKS BY PRIORITY =====
    // Displays only tasks with a specific priority level
    // Demonstrates LINQ Where clause for filtering data
    public static void FilterTasksByPriority()
    {
        Priority priority = GetValidPriority();

        // Use LINQ to filter tasks by selected priority
        // Where clause returns only tasks matching the condition
        var filteredTasks = tasks.Where(t => t.Priority == priority).ToList();

        if (filteredTasks.Count == 0)
        {
            Console.WriteLine($"No tasks with {priority} priority.");
            return;
        }

        Console.WriteLine($"\nTasks with {priority} priority:");
        for (int i = 0; i < filteredTasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {filteredTasks[i]}");
        }
    }

    // ===== SORT TASKS BY DUE DATE =====
    // Sorts all tasks by due date in ascending order
    // Demonstrates LINQ OrderBy for sorting collections
    public static void SortTasksByDueDate()
    {
        // Use LINQ OrderBy to sort tasks by due date
        // Reassign to tasks list to maintain sorted order
        tasks = tasks.OrderBy(t => t.DueDate).ToList();
        Console.WriteLine("Tasks sorted by due date.");
        ViewAllTasks();
    }

    // ===== HELPER: GET VALID PRIORITY =====
    // Validates user input to ensure priority is Low, Medium, or High
    // Keeps prompting until valid input is received
    // Demonstrates input validation pattern with Enum.TryParse
    public static Priority GetValidPriority()
    {
        while (true)
        {
            Console.Write("Enter priority (Low, Medium, High): ");
            string input = Console.ReadLine();

            // Use Enum.TryParse to validate priority value
            // Case-insensitive parsing allows user flexibility
            if (Enum.TryParse(input, true, out Priority priority))
            {
                return priority;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid priority. Please enter Low, Medium, or High.");
                Console.ResetColor();
            }
        }
    }

    // ===== HELPER: GET VALID DATE =====
    // Validates user input to ensure date is in correct format (yyyy-MM-dd)
    // Keeps prompting until valid input is received
    // Demonstrates input validation pattern with DateTime.TryParse
    public static DateTime GetValidDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            // Use DateTime.TryParse to validate date format
            // Allows flexible date parsing while maintaining type safety
            if (DateTime.TryParse(input, out DateTime date))
            {
                return date;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                Console.ResetColor();
            }
        }
    }

    // ===== LOAD TASKS FROM FILE =====
    // Reads tasks from a text file and loads them into memory at startup
    // File format: Title|Description|Priority|DueDate|Status
    // Enables data persistence across application sessions
    public static void LoadTasksFromFile()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                // Split each line by pipe delimiter to get task components
                string[] parts = line.Split('|');
                if (parts.Length == 5)
                {
                    string title = parts[0].Trim();
                    string description = parts[1].Trim();
                    // Parse string priority back to enum
                    Priority priority = (Priority)Enum.Parse(typeof(Priority), parts[2].Trim());
                    DateTime dueDate = DateTime.Parse(parts[3].Trim());
                    bool isCompleted = parts[4].Trim() == "Completed";

                    // Create task object and add to list
                    TaskItem task = new TaskItem(title, description, priority, dueDate);
                    task.IsCompleted = isCompleted;
                    tasks.Add(task);
                }
            }
        }
    }

    // ===== SAVE TASKS TO FILE =====
    // Writes all tasks to a text file for persistent storage
    // File format: Title|Description|Priority|DueDate|Status
    // Uses StreamWriter for proper file handling and resource cleanup
    public static void SaveTasksToFile()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (TaskItem task in tasks)
            {
                // Format: Title|Description|Priority|DueDate|Status
                // Pipe character used as delimiter for easy parsing
                string status = task.IsCompleted ? "Completed" : "Pending";
                writer.WriteLine($"{task.Title}|{task.Description}|{task.Priority}|{task.DueDate:yyyy-MM-dd}|{status}");
            }
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Tasks saved to file successfully.");
        Console.ResetColor();
    }
}