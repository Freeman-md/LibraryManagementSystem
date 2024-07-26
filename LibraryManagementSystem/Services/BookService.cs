using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
	public class BookService
	{
		private readonly BookRepository _bookRepository;

		public BookService(BookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public Book AddBook(Book book) {
            if (book == null) throw new ArgumentNullException(nameof(book));

            ValidateBook(book);

            _bookRepository.AddBook(book);

            return book;
		}

        public void DeleteBook(Guid bookId)
        {
            _bookRepository.DeleteBook(bookId);
        }

        public List<Book> GetAllBooks()
		{
            return _bookRepository.GetAllBooks();
        }

        public List<Book> GetAllAvailableBooks() {
            return _bookRepository.GetAllBooks().Where(book => book.IsAvailable).ToList();
        }

		public Book? GetBookById(Guid id)
		{
            return _bookRepository.GetBookById(id);
		}

        public void UpdateBook(Book updatedBook, Guid id)
        {
            ValidateBook(updatedBook);

            _bookRepository.UpdateBook(updatedBook, id);
        }

        private void ValidateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new ArgumentException("Book title cannot be empty.", nameof(book.Title));
            }

            if (string.IsNullOrWhiteSpace(book.Author))
            {
                throw new ArgumentException("Book author cannot be empty.", nameof(book.Author));
            }

            if (string.IsNullOrWhiteSpace(book.Genre))
            {
                throw new ArgumentException("Book genre cannot be empty.", nameof(book.Genre));
            }

            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new ArgumentException("Book ISBN cannot be empty.", nameof(book.ISBN));
            }

        }

	}
}

