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

    public List<BorrowingTransaction> viewAllBorrowedBooks() {
        throw new NotImplementedException();
    }

    public List<BorrowingTransaction> viewAllBorrowedBooksForMember(Member member) {
        throw new NotImplementedException();
    }
}
