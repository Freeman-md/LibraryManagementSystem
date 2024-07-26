using System;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Repositories
{
	public partial class MemberRepositoryTests
	{
		[Fact]
		public void Update_ExistingMember_ShouldUpdateDetails()
		{
            var randomEmail = $"user{Guid.NewGuid()}@example.com";
            Member member = CreateMember(email: randomEmail);
            _memberRepository.CreateMember(member);
            Member updatedMember = CreateMember(name: "Chris Taylor", email: "christaylor@gmail.com");

			_memberRepository.UpdateMember(updatedMember, member.Id);
            Member? result = _memberRepository.GetMember(member.Id);

			Assert.NotNull(result);
			Assert.Equal(result.Name, updatedMember.Name);
            Assert.Equal(result.Address, updatedMember.Address);
        }

        [Fact]
        public void Update_NonExistingMember_ShouldThrowArgumentException()
        {
            Guid id = Guid.NewGuid();
            Member updatedMember = CreateMember(name: "Chris Taylor", email: "christaylor@gmail.com");

			Assert.Throws<ArgumentException>(() => _memberRepository.UpdateMember(updatedMember, id));
        }
    }
}

