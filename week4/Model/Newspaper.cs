namespace LibrarySystemWeek4;


/// Represents a newspaper in the library with editor information.
/// New item type extending the library management system's capabilities.

public class Newspaper : LibraryItemBase
{
    private string _editor = "";

    public string Editor
    {
        get { return _editor; }
        set { _editor = value ?? throw new InvalidItemDataException("Editor cannot be empty"); }
    }

    public Newspaper(string title, string editor, string publisher, int year) 
        : base(title, publisher, year)
    {
        Editor = editor;
    }

    // Parameterless constructor for JSON deserialization
    public Newspaper() : this("", "", "", 1000) { }

    public override void DisplayInfo()
    {
        Console.WriteLine($"  \u001b[33m[NEWSPAPER]\u001b[0m \u001b[37m'{Title}' - Edited by {Editor} ({PublicationYear})\u001b[0m");
        Console.WriteLine($"              \u001b[90mPublished by: {Publisher}\u001b[0m");
    }

    public override string GetItemType() => "Newspaper";
}
