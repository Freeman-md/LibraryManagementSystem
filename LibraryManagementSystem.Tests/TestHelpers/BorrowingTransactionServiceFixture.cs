using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests;

public class BorrowingTransactionServiceFixture : IDisposable
{
    public BorrowingTransactionService BorrowingTransactionService { get; private set; }
    public BookService BookService { get; private set; }
    public MemberService MemberService { get; private set; }

    private readonly string _testFilePathForBorrowingTransactions;
    private readonly string _testFilePathForBooks;
    private readonly string _testFilePathForMembers;

    public BorrowingTransactionServiceFixture()
    {
        // Setup temporary file paths for test data
        _testFilePathForBorrowingTransactions = CreateTempFilePath("borrowing_transaction_service_tests");
        _testFilePathForBooks = CreateTempFilePath("borrowing_transaction_service_tests_books");
        _testFilePathForMembers = CreateTempFilePath("borrowing_transaction_service_tests_members");

        // Initialize repositories and services
        MemberService = InitializeMemberService(_testFilePathForMembers);
        BookService = InitializeBookService(_testFilePathForBooks);
        BorrowingTransactionService = InitializeBorrowingTransactionService(_testFilePathForBorrowingTransactions);
    }

    private string CreateTempFilePath(string prefix)
    {
        return Path.Combine(Path.GetTempPath(), $"{prefix}_{Guid.NewGuid()}.json");
    }

    private BorrowingTransactionService InitializeBorrowingTransactionService(string filePath)
    {
        var fileContext = new JsonFileContext<BorrowingTransaction>();
        var repository = new BorrowingTransactionRepository(fileContext, filePath);
        return new BorrowingTransactionService(repository, BookService, MemberService);
    }

    private BookService InitializeBookService(string filePath)
    {
        var fileContext = new JsonFileContext<Book>();
        var repository = new BookRepository(fileContext, filePath);
        return new BookService(repository);
    }

    private MemberService InitializeMemberService(string filePath)
    {
        var fileContext = new JsonFileContext<Member>();
        var repository = new MemberRepository(fileContext, filePath);
        return new MemberService(repository);
    }

    public void Dispose()
    {
        DeleteTempFile(_testFilePathForBorrowingTransactions);
        DeleteTempFile(_testFilePathForBooks);
        DeleteTempFile(_testFilePathForMembers);
    }

    private void DeleteTempFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}

