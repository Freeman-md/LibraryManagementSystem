using System.Text.Json.Serialization;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem;

public class BorrowingTransaction : Transaction
{
    
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; set; }
    public double Fine { get; set; }

    public const int DEFAULT_BORROWING_DURATION_IN_DAYS = 7;
    public const double FINE_RATE_PER_DAY = 10.0;
    public const int GRACE_PERIOD_IN_DAYS = 3;

    [JsonConstructor]
    public BorrowingTransaction(Guid id, Book book, Member member, DateTime dueDate, double fine = 0.0, DateTime? returnDate = null)
        : base(id, book, member)
    {
        DueDate = dueDate;
        ReturnDate = returnDate;
        Fine = fine;
    }

    public BorrowingTransaction(Book book, Member member, DateTime dueDate, double fine = 0.0, DateTime? returnDate = null)
        : base(book, member)
    {
        DueDate = dueDate;
        ReturnDate = returnDate;
        Fine = fine;
    }
}
