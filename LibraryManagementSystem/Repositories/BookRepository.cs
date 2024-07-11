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

        public Book GetBookById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Book AddBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Book UpdateBook(Book book)
        {
            throw new NotImplementedException();
        }

        public Book DeleteBook(Book book)
        {
            throw new NotImplementedException();
        }

        public void SaveBooks(List<Book> books)
        {
            throw new NotImplementedException();
        }
    }
}

