using System.Text.Json.Serialization;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem;

public class BorrowingTransaction : Transaction
{
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; set; }
    public double Fine { get; set; }

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
