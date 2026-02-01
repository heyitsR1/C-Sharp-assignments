namespace LibrarySystemWeek4;


/// Thrown when attempting to add an item that already exists in the library.
/// Prevents accidental data duplication in the catalog.

public class DuplicateItemException : Exception
{
    public DuplicateItemException(string message) : base(message) { }
}
