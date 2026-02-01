namespace LibrarySystemWeek4;


/// Abstract base class providing common properties and validation for all library items.
/// Centralizes validation logic to ensure consistency across Book, Magazine, and Newspaper types.

public abstract class LibraryItemBase : ILibraryItem
{
    private string _title = "";
    private string _publisher = "";
    private int _publicationYear;

    public string Title
    {
        get { return _title; }
        set { _title = value ?? throw new InvalidItemDataException("Title cannot be empty"); }
    }

    public string Publisher
    {
        get { return _publisher; }
        set { _publisher = value ?? throw new InvalidItemDataException("Publisher cannot be null"); }
    }

    public int PublicationYear
    {
        get { return _publicationYear; }
        set
        {
            // Ensures publication year is realistic (not before 1000 or in the future)
            if (value < 1000 || value > DateTime.Now.Year)
                throw new InvalidItemDataException($"Publication year must be between 1000 and {DateTime.Now.Year}");
            _publicationYear = value;
        }
    }

    protected LibraryItemBase(string title, string publisher, int year)
    {
        Title = title;
        Publisher = publisher;
        PublicationYear = year;
    }

    // Each item type has its own way to display information in the UI
    public abstract void DisplayInfo();

    // Enables filtering and type-specific comparison in duplicate checking
    public abstract string GetItemType();
}
