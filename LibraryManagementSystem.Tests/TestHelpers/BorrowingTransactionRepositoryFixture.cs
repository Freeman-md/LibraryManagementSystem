using LibraryManagementSystem.FileContexts;

namespace LibraryManagementSystem.Tests;

public class BorrowingTransactionRepositoryFixture : IDisposable
{
    public BorrowingTransactionRepository BorrowingTransactionRepository { get; set; }
    private readonly string _testFilePath;

    public BorrowingTransactionRepositoryFixture() {
        _testFilePath = Path.Combine(Path.GetTempPath(), $"borrowing_transaction_repository_tests_{Guid.NewGuid()}.json");

        JsonFileContext<BorrowingTransaction> fileContext = new JsonFileContext<BorrowingTransaction>();
        BorrowingTransactionRepository = new BorrowingTransactionRepository(fileContext, _testFilePath);
    }

    public void Dispose() {
        if (System.IO.File.Exists(_testFilePath)) 
        {
            System.IO.File.Delete(_testFilePath);
        } 
    }
}
