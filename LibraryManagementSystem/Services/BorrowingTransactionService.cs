using System.Diagnostics.Contracts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem;

public class BorrowingTransactionService
{
    private readonly BorrowingTransactionRepository _borrowingTransactionRepository;
    private readonly BookService _bookService;
    private readonly MemberService _memberService;
    public BorrowingTransactionService(BorrowingTransactionRepository borrowingTransactionRepository, BookService bookService, MemberService memberService) {
        _borrowingTransactionRepository = borrowingTransactionRepository;
        _bookService = bookService;
        _memberService = memberService;
    }

    public BorrowingTransaction BorrowBook(Guid bookId, Guid memberId, int duration) {
        DateTime dueDate = DateTime.Now.AddDays(duration);

        Book? book = _bookService.GetBookById(bookId);
        Member? member = _memberService.GetMember(memberId);

        BorrowingTransaction borrowingTransaction = new BorrowingTransaction(book!, member!, dueDate);

        return _borrowingTransactionRepository.CreateBorrowingTransaction(borrowingTransaction);
    }

    public BorrowingTransaction BorrowBook(Guid bookId, Guid memberId) {
        throw new NotImplementedException();
    }

    public BorrowingTransaction ReturnBook(Guid bookId, Guid memberId) {
        throw new NotImplementedException();
    }

    public List<BorrowingTransaction> GetAllBorrowedBooks() {
        return _borrowingTransactionRepository.GetAllBorrowingTransactions();
    }

    public List<BorrowingTransaction> GetAllBorrowedBooksForMember(Guid memberId) {
        throw new NotImplementedException();
    }

    public BorrowingTransaction GetBorrowedBook(Guid borrowingTransactionId) {
        throw new NotImplementedException();
    }

    public BorrowingTransaction GetBorrowedBook(Guid bookId, Guid memberId) {
        throw new NotImplementedException();
    }
}
