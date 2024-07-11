using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
	public class BookService
	{
		private BookRepository _bookRepository;

		public BookService(BookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public Book AddBook(Book book) {
			// TODO: add book

			throw new NotImplementedException();
		}

		public List<Book> GetBooks()
		{
            // TODO: get books from repository

            throw new NotImplementedException();
        }

	}
}

