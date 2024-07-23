using System.Diagnostics.Contracts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem;

public class BorrowingTransactionService
{
    private readonly BorrowingTransactionRepository _borrowingTransactionRepository;
    public BorrowingTransactionService(BorrowingTransactionRepository borrowingTransactionRepository) {
        _borrowingTransactionRepository = borrowingTransactionRepository;
    }

    public void BorrowBook(Guid bookId, Guid memberId, int duration) {
        
    }

    public void ReturnBook() {

    }

    public List<BorrowingTransaction> ViewAllBorrowedBooks() {
        throw new NotImplementedException();
    }

    public List<BorrowingTransaction> ViewAllBorrowedBooksForMember(Guid id) {
        throw new NotImplementedException();
    }
}
