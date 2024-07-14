using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests.UnitTests.Repositories
{
    public class BookRepositoryTests : IClassFixture<BookRepositoryFixture>
    {
        private readonly LibraryManagementSystem.Repositories.BookRepository _bookRepository;

        public BookRepositoryTests(BookRepositoryFixture fixture)
        {
            _bookRepository = fixture.BookRepository;
        }

        private static Book CreateBook(
            string title = "Original Title",
            string author = "Original Author",
            string genre = "Original Genre",
            string isbn = "090-93080-3893",
            DateTime publishDate = default(DateTime),
            bool isAvailable = true) => new Book(title, author, genre, isbn, publishDate, isAvailable);

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            
            Book book = CreateBook();
            _bookRepository.AddBook(book);

            List<Book> books = _bookRepository.GetAllBooks();

            Assert.Contains(book, books);
        }

        [Fact]
        public void GetAllBooks_WhenNoBooks_ShouldReturnEmptyList()
        {
            _bookRepository.SaveBooks(new List<Book>());

            List<Book> books = _bookRepository.GetAllBooks();

            Assert.Empty(books);
        }

        [Fact]
        public void GetBook_ByExistingId_ShouldReturnBook()
        {
            Book book = CreateBook();
            _bookRepository.AddBook(book);

            Book? foundBook = _bookRepository.GetBookById(book.Id);

            Assert.NotNull(foundBook);
            Assert.Equal(book.Id, foundBook.Id);
            Assert.Equal(book.Title, foundBook.Title);
        }

        [Fact]
        public void GetBook_WhenBookDoesNotExist_ShouldReturnNull()
        {
            // creates book but doesn't save it - AddBook saves to file. CreateBook is a private method here that instantiates a Book
            Book book = CreateBook();

            Book? foundBook = _bookRepository.GetBookById(book.Id);

            Assert.Null(foundBook);
        }

        [Fact]
        public void AddBook_ShouldAddBookToFile()
        {
            Book book = CreateBook();

            _bookRepository.AddBook(book);
            List<Book> books = _bookRepository.GetAllBooks();

            Assert.Contains(book, books);
        }

        [Fact]
        public void UpdateBook_ShouldUpdateBookDetails()
        {
            Book book = CreateBook();
            _bookRepository.AddBook(book);
            Book updatedBook = CreateBook(title: "Updated Title", author: "Updated Author");

            _bookRepository.UpdateBook(updatedBook, book.Id);
            Book? result = _bookRepository.GetBookById(book.Id);

            Assert.NotNull(result);
            Assert.Equal(result.Title, updatedBook.Title);
            Assert.Equal(result.Author, updatedBook.Author);
        }

        [Fact]
        public void DeleteBook_ShouldRemoveBookFromFile()
        {
            Book book = CreateBook();
            _bookRepository.AddBook(book);

            _bookRepository.DeleteBook(book.Id);

            List<Book> books = _bookRepository.GetAllBooks();
            Assert.DoesNotContain(book, books);
        }

        [Fact]
        public void SaveBooks_ShouldWriteBooksToFile()
        {
            Book book = CreateBook();
            List<Book> books = new List<Book> { book };

            _bookRepository.SaveBooks(books);
            List<Book> savedBooks = _bookRepository.GetAllBooks();

            Assert.Contains(book, savedBooks);
        }
    }
}

