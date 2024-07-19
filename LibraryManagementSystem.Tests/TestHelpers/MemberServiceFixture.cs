using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests.TestHelpers
{
	public class MemberServiceFixture
	{
		public MemberService MemberService;
		private readonly string _testFilePath;

		public MemberServiceFixture()
		{
            _testFilePath = Path.Combine(Path.GetTempPath(), $"member_service_test_{Guid.NewGuid()}.json");

            JsonFileContext<Member> fileContext = new JsonFileContext<Member>();
			MemberRepository memberRepository = new MemberRepository(fileContext, _testFilePath);

			MemberService = new MemberService(memberRepository);
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

