using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
	public class BaseRepository<T>
    {
        protected readonly IFileContext<T> _fileContext;
        protected readonly string _filePath;

        public BaseRepository(IFileContext<T> fileContext, string filePath = "data.json")
        {
            _fileContext = fileContext;
            _filePath = filePath;
        }
	}
}

