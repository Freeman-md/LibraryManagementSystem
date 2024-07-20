using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem;

public class BorrowingTransactionRepository : BaseRepository<BorrowingTransaction>
{
    public BorrowingTransactionRepository(IFileContext<BorrowingTransaction> fileContext, string filePath = "borrowing-transactions.json") : base(fileContext, filePath) { }

    public List<BorrowingTransaction> GetAllBorrowingTransactions() {
        throw new NotImplementedException();
    }

    public List<BorrowingTransaction> GetAllBorrowingTransactionsForMember(Member member) {
        throw new NotImplementedException();
    }

    public BorrowingTransaction GetBorrowingTransaction(Guid id) {
        throw new NotImplementedException();
    }

    public BorrowingTransaction CreateBorrowingTransaction(BorrowingTransaction borrowingTransaction) {
        throw new NotImplementedException();
    }

    public BorrowingTransaction UpdateBorrowingTransaction(BorrowingTransaction updatedBorrowingTransaction, Guid id) {
        throw new NotImplementedException();
    }

    public void DeleteBorrowingTransaction(Guid id) {
        throw new NotImplementedException();
    }

    public void SaveBorrowingTransactions(List<BorrowingTransaction> borrowingTransactions) {
        throw new NotImplementedException();
    }
}
