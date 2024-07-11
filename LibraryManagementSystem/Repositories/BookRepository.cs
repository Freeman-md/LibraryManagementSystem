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
    }
}

