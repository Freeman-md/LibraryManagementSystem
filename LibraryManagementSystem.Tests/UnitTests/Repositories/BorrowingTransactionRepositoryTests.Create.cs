using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionRepositoryTests
{
    [Fact]
    public void CreateBorrowingTransaction_ShouldCreateBorrowingTransactionSuccesfully() {
        BorrowingTransaction borrowingTransaction = Helpers.CreateBorrowingTransaction();

        _borrowingTransactionRepository.CreateBorrowingTransaction(borrowingTransaction);
        BorrowingTransaction createdBorrowingTransaction = _borrowingTransactionRepository.GetBorrowingTransaction(borrowingTransaction.Id);

        Assert.NotNull(createdBorrowingTransaction);
        Assert.Equal(borrowingTransaction.Id, createdBorrowingTransaction.Id);
        Assert.Equal(borrowingTransaction.Book, createdBorrowingTransaction.Book);
        Assert.Equal(borrowingTransaction.Member, createdBorrowingTransaction.Member);
    }

    [Fact]
    public void SaveBorrowingTransactions_ShouldSaveBorrowingTransactionsToFile() {
        const int TOTAL_NUMBER_OF_BORROWING_TRANSACTIONS_TO_CREATE = 5;
        List<BorrowingTransaction> borrowingTransactions = new List<BorrowingTransaction>();

        for (int i = 0; i < TOTAL_NUMBER_OF_BORROWING_TRANSACTIONS_TO_CREATE; i++)
        {
            borrowingTransactions.Add(Helpers.CreateBorrowingTransaction());
        }

        _borrowingTransactionRepository.SaveBorrowingTransactions(borrowingTransactions);


        List<BorrowingTransaction> createdBorrowingTransactions = _borrowingTransactionRepository.GetAllBorrowingTransactions();


        Assert.NotNull(createdBorrowingTransactions);
        Assert.Equal(TOTAL_NUMBER_OF_BORROWING_TRANSACTIONS_TO_CREATE, createdBorrowingTransactions.Count);

        foreach (BorrowingTransaction borrowingTransaction in borrowingTransactions)
        {
            Assert.Contains(createdBorrowingTransactions, b => b.Id == borrowingTransaction.Id);
        }
    }
}
