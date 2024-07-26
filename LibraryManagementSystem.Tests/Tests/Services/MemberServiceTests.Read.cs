using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
	public partial class MemberServiceTests
	{
		[Fact]
        public void GetAllMembers_ShouldReturnCollectionOfMember()
        {
            for (int i = 1; i <= 10; i++)
            {
                var randomEmail = $"user{Guid.NewGuid()}@example.com";
                _memberService.RegisterMember(CreateMember(email: randomEmail));
            }

            List<Member> members = _memberService.GetAllMembers();

            Assert.IsType<List<Member>>(members);
            Assert.NotEmpty(members);
        }

        [Fact]
        public void GetMember_WhenMemberIdIsValid_ShouldReturnMember()
        {
            var randomEmail = $"user{Guid.NewGuid()}@example.com";
            Member member = _memberService.RegisterMember(CreateMember(email: randomEmail));

            Member? foundMember = _memberService.GetMember(member.Id);

            Assert.NotNull(foundMember);
            Assert.Equal(member.Name, foundMember.Name);
        }

        [Fact]
        public void GetMember_WhenMemberDoesNotExist_ShouldReturnNull()
        {
            Member member = CreateMember();

            Member? foundMember = _memberService.GetMember(member.Id);

            Assert.Null(foundMember);
        }
    }
}

