using System.Diagnostics.Contracts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem;

public class BorrowingTransactionService
{
    private readonly BorrowingTransactionRepository _borrowingTransactionRepository;
    public BorrowingTransactionService(BorrowingTransactionRepository borrowingTransactionRepository) {
        _borrowingTransactionRepository = borrowingTransactionRepository;
    }

    public void borrowBook(Book book, Member member, int duration) {
        
    }

    public void returnBook() {

    }

    public void viewBorrowedBooks() {
        
    }
}
