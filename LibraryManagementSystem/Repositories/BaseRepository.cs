using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
	public class BaseRepository
    {
        protected readonly IFileContext<Book> _fileContext;
        protected readonly string _filePath;

        public BaseRepository(IFileContext<Book> fileContext, string filePath = "data.json")
        {
            _fileContext = fileContext;
            _filePath = filePath;
        }
	}
}

