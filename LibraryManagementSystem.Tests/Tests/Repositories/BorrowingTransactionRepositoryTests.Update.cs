using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests;

public partial class BorrowingTransactionRepositoryTests
{
    [Fact]
		public void Update_ExistingBorrowingTransaction_ShouldUpdateDetails()
		{
            BorrowingTransaction borrowingTransaction = Helpers.CreateBorrowingTransaction();
            _borrowingTransactionRepository.CreateBorrowingTransaction(borrowingTransaction);
            BorrowingTransaction updatedBorrowingTransaction = Helpers.CreateBorrowingTransaction(fine: 90, returnDate: DateTime.Now.AddDays(10));

			_borrowingTransactionRepository.UpdateBorrowingTransaction(updatedBorrowingTransaction, borrowingTransaction.Id);
            BorrowingTransaction? result = _borrowingTransactionRepository.GetBorrowingTransaction(borrowingTransaction.Id);

			Assert.NotNull(result);
            Assert.NotNull(result.ReturnDate);
			Assert.NotEqual(result.Fine, borrowingTransaction.Fine);
            Assert.NotEqual(result.ReturnDate, borrowingTransaction.ReturnDate);
            Assert.Equal(result.Fine, updatedBorrowingTransaction.Fine);
            Assert.Equal(result.Fine, updatedBorrowingTransaction.Fine);
        }

        [Fact]
        public void Update_NonExistingBorrowingTransaction_ShouldThrowArgumentException()
        {
            Guid id = Guid.NewGuid();
            BorrowingTransaction updatedBorrowingTransaction = Helpers.CreateBorrowingTransaction();

			Assert.Throws<ArgumentException>(() => _borrowingTransactionRepository.UpdateBorrowingTransaction(updatedBorrowingTransaction, id));
        }
}
