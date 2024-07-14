using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Tests.TestHelpers
{
	public class MemberRepositoryFixture
	{
        public MemberRepository MemberRepository { get; private set; }
        private readonly string _testFilePath;

        public MemberRepositoryFixture()
        {
            _testFilePath = Path.Combine(Path.GetTempPath(), $"members_repository_test_{Guid.NewGuid()}.json");

            JsonFileContext<Book> fileContext = new JsonFileContext<Book>();
            MemberRepository = new MemberRepository(fileContext, _testFilePath);
        }

        public void Dispose()
        {
            if (System.IO.File.Exists(_testFilePath))
            {
                System.IO.File.Delete(_testFilePath);
            }
        }
    }
}

