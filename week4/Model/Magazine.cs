namespace LibrarySystemWeek4;


/// Represents a magazine in the library with issue number tracking.

public class Magazine : LibraryItemBase
{
    private int _issueNumber;

    public int IssueNumber
    {
        get { return _issueNumber; }
        set
        {
            // Issue numbers must be positive to be meaningful
            if (value <= 0)
                throw new InvalidItemDataException("Issue number must be positive");
            _issueNumber = value;
        }
    }

    public Magazine(string title, int issueNum, string publisher, int year) 
        : base(title, publisher, year)
    {
        IssueNumber = issueNum;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"  \u001b[35m[MAGAZINE]\u001b[0m \u001b[37m'{Title}' - Issue #{IssueNumber} ({PublicationYear})\u001b[0m");
        Console.WriteLine($"             \u001b[90mPublished by: {Publisher}\u001b[0m");
    }

    public override string GetItemType() => "Magazine";
}
