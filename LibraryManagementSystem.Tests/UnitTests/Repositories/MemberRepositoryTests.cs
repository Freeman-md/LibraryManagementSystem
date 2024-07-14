using System;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests.UnitTests.Repositories
{
	public partial class MemberRepositoryTests : IClassFixture<MemberRepositoryFixture>
	{
		private readonly MemberRepository _memberRepository;

		public MemberRepositoryTests(MemberRepositoryFixture fixture)
		{
			_memberRepository = fixture.MemberRepository;
		}

		
	}
}

