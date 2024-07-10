using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
	public class BookRepository
	{
		private readonly IFileContext<Book> _fileContext;
		private readonly string _filePath = "books.json";

        public BookRepository(IFileContext<Book> fileContext)
        {
            _fileContext = fileContext;
        }
    }
}

