using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
	public class MemberRepository : BaseRepository
	{
        public MemberRepository(IFileContext<Book> fileContext, string filePath = "members.json") : base(fileContext, filePath) { }
	}
}

