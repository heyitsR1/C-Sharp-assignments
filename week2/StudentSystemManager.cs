using System;
using System.Collections.Generic;

// ===== STUDENT SYSTEM MANAGER =====
// Manages all operations related to the Student Grade Management System
// Handles the menu loop and delegates student operations to the Student class

class StudentSystemManager
{
    // ===== STUDENT GRADE MANAGEMENT SYSTEM =====
    // Entry point for student grade management
    // Manages the menu loop and routes to appropriate student operations
    public static void RunStudentGradeManagementSystem()
    {
        bool exitGMS = false;

        // Grade Management loop - continues until user exits this system
        while (!exitGMS)
        {
            MenuManager.ShowStudentMenu();

            string choiceGMS = Console.ReadLine();

            // Switch statement handles student operations based on user choice
            switch (choiceGMS)
            {
                case "1":
                    // Add a new student to the system
                    Student.AddStudent();
                    break;
                case "2":
                    // Display all students and their grades
                    Student.ViewAllStudents();
                    break;
                case "3":
                    // Add a grade to an existing student
                    Student.AddGradeToStudent();
                    break;
                case "4":
                    // Calculate and display average grade for a student
                    Student.CalculateAverageForStudent();
                    break;
                case "5":
                    // Exit student management system and return to main menu
                    exitGMS = true;
                    Console.WriteLine("Exiting Student Grade Management System. Goodbye!");
                    break;
                default:
                    // Handle invalid menu choices
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Please select 1-5.");
                    Console.ResetColor();
                    break;
            }
        }
    }
}

// ===== STUDENT CLASS =====
// Represents a student with name and grades
// Provides methods for adding students, adding grades, and calculating averages
// Uses static list to maintain all student data throughout program execution

public class Student
{
    public string Name { get; set; }
    public List<double> Grades { get; set; } = new List<double>();
    
    // Static list maintains all students throughout the program
    // Persists data as long as application is running
    static List<Student> students = new List<Student>();

    public Student()
    {
        
    }

    public Student(string name)
    {
        Name = name;
    }

    // ===== CALCULATE AVERAGE =====
    // Computes the average of all grades for a student
    // Returns 0.0 if no grades exist to avoid division by zero error
    public double CalculateAverage()
    {
        if (Grades.Count == 0) return 0.0;
        
        double sum = 0;
        // Sum all grades using foreach loop
        foreach (double grade in Grades)
        {
            sum += grade;
        }
        
        // Calculate average by dividing sum by number of grades
        double avg = sum / Grades.Count;
        return avg;
    }

    // ===== ADD STUDENT FUNCTION =====
    // Allows user to add a new student to the system
    // Validates that student name is not empty or whitespace
    public static void AddStudent()
    {
        Console.Write("Enter student name: ");
        string name = Console.ReadLine();

        // Validate that name is not empty or just whitespace
        if (!string.IsNullOrWhiteSpace(name))
        {
            students.Add(new Student(name));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Student '{name}' added successfully.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid name. Please enter a valid student name.");
            Console.ResetColor();
        }
    }

    // ===== VIEW ALL STUDENTS =====
    // Displays all students and their grades in a formatted list
    // Shows index, name, and all grades for each student
    public static void ViewAllStudents()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students in the system.");
            return;
        }

        Console.WriteLine("\nList of Students:");
        // Display each student with numbered index
        for (int i = 0; i < students.Count; i++)
        {
            // string.Join combines all grades into comma-separated list
            Console.WriteLine($"{i + 1}. {students[i].Name} (Grades: {string.Join(", ", students[i].Grades)})");
        }
    }

    // ===== ADD GRADE TO STUDENT =====
    // Allows user to add a grade to an existing student
    // Validates student selection and grade value
    public static void AddGradeToStudent()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students available. Add a student first.");
            return;
        }

        // Display student list for selection
        ViewAllStudents();
        Console.Write("Enter the student number to add grade: ");
        string input = Console.ReadLine();

        // Validate student number and add grade if valid
        // TryParse safely converts string to integer
        if (int.TryParse(input, out int index) && index >= 1 && index <= students.Count)
        {
            double grade = GetValidGrade("Enter grade (0-100): ");
            // Add grade to the selected student (index-1 because lists are 0-indexed)
            students[index - 1].Grades.Add(grade);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Grade {grade} added to {students[index - 1].Name}.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid student number.");
            Console.ResetColor();
        }
    }

    // ===== CALCULATE AVERAGE FOR STUDENT =====
    // Allows user to select a student and view their average grade
    public static void CalculateAverageForStudent()
    {
        if (students.Count == 0)
        {
            Console.WriteLine("No students available. Add a student first.");
            return;
        }

        // Display student list for selection
        ViewAllStudents();
        Console.Write("Enter the student number to calculate average: ");
        string input = Console.ReadLine();

        // Validate student number and calculate average if valid
        if (int.TryParse(input, out int index) && index >= 1 && index <= students.Count)
        {
            double average = students[index - 1].CalculateAverage();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Average grade for {students[index - 1].Name}: {average:F2}");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid student number.");
            Console.ResetColor();
        }
    }

    // ===== HELPER: GET VALID GRADE =====
    // Validates user input to ensure grade is between 0-100
    // Keeps prompting until valid input is received
    // Demonstrates input validation pattern that prevents invalid data
    public static double GetValidGrade(string prompt)
    {
        double grade;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            // Validate that input is numeric and within valid range (0-100)
            // TryParse prevents crashes from non-numeric input
            if (double.TryParse(input, out grade) && grade >= 0 && grade <= 100)
            {
                return grade;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid grade. Please enter a number between 0 and 100.");
                Console.ResetColor();
            }
        }
    }
}