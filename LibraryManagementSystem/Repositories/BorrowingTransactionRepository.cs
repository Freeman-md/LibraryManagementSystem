﻿using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem;

public class BorrowingTransactionRepository : BaseRepository<BorrowingTransaction>
{
    public BorrowingTransactionRepository(IFileContext<BorrowingTransaction> fileContext, string filePath = "borrowing-transactions.json") : base(fileContext, filePath) { }

    public List<BorrowingTransaction> GetAllBorrowingTransactions() {
        return _fileContext.ReadFromFile(_filePath);
    }

    public List<BorrowingTransaction> GetAllBorrowingTransactionsForMember(Member member) {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();

        return borrowingTransactions.Where((borrowingTransaction) => borrowingTransaction.Member.Id == member.Id).ToList();
    }

    public BorrowingTransaction? GetBorrowingTransaction(Guid id) {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();

        return borrowingTransactions.FirstOrDefault((borrowingTransaction) => borrowingTransaction.Id == id);
    }

    public BorrowingTransaction CreateBorrowingTransaction(BorrowingTransaction borrowingTransaction) {
        List<BorrowingTransaction> borrowingTransactions = GetAllBorrowingTransactions();

        borrowingTransactions.Add(borrowingTransaction);

        SaveBorrowingTransactions(borrowingTransactions);

        return borrowingTransaction;
    }

    public BorrowingTransaction UpdateBorrowingTransaction(BorrowingTransaction updatedBorrowingTransaction, Guid id) {
        throw new NotImplementedException();
    }

    public void DeleteBorrowingTransaction(Guid id) {
        throw new NotImplementedException();
    }

    public void SaveBorrowingTransactions(List<BorrowingTransaction> borrowingTransactions) {
        _fileContext.WriteToFile(_filePath, borrowingTransactions);
    }
}
