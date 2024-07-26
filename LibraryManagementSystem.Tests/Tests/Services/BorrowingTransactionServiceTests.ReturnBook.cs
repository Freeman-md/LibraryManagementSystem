using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionServiceTests
{
    [Fact]
    public void ReturnBook_ShouldReturnBorrowedBook()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());
        int durationInDays = 5;
        double expectedFine = 0.0;

        BorrowingTransaction borrowingTransaction = _borrowingTransactionService.BorrowBook(book.Id, member.Id, durationInDays);

        // Act
        BorrowingTransaction returnedBorrowingTransaction = _borrowingTransactionService.ReturnBook(book.Id, member.Id);
        Book? availableBook = _bookService.GetBookById(book.Id);

        // Assert
        Assert.NotNull(returnedBorrowingTransaction.ReturnDate);
        Assert.Equal(DateTime.Today.Date, returnedBorrowingTransaction.ReturnDate?.Date);
        Assert.Equal(expectedFine, returnedBorrowingTransaction.Fine);
        Assert.True(availableBook?.IsAvailable);
        Assert.True(returnedBorrowingTransaction.ReturnDate <= borrowingTransaction.DueDate,
                    "Return date should be less than or equal to the due date");
    }

    [Fact]
    public void ReturnBook_WhenBookIsReturnedPastDueDate_ShouldCalculateFine()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());

        int durationInDays = 5;
        int daysPastDueDate = 4;
        double expectedFine = BorrowingTransaction.FINE_RATE_PER_DAY * daysPastDueDate;

        BorrowingTransaction borrowingTransaction = _borrowingTransactionService.BorrowBook(book.Id, member.Id, durationInDays);

        DateTime pastDueDate = borrowingTransaction.DueDate.AddDays(daysPastDueDate); // 3 days late
        DateTimeProvider.SetDateTime(pastDueDate);

        // Act
        BorrowingTransaction returnedBorrowingTransaction = _borrowingTransactionService.ReturnBook(book.Id, member.Id);
        Book? availableBook = _bookService.GetBookById(book.Id);

        DateTimeProvider.Reset();

        // Assert
        Assert.NotNull(returnedBorrowingTransaction.ReturnDate);
        Assert.Equal(pastDueDate, returnedBorrowingTransaction.ReturnDate);

        TimeSpan? timeSpanLate = returnedBorrowingTransaction.ReturnDate - returnedBorrowingTransaction.DueDate;
        Assert.NotNull(timeSpanLate);
        Assert.Equal(daysPastDueDate, timeSpanLate.Value.Days);
        Assert.True(returnedBorrowingTransaction.ReturnDate > returnedBorrowingTransaction.DueDate, "Return date should be greater than the due date when returned past due date");

        Assert.Equal(expectedFine, returnedBorrowingTransaction.Fine);
        Assert.True(availableBook?.IsAvailable);
    }

    [Fact]
    public void ReturnBook_WhenBookIsReturnedWithinGracePeriod_ShouldNotFine()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());

        int durationInDays = 5;
        int gracePeriodInDays = BorrowingTransaction.GRACE_PERIOD_IN_DAYS;
        int daysPastDueDate = gracePeriodInDays - 2; // Within grace period
        double expectedFine = 0.0;

        BorrowingTransaction borrowingTransaction = _borrowingTransactionService.BorrowBook(book.Id, member.Id, durationInDays);

        DateTime returnDateWithinGracePeriod = borrowingTransaction.DueDate.AddDays(daysPastDueDate); // late but within grace period
        DateTimeProvider.SetDateTime(returnDateWithinGracePeriod);

        // Act
        BorrowingTransaction returnedBorrowingTransaction = _borrowingTransactionService.ReturnBook(book.Id, member.Id);
        Book? availableBook = _bookService.GetBookById(book.Id);

        DateTimeProvider.Reset();

        Assert.NotNull(returnedBorrowingTransaction.ReturnDate);
        Assert.Equal(returnDateWithinGracePeriod, returnedBorrowingTransaction.ReturnDate);
        Assert.True(returnedBorrowingTransaction.ReturnDate > returnedBorrowingTransaction.DueDate, "Return date should be greater than the due date when returned past due date");
        Assert.Equal(expectedFine, returnedBorrowingTransaction.Fine);
        Assert.True(availableBook?.IsAvailable);
    }

    [Fact]
    public void ReturnBook_WhenBookIsReturnedOnLastGracePeriodDay_ShouldNotFine()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());

        int durationInDays = 5;
        int gracePeriodInDays = BorrowingTransaction.GRACE_PERIOD_IN_DAYS;
        int daysPastDueDate = gracePeriodInDays; // last grace period day
        double expectedFine = 0.0;

        BorrowingTransaction borrowingTransaction = _borrowingTransactionService.BorrowBook(book.Id, member.Id, durationInDays);

        DateTime returnDateWithinGracePeriod = borrowingTransaction.DueDate.AddDays(daysPastDueDate); // late but within grace period
        DateTimeProvider.SetDateTime(returnDateWithinGracePeriod);

        // Act
        BorrowingTransaction returnedBorrowingTransaction = _borrowingTransactionService.ReturnBook(book.Id, member.Id);
        Book? availableBook = _bookService.GetBookById(book.Id);

        DateTimeProvider.Reset();

        Assert.NotNull(returnedBorrowingTransaction.ReturnDate);
        Assert.Equal(returnDateWithinGracePeriod, returnedBorrowingTransaction.ReturnDate);
        Assert.True(returnedBorrowingTransaction.ReturnDate > returnedBorrowingTransaction.DueDate, "Return date should be greater than the due date when returned past due date");
        Assert.Equal(expectedFine, returnedBorrowingTransaction.Fine);
        Assert.True(availableBook?.IsAvailable);
    }

    [Fact]
    public void ReturnBook_WhenBookIsSignificantlyOverdue_ShouldHandleMaxFineCalculation()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());

        int durationInDays = 5;
        int daysOverdue = BorrowingTransaction.MAX_OVERDUE_DAYS + 2;
        double expectedFine = BorrowingTransaction.MAX_FINE;

        BorrowingTransaction borrowingTransaction = _borrowingTransactionService.BorrowBook(book.Id, member.Id, durationInDays);

        DateTime returnDatePastMaxOverdueDays = borrowingTransaction.DueDate.AddDays(daysOverdue); // book is overdue
        DateTimeProvider.SetDateTime(returnDatePastMaxOverdueDays);

        // Act
        BorrowingTransaction returnedBorrowingTransaction = _borrowingTransactionService.ReturnBook(book.Id, member.Id);
        Book? availableBook = _bookService.GetBookById(book.Id);

        DateTimeProvider.Reset();

        Assert.NotNull(returnedBorrowingTransaction.ReturnDate);
        Assert.Equal(returnDatePastMaxOverdueDays, returnedBorrowingTransaction.ReturnDate);
        Assert.True(returnedBorrowingTransaction.ReturnDate > returnedBorrowingTransaction.DueDate, "Return date should be greater than the due date when returned past due date");
        Assert.Equal(expectedFine, returnedBorrowingTransaction.Fine);
        Assert.True(availableBook?.IsAvailable);
    }

    [Fact]
    public void ReturnBook_WhenReturningBookForNonExistentMember_ShouldThrowArgumentError()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = Helpers.CreateMember(email: randomEmail);
        Book book = _bookService.AddBook(Helpers.CreateBook(isAvailable: false));

        ArgumentException ex = Assert.Throws<ArgumentException>(() => _borrowingTransactionService.ReturnBook(book.Id, member.Id));
        Assert.Equal("memberId", ex.ParamName);
    }

    [Fact]
    public void ReturnBook_WhenBookDoesNotExist_ShouldThrowArgumentError()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = Helpers.CreateBook();

        ArgumentException ex = Assert.Throws<ArgumentException>(() => _borrowingTransactionService.ReturnBook(book.Id, member.Id));
        Assert.Equal("bookId", ex.ParamName);
    }

    [Fact]
    public void ReturnBook_WhenReturningBookThatWasNotBorrowed_ShouldThrowInvalidOperationError()
    {
        // Arrange
        string randomEmail = $"{Guid.NewGuid()}@example.com";
        Member member = _memberService.RegisterMember(Helpers.CreateMember(email: randomEmail));
        Book book = _bookService.AddBook(Helpers.CreateBook());

        // returning a book that has not been borrowed / that is available to be borrowed
        Assert.Throws<InvalidOperationException>(() => _borrowingTransactionService.ReturnBook(book.Id, member.Id));
    }

}
