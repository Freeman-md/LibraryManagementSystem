using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests.UnitTests.Repositories
{
	public partial class MemberRepositoryTests : IClassFixture<MemberRepositoryFixture>
	{
		private readonly MemberRepository _memberRepository;

        private static Member CreateMember(
            Guid id,
            string name = "John Doe",
            string email = "johndoe@gmail.com",
            string phoneNumber = "09089432893",
            string address = "35 Cranbourn Street, Leicester Square",
            DateTime publishDate = default(DateTime)
        ) => new Member(id, name, email, phoneNumber, address, publishDate);

        private static Member CreateMember(
            string name = "John Doe",
            string email = "johndoe@gmail.com",
            string phoneNumber = "09089432893",
            string address = "35 Cranbourn Street, Leicester Square",
            DateTime publishDate = default(DateTime)
        ) => CreateMember(Guid.NewGuid(), name, email, phoneNumber, address, publishDate);


        public MemberRepositoryTests(MemberRepositoryFixture fixture)
		{
			_memberRepository = fixture.MemberRepository;
		}

		
	}
}

