using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem;

public class BorrowingTransactionRepository : BaseRepository<BorrowingTransaction>
{
    public BorrowingTransactionRepository(IFileContext<BorrowingTransaction> fileContext, string filePath = "borrowing-transactions.json") : base(fileContext, filePath) { }

    public List<BorrowingTransaction> GetAllBorrowingTransactions()
    {
        return _fileContext.ReadFromFile(_filePath);
    }

    public List<BorrowingTransaction> GetAllBorrowingTransactionsForMember(Guid memberId)
    {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();

        return borrowingTransactions.Where((borrowingTransaction) => borrowingTransaction.Member.Id == memberId).ToList();
    }

    public BorrowingTransaction? GetBorrowingTransaction(Guid id)
    {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();

        return borrowingTransactions
        .Where(borrowingTransaction => borrowingTransaction.Id == id)
        .OrderByDescending(borrowingTransaction => borrowingTransaction.TransactionDate)
        .FirstOrDefault();
    }

    public BorrowingTransaction? GetBorrowingTransaction(Guid bookId, Guid memberId)
    {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();

        return borrowingTransactions
        .Where(borrowingTransaction => borrowingTransaction.Book.Id == bookId && borrowingTransaction.Member.Id == memberId)
        .OrderByDescending(borrowingTransaction => borrowingTransaction.TransactionDate)
        .FirstOrDefault();
    }

    public BorrowingTransaction CreateBorrowingTransaction(BorrowingTransaction borrowingTransaction)
    {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();

        borrowingTransactions.Add(borrowingTransaction);

        SaveBorrowingTransactions(borrowingTransactions);

        return borrowingTransaction;
    }

    public BorrowingTransaction UpdateBorrowingTransaction(BorrowingTransaction updatedBorrowingTransaction, Guid id)
    {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();

        BorrowingTransaction? borrowingTransactionToUpdate = borrowingTransactions.Where(borrowingTransaction => borrowingTransaction.Id == id)
        .OrderByDescending(borrowingTransaction => borrowingTransaction.TransactionDate)
        .FirstOrDefault();

        if (borrowingTransactionToUpdate == null)
        {
            throw new ArgumentException("Borrowing Transaction does not exist.", nameof(id));
        }

        borrowingTransactionToUpdate.Fine = updatedBorrowingTransaction.Fine;
        borrowingTransactionToUpdate.ReturnDate = updatedBorrowingTransaction.ReturnDate;

        SaveBorrowingTransactions(borrowingTransactions);

        return borrowingTransactionToUpdate;
    }

    public BorrowingTransaction UpdateBorrowingTransaction(BorrowingTransaction updatedBorrowingTransaction, Guid bookId, Guid memberId)
    {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();

        BorrowingTransaction? borrowingTransactionToUpdate = borrowingTransactions.Where(borrowingTransaction => borrowingTransaction.Book.Id == bookId && borrowingTransaction.Member.Id == memberId)
        .OrderByDescending(borrowingTransaction => borrowingTransaction.TransactionDate)
        .FirstOrDefault();

        if (borrowingTransactionToUpdate == null)
        {
            throw new ArgumentException("Borrowing Transaction does not exist.");
        }

        borrowingTransactionToUpdate.Fine = updatedBorrowingTransaction.Fine;
        borrowingTransactionToUpdate.ReturnDate = updatedBorrowingTransaction.ReturnDate;

        SaveBorrowingTransactions(borrowingTransactions);

        return borrowingTransactionToUpdate;
    }

    public void DeleteBorrowingTransaction(Guid id)
    {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();
        BorrowingTransaction? borrowingTransactionToDelete = GetBorrowingTransaction(id);

        if (borrowingTransactionToDelete == null) throw new ArgumentException("Borrowing Transaction does not exist.", nameof(id));

        borrowingTransactions.Remove(borrowingTransactionToDelete);

        SaveBorrowingTransactions(borrowingTransactions);
    }

    public void SaveBorrowingTransactions(List<BorrowingTransaction> borrowingTransactions)
    {
        _fileContext.WriteToFile(_filePath, borrowingTransactions);
    }
}
