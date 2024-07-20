using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests;

public class BorrowingTransactionRepositoryTests : IClassFixture<BorrowingTransactionRepositoryFixture>
{
    private readonly BorrowingTransactionRepository _borrowingTransactionRepository;
    public BorrowingTransactionRepositoryTests(BorrowingTransactionRepositoryFixture fixture) {
        _borrowingTransactionRepository = fixture.BorrowingTransactionRepository;
    }   

    private BorrowingTransaction CreateBorrowingTransaction(Book book, Member member, int borrowingDurationInDays = 7, double fine = 0.0, DateTime? returnDate = null) {
        DateTime dueDate = DateTime.Now.AddDays(borrowingDurationInDays);

        return new BorrowingTransaction(book, member, dueDate, fine, returnDate);
    }
}
