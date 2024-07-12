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
            List<Book> books = GetAllBooks();

            books.Add(book);

            SaveBooks(books);

            return book;
        }

        public Book UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Book DeleteBook(Book book)
        {
            List<Book> books = GetAllBooks();

            books.Remove(book);

            SaveBooks(books);

            return book;
        }

        public void SaveBooks(List<Book> books)
        {
            _fileContext.WriteToFile(_filePath, books);
        }
    }
}

