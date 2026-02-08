namespace LibrarySystemWeek4;

/// <summary>
/// Defines the contract that all library items (books, magazines, newspapers) must follow.
/// Enables polymorphic behavior and consistent item handling across the system.
/// </summary>
public interface ILibraryItem
{
    string Title { get; set; }
    string Publisher { get; set; }
    int PublicationYear { get; set; }

    // Displays item information in a formatted way
    void DisplayInfo();

    // Returns the type of item (Book, Magazine, Newspaper) for filtering and classification
    string GetItemType();
}
