using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
	public partial class MemberServiceTests
	{
		[Fact]
		public void RegisterMember_WithNullItem_ShouldThrowArgumentNullException()
		{
			Assert.Throws<ArgumentNullException>(() => _memberService.RegisterMember(null!));
		}

		[Fact]
		public void RegisterMember_WithEmptyName_ShouldThrowArgumentException()
        {
            Member invalidMember = CreateMember(name: "");

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _memberService.RegisterMember(invalidMember));
            Assert.Equal("Name", ex.ParamName);
        }

        [Fact]
        public void RegisterMember_WithEmptyEmail_ShouldThrowArgumentException()
        {
            Member invalidMember = CreateMember(email: "");

            ArgumentException ex = Assert.Throws<ArgumentException>(() => _memberService.RegisterMember(invalidMember));
            Assert.Equal("Email", ex.ParamName);
        }

        [Fact]
        public void RegisterMember_WithAllDetails_ShouldRegisterMemberSuccessfully()
        {
            var randomEmail = $"user{Guid.NewGuid()}@example.com";
            Member member = CreateMember(email: randomEmail);

            _memberService.RegisterMember(member);
            Member? createdMember = _memberService.GetMember(member.Id);

            Assert.NotNull(createdMember);
            Assert.Equal(createdMember.Name, member.Name);
            Assert.Equal(createdMember.Email, member.Email);
        }
    }
}

