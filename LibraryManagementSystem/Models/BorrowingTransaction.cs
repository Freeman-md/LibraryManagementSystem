using LibraryManagementSystem.Models;

namespace LibraryManagementSystem;

public class BorrowingTransaction : Transaction
{
    public DateTime DueDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public double Fine { get; set; }

    public BorrowingTransaction(int id, Book book, Member member, DateTime borrowDate, DateTime dueDate, DateTime returnDate, double fine)
        : base(id, book, member, borrowDate)
    {
        DueDate = dueDate;
        ReturnDate = returnDate;
        Fine = fine;
    }
}
