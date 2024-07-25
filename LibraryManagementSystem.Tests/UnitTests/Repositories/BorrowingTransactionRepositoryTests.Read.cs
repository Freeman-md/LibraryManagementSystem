using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionRepositoryTests
{
    //TODO: At the moment, in the repository we're not checking if the member and book exists before creating a borrowing transaction as this check is done by the service. This can be implemented later in the repository using a TDD first approach

    [Fact]
    public void GetAllBorrowingTransactions_ShouldReturnAllBorrowingTransactions()
    {
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

    [Fact]
    public void GetAllBorrowingTransactions_WhenNoneExist_ShouldReturnEmptyList()
    {
        _borrowingTransactionRepository.SaveBorrowingTransactions(new List<BorrowingTransaction>());
        List<BorrowingTransaction> borrowingTransactions = _borrowingTransactionRepository.GetAllBorrowingTransactions();

        Assert.NotNull(borrowingTransactions);
        Assert.Empty(borrowingTransactions);
    }

    [Fact]
    public void GetAllBorrowingTransactions_ForMember_ShouldReturnAllBorrowingTransactions()
    {
        const int TOTAL_NUMBER_OF_BORROWING_TRANSACTIONS_TO_CREATE = 2;
        List<BorrowingTransaction> borrowingTransactions = new List<BorrowingTransaction>();
        List<Member> members = new List<Member>() {
            Helpers.CreateMember(email: $"ritabernard@example.com"),
            Helpers.CreateMember(email: $"joshruaser@example.com"),
        };

        Member firstMember = members.First();

        foreach (Member member in members)
        {
            for (int i = 0; i < TOTAL_NUMBER_OF_BORROWING_TRANSACTIONS_TO_CREATE; i++)
            {
                borrowingTransactions.Add(Helpers.CreateBorrowingTransaction(member: member));
            }
        }

        _borrowingTransactionRepository.SaveBorrowingTransactions(borrowingTransactions);

        List<BorrowingTransaction> createdBorrowingTransactions = _borrowingTransactionRepository.GetAllBorrowingTransactionsForMember(firstMember);

        Assert.NotNull(createdBorrowingTransactions);
        Assert.Equal(TOTAL_NUMBER_OF_BORROWING_TRANSACTIONS_TO_CREATE, createdBorrowingTransactions.Count);

        foreach (BorrowingTransaction borrowingTransaction in borrowingTransactions)
        {
            Assert.Contains(createdBorrowingTransactions, b => b.Member.Id == firstMember.Id);
        }
    }

    [Fact]
    public void GetAllBorrowingTransactions_ForMember_WhenNoneExists_ShouldReturnEmptyList()
    {
        const int EXPECTED_TOTAL_NUMBER_OF_CREATED_BORROWING_TRANSACTIONS_FOR_FIRST_MEMBER = 2;
        List<BorrowingTransaction> borrowingTransactions = new List<BorrowingTransaction>();
        List<Member> members = new List<Member>() {
            Helpers.CreateMember($"user{Guid.NewGuid()}@example.com"),
            Helpers.CreateMember($"user{Guid.NewGuid()}@example.com"),
        };

        Member firstMember = members.First();
        Member secondMember = members[1];

        borrowingTransactions.Add(Helpers.CreateBorrowingTransaction(member: firstMember));
        borrowingTransactions.Add(Helpers.CreateBorrowingTransaction(member: firstMember));

        _borrowingTransactionRepository.SaveBorrowingTransactions(borrowingTransactions);

        List<BorrowingTransaction> createdBorrowingTransactionsForFirstMember = _borrowingTransactionRepository.GetAllBorrowingTransactionsForMember(firstMember);
        List<BorrowingTransaction> nonExistingBorrowingTransactionsForSecondMember = _borrowingTransactionRepository.GetAllBorrowingTransactionsForMember(secondMember);

        Assert.NotNull(createdBorrowingTransactionsForFirstMember);
        Assert.NotNull(nonExistingBorrowingTransactionsForSecondMember);
        Assert.Equal(EXPECTED_TOTAL_NUMBER_OF_CREATED_BORROWING_TRANSACTIONS_FOR_FIRST_MEMBER, createdBorrowingTransactionsForFirstMember.Count);
        Assert.Empty(nonExistingBorrowingTransactionsForSecondMember);

        foreach (BorrowingTransaction borrowingTransaction in borrowingTransactions)
        {
            Assert.Contains(createdBorrowingTransactionsForFirstMember, b => b.Member.Id == firstMember.Id);
        }
    }

    [Fact]
    public void GetBorrowingTransaction_ByExistingId_ShouldReturnBorrowingTransaction()
    {
        BorrowingTransaction borrowingTransaction = Helpers.CreateBorrowingTransaction();
        _borrowingTransactionRepository.CreateBorrowingTransaction(borrowingTransaction);

        BorrowingTransaction? foundBorrowingTransaction = _borrowingTransactionRepository.GetBorrowingTransaction(borrowingTransaction.Id);

        Assert.NotNull(foundBorrowingTransaction);
        Assert.Equal(foundBorrowingTransaction.Book.Id, borrowingTransaction.Book.Id);
        Assert.Equal(foundBorrowingTransaction.Member.Email, borrowingTransaction.Member.Email);
    }

    [Fact]
    public void GetBorrowingTransaction_ByBookIdAndMemberId_ShouldReturnBorrowingTransaction()
    {
        Member member = Helpers.CreateMember($"user{Guid.NewGuid()}@example.com");
        Book book = Helpers.CreateBook();
        BorrowingTransaction borrowingTransaction = Helpers.CreateBorrowingTransaction(book: book, member: member);
        _borrowingTransactionRepository.CreateBorrowingTransaction(borrowingTransaction);

        BorrowingTransaction? foundBorrowingTransaction = _borrowingTransactionRepository.GetBorrowingTransaction(book.Id, member.Id);

        Assert.NotNull(foundBorrowingTransaction);
        Assert.Equal(foundBorrowingTransaction.Book.Id, borrowingTransaction.Book.Id);
        Assert.Equal(foundBorrowingTransaction.Member.Email, borrowingTransaction.Member.Email);
    }

    [Fact]
    public void GetBorrowingTransaction_WhenBorrowingTransactionDoesNotExist_ShouldReturnNull()
    {
        Guid id = Guid.NewGuid();

        BorrowingTransaction? nullBorrowingTransaction = _borrowingTransactionRepository.GetBorrowingTransaction(id);

        Assert.Null(nullBorrowingTransaction);
    }

    [Fact]
    public void GetBorrowingTransaction_ByBookIdAndMemberId_WhenBorrowingTransactionDoesNotExist_ShouldReturnNull()
    {
        Member member = Helpers.CreateMember($"user{Guid.NewGuid()}@example.com");
        Book book = Helpers.CreateBook();

        BorrowingTransaction? nullBorrowingTransaction = _borrowingTransactionRepository.GetBorrowingTransaction(book.Id, member.Id);

        Assert.Null(nullBorrowingTransaction);
    }
}
