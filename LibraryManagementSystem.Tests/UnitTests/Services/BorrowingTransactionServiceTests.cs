namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionServiceTests : IClassFixture<BorrowingTransactionServiceFixture>
{
    private readonly BorrowingTransactionService _borrowingTransactionService;
    public BorrowingTransactionServiceTests(BorrowingTransactionServiceFixture fixture) {
        _borrowingTransactionService = fixture.BorrowingTransactionService;
    }

    
}
