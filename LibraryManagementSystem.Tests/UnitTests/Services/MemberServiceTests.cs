using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Tests.TestHelpers;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
	public partial class MemberServiceTests : IClassFixture<MemberServiceFixture>
	{
		private readonly MemberService _memberService;

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

        public MemberServiceTests(MemberServiceFixture fixture)
		{
            _memberService = fixture.MemberService;
		}
	}
}

