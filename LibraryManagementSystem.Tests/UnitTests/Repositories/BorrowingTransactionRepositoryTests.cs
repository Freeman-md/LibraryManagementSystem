using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionRepositoryTests : IClassFixture<BorrowingTransactionRepositoryFixture>
{
    private readonly BorrowingTransactionRepository _borrowingTransactionRepository;
    public BorrowingTransactionRepositoryTests(BorrowingTransactionRepositoryFixture fixture) {
        _borrowingTransactionRepository = fixture.BorrowingTransactionRepository;
    }   
}
