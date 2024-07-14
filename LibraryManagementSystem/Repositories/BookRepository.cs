using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
	public class BookRepository
	{
		private readonly IFileContext<Book> _fileContext;
		private readonly string _filePath;

        public BookRepository(IFileContext<Book> fileContext, string filePath = "books.json")
        {
            _fileContext = fileContext;
            _filePath = filePath;
        }

        public List<Book> GetAllBooks()
        {
            return _fileContext.ReadFromFile(_filePath);
        }

        public Book? GetBookById(Guid id)
        {
            List<Book> books = GetAllBooks();

            return books.FirstOrDefault((book) => book.Id == id);
        }

        public Book AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            List<Book> books = GetAllBooks();

            books.Add(book);

            SaveBooks(books);

            return book;
        }

        public Book UpdateBook(Book updatedBook, Guid bookId)
        {
            List<Book> books = GetAllBooks();
            Book? existingBook = GetBookById(bookId);

            if (existingBook == null) throw new ArgumentException("Book does not exist.", nameof(updatedBook));

            books.Remove(existingBook);
            books.Add(updatedBook);

            SaveBooks(books);

            return updatedBook;
        }

        public void DeleteBook(Guid bookId)
        {
            var books = GetAllBooks();
            var bookToDelete = GetBookById(bookId);

            if (bookToDelete == null) throw new ArgumentException("Book does not exist.", nameof(bookId));

            books.Remove(bookToDelete);
            SaveBooks(books);
        }

        public void SaveBooks(List<Book> books)
        {
            _fileContext.WriteToFile(_filePath, books);
        }
    }
}

