using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionRepositoryTests
{
    [Fact]
    public void DeleteBorrowingTransaction_ShouldDeleteBorrowingTransactionSuccessfully()
    {
        BorrowingTransaction borrowingTransaction = Helpers.CreateBorrowingTransaction();
        _borrowingTransactionRepository.CreateBorrowingTransaction(borrowingTransaction);

        _borrowingTransactionRepository.DeleteBorrowingTransaction(borrowingTransaction.Id);
        BorrowingTransaction? deletedBorrowingTransaction = _borrowingTransactionRepository.GetBorrowingTransaction(borrowingTransaction.Id);

        Assert.Null(deletedBorrowingTransaction);
    }

    [Fact]
    public void Delete_NonExistingBorrowingTransaction_ShouldThrowArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _borrowingTransactionRepository.DeleteBorrowingTransaction(Guid.NewGuid()));
    }
}
