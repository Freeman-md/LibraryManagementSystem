using System.Net;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionServiceTests : IClassFixture<BorrowingTransactionServiceFixture>, IClassFixture<MemberServiceFixture>
{
    private readonly BorrowingTransactionService _borrowingTransactionService;
    private readonly MemberService _memberService;
    public BorrowingTransactionServiceTests(BorrowingTransactionServiceFixture borrowingTransactionServiceFixture, MemberServiceFixture memberServiceFixture)
    {
        _borrowingTransactionService = borrowingTransactionServiceFixture.BorrowingTransactionService;
        _memberService = memberServiceFixture.MemberService;
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

        foreach (Member member in members) {
            _memberService.RegisterMember(member);
        }

        foreach (int index in Enumerable.Range(0, 2))
        {
            _borrowingTransactionService.BorrowBook(books[index].Id, members[index].Id, duration_in_days);
        }

        // Act
        List<BorrowingTransaction> borrowingTransactions = _borrowingTransactionService.ViewAllBorrowedBooks();

        // Assert
        Assert.NotNull(borrowingTransactions);
        Assert.Equal(books.Count, borrowingTransactions.Count);
    }

    [Fact]
    public void GetAllBorrowedBooks_WhenNoBorrowedBooks_ShouldReturnEmptyList()
    {
        // Act
        List<BorrowingTransaction> borrowingTransactions = _borrowingTransactionService.ViewAllBorrowedBooks();

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

        foreach (int index in Enumerable.Range(0, 2))
        {
            _borrowingTransactionService.BorrowBook(books[index].Id, member.Id, duration_in_days);
        }

        // Act
        List<BorrowingTransaction> borrowingTransactions = _borrowingTransactionService.ViewAllBorrowedBooksForMember(member.Id);

        // Assert
        Assert.NotNull(borrowingTransactions);
        Assert.Equal(books.Count, borrowingTransactions.Count);

        foreach (BorrowingTransaction borrowingTransaction in borrowingTransactions)
        {
            Assert.Contains(borrowingTransactions, borrowingTransaction => borrowingTransaction.Member.Id == member.Id);
        }
    }

    [Fact]
    public void GetAllBorrowedBooks_ForAMember_WhenMemberHasNoBorrowedBooks_ShouldReturnAllBorrowedBooks()
    {
        // Arrange
        Member member = Helpers.CreateMember(email: $"{Guid.NewGuid()}@example.com");
        _memberService.RegisterMember(member);

        // Act
        List<BorrowingTransaction> borrowingTransactions = _borrowingTransactionService.ViewAllBorrowedBooksForMember(member.Id);

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
        Assert.Throws<ArgumentException>(() => _borrowingTransactionService.ViewAllBorrowedBooksForMember(member.Id));
    }


}
