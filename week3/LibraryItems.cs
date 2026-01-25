using System;

namespace LibrarySystem;

// custom exception for duplicate items
public class DuplicateItemException : Exception
{
    public DuplicateItemException(string message) : base(message) { }
}

// exception for bad data input
public class InvalidItemDataException : Exception
{
    public InvalidItemDataException(string message) : base(message) { }
}

// base class for all library items
public abstract class MediaItem
{
    private string _title;
    private string _publisher;
    private int _publicationYear;

    public string Title
    {
        get { return _title; }
        set { _title = value ?? throw new InvalidItemDataException("title cant be empty"); }
    }

    public string Publisher
    {
        get { return _publisher; }
        set { _publisher = value ?? throw new InvalidItemDataException("publisher cant be null"); }
    }

    public int PublicationYear
    {
        get { return _publicationYear; }
        set
        {
            if (value < 1000 || value > DateTime.Now.Year)
                throw new InvalidItemDataException($"year must be between 1000 and {DateTime.Now.Year}");
            _publicationYear = value;
        }
    }

    public MediaItem(string title, string publisher, int year)
    {
        Title = title;
        Publisher = publisher;
        PublicationYear = year;
    }

    // each item type has its own way to display info
    public abstract void DisplayInfo();

    public abstract string GetItemType();
}

// represents a book in library
public class Book : MediaItem
{
    private string _author;

    public string Author
    {
        get { return _author; }
        set { _author = value ?? throw new InvalidItemDataException("author cannot be empty"); }
    }

    public Book(string title, string author, string publisher, int year) : base(title, publisher, year)
    {
        Author = author;
    }

    // override to show book specific info
    public override void DisplayInfo()
    {
        Console.WriteLine($"  \u001b[34m[BOOK]\u001b[0m \u001b[37m'{Title}' by {Author} ({PublicationYear})\u001b[0m");
        Console.WriteLine($"         \u001b[90mPublished by: {Publisher}\u001b[0m");
    }

    public override string GetItemType() => "Book";
}

// represents a magazine in library
public class Magazine : MediaItem
{
    private int _issueNumber;

    public int IssueNumber
    {
        get { return _issueNumber; }
        set
        {
            if (value <= 0)
                throw new InvalidItemDataException("issue number must be positive");
            _issueNumber = value;
        }
    }

    public Magazine(string title, int issueNum, string publisher, int year) : base(title, publisher, year)
    {
        IssueNumber = issueNum;
    }

    // magazine info display
    public override void DisplayInfo()
    {
        Console.WriteLine($"  \u001b[35m[MAGAZINE]\u001b[0m \u001b[37m'{Title}' - Issue #{IssueNumber} ({PublicationYear})\u001b[0m");
        Console.WriteLine($"             \u001b[90mPublished by: {Publisher}\u001b[0m");
    }

    public override string GetItemType() => "Magazine";
}
