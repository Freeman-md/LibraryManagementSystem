using System.Diagnostics.Contracts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem;

public class BorrowingTransactionService
{
    private readonly BorrowingTransactionRepository _borrowingTransactionRepository;
    private readonly BookService _bookService;
    private readonly MemberService _memberService;
    public BorrowingTransactionService(BorrowingTransactionRepository borrowingTransactionRepository, BookService bookService, MemberService memberService)
    {
        _borrowingTransactionRepository = borrowingTransactionRepository;
        _bookService = bookService;
        _memberService = memberService;
    }

    public List<BorrowingTransaction> GetAllBorrowedBooks()
    {
        return _borrowingTransactionRepository.GetAllBorrowingTransactions();
    }

    public List<BorrowingTransaction> GetAllBorrowedBooksForMember(Guid memberId)
    {
        Member? member = _memberService.GetMember(memberId);

        if (member == null) throw new ArgumentException(nameof(memberId));

        return _borrowingTransactionRepository.GetAllBorrowingTransactionsForMember(memberId);
    }

    public BorrowingTransaction? GetBorrowedBook(Guid borrowingTransactionId)
    {
        return _borrowingTransactionRepository.GetBorrowingTransaction(borrowingTransactionId);
    }

    public BorrowingTransaction? GetBorrowedBook(Guid bookId, Guid memberId)
    {
        return _borrowingTransactionRepository.GetBorrowingTransaction(bookId, memberId);
    }

    public BorrowingTransaction BorrowBook(Guid bookId, Guid memberId, int duration)
    {
        ValidateDuration(duration);

        Book? book = GetAndValidateBookForBorrowing(bookId);
        Member? member = GetAndValidateMember(memberId);
        DateTime dueDate = DateTime.Now.AddDays(duration);

        BorrowingTransaction borrowingTransaction = _borrowingTransactionRepository.CreateBorrowingTransaction(new BorrowingTransaction(book, member!, dueDate));

        book.markAsBorrowed();
        _bookService.UpdateBook(book, bookId);

        return borrowingTransaction;
    }

    public BorrowingTransaction BorrowBook(Guid bookId, Guid memberId)
    {
        return BorrowBook(bookId, memberId, BorrowingTransaction.DEFAULT_BORROWING_DURATION_IN_DAYS);
    }

    public BorrowingTransaction ReturnBook(Guid bookId, Guid memberId)
    {
        Book book = GetAndValidateBookForReturning(bookId);
        Member member = GetAndValidateMember(memberId);

        BorrowingTransaction? borrowingTransactionToUpdate = _borrowingTransactionRepository.GetBorrowingTransaction(bookId, memberId);

        if (borrowingTransactionToUpdate == null) throw new ArgumentException($"Book was not borrowed by member");

        borrowingTransactionToUpdate.ReturnDate = DateTime.Now;

        BorrowingTransaction updatedBorrowingTransaction = _borrowingTransactionRepository.UpdateBorrowingTransaction(borrowingTransactionToUpdate, bookId, memberId);

        book.markAsReturned();
        _bookService.UpdateBook(book, bookId);

        return updatedBorrowingTransaction;
    }

    private void ValidateDuration(int duration)
    {
        if (duration <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(duration), "Duration must be a positive integer.");
        }
    }

    private Book GetAndValidateBookForBorrowing(Guid bookId)
    {
        Book? book = _bookService.GetBookById(bookId);
        if (book == null)
        {
            throw new ArgumentException("Book does not exist.", nameof(bookId));
        }

        if (!book.IsAvailable)
        {
            throw new InvalidOperationException("Book is not available for borrowing");

        }

        return book;
    }

    private Book GetAndValidateBookForReturning(Guid bookId)
    {
        Book? book = _bookService.GetBookById(bookId);
        if (book == null)
        {
            throw new ArgumentException("Book does not exist.", nameof(bookId));
        }

        if (book.IsAvailable)
        {
            throw new InvalidOperationException("Book has not been borrowed and cannot be returned");
        }

        return book;
    }

    private Member GetAndValidateMember(Guid memberId)
    {
        Member? member = _memberService.GetMember(memberId);
        if (member == null)
        {
            throw new ArgumentException("Member does not exist", nameof(memberId));
        }

        return member;
    }
}
