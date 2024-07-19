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
            string address = "35 Cranbourn Street, Leicester Square"
        ) => new Member(id, name, email, phoneNumber, address);

        private static Member CreateMember(
            string name = "John Doe",
            string email = "johndoe@gmail.com",
            string phoneNumber = "09089432893",
            string address = "35 Cranbourn Street, Leicester Square"
        ) => CreateMember(Guid.NewGuid(), name, email, phoneNumber, address);


        public MemberRepositoryTests(MemberRepositoryFixture fixture)
		{
			_memberRepository = fixture.MemberRepository;
		}

		
	}
}

