using System;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Repositories
{
	public partial class MemberRepositoryTests
	{
		[Fact]
		public void Delete_ExistingMember_ShouldDeleteMemberSuccessfully()
		{
            var randomEmail = $"user{Guid.NewGuid()}@example.com";
            Member member = CreateMember(email: randomEmail);
            _memberRepository.CreateMember(member);

            _memberRepository.DeleteMember(member.Id);
            Member? deletedMember = _memberRepository.GetMember(member.Id);

            Assert.Null(deletedMember);
        }

        [Fact]
        public void Delete_NonExistingMember_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _memberRepository.DeleteMember(Guid.NewGuid()));
        }
    }
}

