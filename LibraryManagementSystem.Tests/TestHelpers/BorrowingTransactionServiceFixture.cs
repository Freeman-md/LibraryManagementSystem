using LibraryManagementSystem.FileContexts;

namespace LibraryManagementSystem.Tests;

public class BorrowingTransactionServiceFixture : IDisposable
{
    public BorrowingTransactionService BorrowingTransactionService;
    private readonly string _testFilePath;

    public BorrowingTransactionServiceFixture() {
        _testFilePath = Path.Combine(Path.GetTempPath(), $"borrowing_transaction_service_tests_{Guid.NewGuid()}.json");

        JsonFileContext<BorrowingTransaction> fileContext = new JsonFileContext<BorrowingTransaction>();
        BorrowingTransactionRepository borrowingTransactionRepository = new BorrowingTransactionRepository(fileContext);

        BorrowingTransactionService = new BorrowingTransactionService(borrowingTransactionRepository);
    }

    public void Dispose() {
        if (System.IO.File.Exists(_testFilePath)) {
            System.IO.File.Delete(_testFilePath);
        }
    }
}
