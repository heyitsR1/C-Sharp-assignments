namespace LibrarySystemWeek4;


/// Represents a book in the library with author-specific information.

public class Book : LibraryItemBase
{
    private string _author = "";

    public string Author
    {
        get { return _author; }
        set { _author = value ?? throw new InvalidItemDataException("Author cannot be empty"); }
    }

    public Book(string title, string author, string publisher, int year) 
        : base(title, publisher, year)
    {
        Author = author;
    }

    // Parameterless constructor for JSON deserialization
    public Book() : this("", "", "", 1000) { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"  \u001b[34m[BOOK]\u001b[0m \u001b[37m'{Title}' by {Author} ({PublicationYear})\u001b[0m");
        Console.WriteLine($"         \u001b[90mPublished by: {Publisher}\u001b[0m");
    }

    public override string GetItemType() => "Book";
}
