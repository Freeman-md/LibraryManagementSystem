using System;
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
	}
}

