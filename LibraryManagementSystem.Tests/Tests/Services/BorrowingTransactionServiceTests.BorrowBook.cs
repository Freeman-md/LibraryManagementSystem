using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionServiceTests
{
    [Fact]
    public void BorrowBook_WithAllParameters_ShouldBorrowBook()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());
        int duration_in_days = 5;

        // Act
        BorrowingTransaction borrowingTransaction = _borrowingTransactionService.BorrowBook(book.Id, member.Id, duration_in_days);
        BorrowingTransaction? foundBorrowingTransaction = _borrowingTransactionService.GetBorrowedBook(borrowingTransaction.Id);
        Book? unavailableBook = _bookService.GetBookById(foundBorrowingTransaction!.Book.Id);

        Assert.NotNull(foundBorrowingTransaction);
        Assert.Equal(borrowingTransaction.Id, foundBorrowingTransaction.Id);
        Assert.Equal(book.Id, foundBorrowingTransaction.Book.Id);
        Assert.Equal(member.Id, foundBorrowingTransaction.Member.Id);
        Assert.Equal(borrowingTransaction.TransactionDate.Date, foundBorrowingTransaction.TransactionDate.Date);
        Assert.Equal(borrowingTransaction.DueDate.Date, foundBorrowingTransaction.DueDate.Date);
        Assert.Equal(duration_in_days, (foundBorrowingTransaction.DueDate - foundBorrowingTransaction.TransactionDate).Days + 1);

        Assert.NotNull(unavailableBook);
        Assert.False(unavailableBook.IsAvailable, "Expected book to be unavailable after it is borrowed");
    }

    [Fact]
    public void BorrowBook_WhenNoDurationIsSpecified_ShouldUseDefaultDuration()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());
        const int EXPECTED_BORROWING_DURATION_IN_DAYS = BorrowingTransaction.DEFAULT_BORROWING_DURATION_IN_DAYS;

        // Act
        BorrowingTransaction borrowingTransaction = _borrowingTransactionService.BorrowBook(book.Id, member.Id);
        BorrowingTransaction? foundBorrowingTransaction = _borrowingTransactionService.GetBorrowedBook(borrowingTransaction.Id);
        Book? unavailableBook = _bookService.GetBookById(foundBorrowingTransaction!.Book.Id);

        Assert.NotNull(foundBorrowingTransaction);
        Assert.Equal(borrowingTransaction.Id, foundBorrowingTransaction.Id);
        Assert.Equal(book.Id, foundBorrowingTransaction.Book.Id);
        Assert.Equal(member.Id, foundBorrowingTransaction.Member.Id);
        Assert.Equal(borrowingTransaction.TransactionDate.Date, foundBorrowingTransaction.TransactionDate.Date);
        Assert.Equal(borrowingTransaction.DueDate.Date, foundBorrowingTransaction.DueDate.Date);
        Assert.Equal(EXPECTED_BORROWING_DURATION_IN_DAYS, (foundBorrowingTransaction.DueDate - foundBorrowingTransaction.TransactionDate).Days + 1);

        Assert.NotNull(unavailableBook);
        Assert.False(unavailableBook.IsAvailable, "Expected book to be unavailable after it is borrowed");
    }

    [Fact]
    public void BorrowBook_WhenBookDoesNotExist_ShouldThrowArgumentError() {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Guid nonExistentBookId = Guid.NewGuid();

        ArgumentException ex = Assert.Throws<ArgumentException>(() => _borrowingTransactionService.BorrowBook(nonExistentBookId, member.Id));
        Assert.Equal("bookId", ex.ParamName);
    }

    [Fact]
    public void BorrowBook_WhenBookIsUnavailable_ShouldThrowArgumentError() {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook(isAvailable: false));

        Assert.Throws<InvalidOperationException>(() => _borrowingTransactionService.BorrowBook(book.Id, member.Id));
    }

    [Fact]
    public void BorrowBook_WhenMemberDoesNotExist_ShouldThrowArgumentError() {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = Helpers.CreateMember(email: randomEmail);
        Book book = _bookService.AddBook(Helpers.CreateBook());

        ArgumentException ex = Assert.Throws<ArgumentException>(() => _borrowingTransactionService.BorrowBook(book.Id, member.Id));
        Assert.Equal("memberId", ex.ParamName);
    }

    [Fact]
    public void BorrowBook_WhenDurationIsInvalid_ShouldThrowArgumentOutOfRangeError() {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = Helpers.CreateMember(email: randomEmail);
        Book book = _bookService.AddBook(Helpers.CreateBook());
        int duration_in_days = -5;

        Assert.Throws<ArgumentOutOfRangeException>(() => _borrowingTransactionService.BorrowBook(book.Id, member.Id, duration_in_days));
    }
}
