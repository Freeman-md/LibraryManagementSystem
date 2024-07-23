using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.TestHelpers
{
    public static class Helpers
    {
        public static Member CreateMember(
            Guid id,
            string name = "John Doe",
            string email = "johndoe@gmail.com",
            string phoneNumber = "09089432893",
            string address = "35 Cranbourn Street, Leicester Square"
        ) => new Member(id, name, email, phoneNumber, address);

        public static Member CreateMember(
            string name = "John Doe",
            string email = "johndoe@gmail.com",
            string phoneNumber = "09089432893",
            string address = "35 Cranbourn Street, Leicester Square"
        ) => CreateMember(Guid.NewGuid(), name, email, phoneNumber, address);

        public static Book CreateBook(
            string title = "Original Title",
            string author = "Original Author",
            string genre = "Original Genre",
            string isbn = "090-93080-3893",
            DateTime publishDate = default(DateTime),
            bool isAvailable = true) => new Book(title, author, genre, isbn, publishDate, isAvailable);

        public static BorrowingTransaction CreateBorrowingTransaction(
            Book? book = null,
            Member? member = null,
            int borrowingDurationInDays = 7,
            double fine = 0.0,
            DateTime? returnDate = null)
        {
            book ??= CreateBook();

            var randomEmail = $"user{Guid.NewGuid()}@example.com";
            member ??= CreateMember(email: randomEmail);

            DateTime dueDate = DateTime.Now.AddDays(borrowingDurationInDays);

            return new BorrowingTransaction(book, member, dueDate, fine, returnDate);
        }
    }
}
