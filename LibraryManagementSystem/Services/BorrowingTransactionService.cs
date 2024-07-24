using System.Diagnostics.Contracts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem;

public class BorrowingTransactionService
{
    private readonly BorrowingTransactionRepository _borrowingTransactionRepository;
    public BorrowingTransactionService(BorrowingTransactionRepository borrowingTransactionRepository) {
        _borrowingTransactionRepository = borrowingTransactionRepository;
    }

    public BorrowingTransaction BorrowBook(Guid bookId, Guid memberId, int duration) {
        throw new NotImplementedException();
    }

    public BorrowingTransaction BorrowBook(Guid bookId, Guid memberId) {
        throw new NotImplementedException();
    }

    public BorrowingTransaction ReturnBook() {
        throw new NotImplementedException();
    }

    public List<BorrowingTransaction> GetAllBorrowedBooks() {
        throw new NotImplementedException();
    }

    public List<BorrowingTransaction> GetAllBorrowedBooksForMember(Guid memberId) {
        throw new NotImplementedException();
    }

    public BorrowingTransaction GetBorrowedBook(Guid borrowingTransactionId) {
        throw new NotImplementedException();
    }
}
