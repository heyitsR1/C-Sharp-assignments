namespace LibrarySystemWeek4;


/// Thrown when item data fails validation (null, invalid format, or out-of-range values).
/// Ensures data integrity before objects are created or stored.
public class InvalidItemDataException : Exception
{
    public InvalidItemDataException(string message) : base(message) { }
}
