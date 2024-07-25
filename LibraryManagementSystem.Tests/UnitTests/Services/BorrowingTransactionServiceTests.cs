using System.Net;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionServiceTests : IClassFixture<BorrowingTransactionServiceFixture>
{
    private readonly BorrowingTransactionService _borrowingTransactionService;
    private readonly MemberService _memberService;
    private readonly BookService _bookService;
    public BorrowingTransactionServiceTests(BorrowingTransactionServiceFixture fixture)
    {
        _borrowingTransactionService = fixture.BorrowingTransactionService;
        _memberService = fixture.MemberService;
        _bookService = fixture.BookService;

        fixture.ClearData();
    }

    [Fact]
    public void GetAllBorrowedBooks_WhenThereAreBorrowedBooks_ShouldReturnAllBorrowedBooks()
    {
        // Arrange
        List<Book> books = new List<Book>() {
            Helpers.CreateBook(),
            Helpers.CreateBook(),
        };
        List<Member> members = new List<Member>() {
            Helpers.CreateMember(email: $"{Guid.NewGuid()}@example.com"),
            Helpers.CreateMember(email: $"{Guid.NewGuid()}@example.com"),
        };
        int duration_in_days = 7;

        foreach (Book book in books)
        {
            _bookService.AddBook(book);
        }

        foreach (Member member in members)
        {
            _memberService.RegisterMember(member);
        }

        foreach (int index in Enumerable.Range(0, 2))
        {
            _borrowingTransactionService.BorrowBook(books[index].Id, members[index].Id, duration_in_days);
        }

        // Act
        List<BorrowingTransaction> borrowingTransactions = _borrowingTransactionService.GetAllBorrowedBooks();

        // Assert
        Assert.NotNull(borrowingTransactions);
        Assert.Equal(books.Count, borrowingTransactions.Count);
    }

    [Fact]
    public void GetAllBorrowedBooks_WhenNoBorrowedBooks_ShouldReturnEmptyList()
    {
        // Act
        List<BorrowingTransaction> borrowingTransactions = _borrowingTransactionService.GetAllBorrowedBooks();

        // Assert
        Assert.NotNull(borrowingTransactions);
        Assert.Empty(borrowingTransactions);
    }


    [Fact]
    public void GetAllBorrowedBooks_ForAMember_WhenMemberHasBorrowedBooks_ShouldReturnAllBorrowedBooks()
    {
        // Arrange
        Member member = Helpers.CreateMember(email: $"{Guid.NewGuid()}@example.com");
        _memberService.RegisterMember(member);

        List<Book> books = new List<Book>() {
            Helpers.CreateBook(),
            Helpers.CreateBook(),
        };
        int duration_in_days = 7;

        foreach (Book book in books)
        {
            _bookService.AddBook(book);
        }

        foreach (int index in Enumerable.Range(0, 2))
        {
            _borrowingTransactionService.BorrowBook(books[index].Id, member.Id, duration_in_days);
        }

        // Act
        List<BorrowingTransaction> borrowingTransactions = _borrowingTransactionService.GetAllBorrowedBooksForMember(member.Id);

        // Assert
        Assert.NotNull(borrowingTransactions);
        Assert.Equal(books.Count, borrowingTransactions.Count);

        foreach (BorrowingTransaction borrowingTransaction in borrowingTransactions)
        {
            Assert.Contains(borrowingTransactions, borrowingTransaction => borrowingTransaction.Member.Id == member.Id);
        }
    }

    [Fact]
    public void GetAllBorrowedBooks_ForAMember_WhenMemberHasNoBorrowedBooks_ShouldReturnEmptyList()
    {
        // Arrange
        Member member = Helpers.CreateMember(email: $"{Guid.NewGuid()}@example.com");
        _memberService.RegisterMember(member);

        // Act
        List<BorrowingTransaction> borrowingTransactions = _borrowingTransactionService.GetAllBorrowedBooksForMember(member.Id);

        // Assert
        Assert.NotNull(borrowingTransactions);
        Assert.Empty(borrowingTransactions);
    }

    [Fact]
    public void GetAllBorrowedBooks_ForAMember_WhenMemberDoesNotExist_ShouldThrowArgumentException()
    {
        // Arrange
        Member member = Helpers.CreateMember(email: $"{Guid.NewGuid()}@example.com");

        // Assert
        Assert.Throws<ArgumentException>(() => _borrowingTransactionService.GetAllBorrowedBooksForMember(member.Id));
    }

    [Fact]
    public void GetBorrowedBook_ByTransactionId_ShouldReturnBorrowingTransaction()
    {
        // Arrange
        var randomEmail = $"user{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());
        int duration_in_days = 7;

        BorrowingTransaction borrowingTransaction = _borrowingTransactionService.BorrowBook(book.Id, member.Id, duration_in_days);

        // Act
        BorrowingTransaction? foundBorrowingTransaction = _borrowingTransactionService.GetBorrowedBook(borrowingTransaction.Id);

        // Assert
        Assert.NotNull(foundBorrowingTransaction);
        Assert.Equal(borrowingTransaction.Id, foundBorrowingTransaction.Id);
        Assert.Equal(book.Id, foundBorrowingTransaction.Book.Id);
        Assert.Equal(member.Id, foundBorrowingTransaction.Member.Id);
        Assert.Equal(borrowingTransaction.TransactionDate, foundBorrowingTransaction.TransactionDate);
        Assert.Equal(borrowingTransaction.DueDate, foundBorrowingTransaction.DueDate);
        Assert.Equal(duration_in_days, (foundBorrowingTransaction.DueDate - foundBorrowingTransaction.TransactionDate).Days);
    }

    [Fact]
    public void GetBorrowedBook_ByBookIdAndMemberId_ShouldReturnBorrowingTransaction()
    {
        // Arrange
        var randomEmail = $"user{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());
        int duration_in_days = 7;

        BorrowingTransaction borrowingTransaction = _borrowingTransactionService.BorrowBook(book.Id, member.Id, duration_in_days);

        // Act
        BorrowingTransaction? foundBorrowingTransaction = _borrowingTransactionService.GetBorrowedBook(book.Id, member.Id);

        // Assert
        Assert.NotNull(foundBorrowingTransaction);
        Assert.Equal(borrowingTransaction.Id, foundBorrowingTransaction.Id);
        Assert.Equal(book.Id, foundBorrowingTransaction.Book.Id);
        Assert.Equal(member.Id, foundBorrowingTransaction.Member.Id);
        Assert.Equal(borrowingTransaction.TransactionDate, foundBorrowingTransaction.TransactionDate);
        Assert.Equal(borrowingTransaction.DueDate, foundBorrowingTransaction.DueDate);
        Assert.Equal(duration_in_days, (foundBorrowingTransaction.DueDate - foundBorrowingTransaction.TransactionDate).Days);
    }

    [Fact]
    public void GetBorrowedBook_WhenBorrowedBookDoesNotExist_ShouldReturnNull()
    {
        BorrowingTransaction borrowingTransaction = Helpers.CreateBorrowingTransaction();

        BorrowingTransaction? foundBorrowingTransaction = _borrowingTransactionService.GetBorrowedBook(borrowingTransaction.Id);

        Assert.Null(foundBorrowingTransaction);
    }


}
