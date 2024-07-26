using System;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Repositories
{
	public partial class MemberRepositoryTests
	{
		[Fact]
		public void CreateMember_ShouldCreateMemberSuccessfully()
		{
            var randomEmail = $"user{Guid.NewGuid()}@example.com";
            Member member = CreateMember(email: randomEmail);
			_memberRepository.CreateMember(member);

			Member? createdMember = _memberRepository.GetMember(member.Id);

			Assert.NotNull(createdMember);
			Assert.Equal(createdMember.Name, member.Name);
            Assert.Equal(createdMember.Email, member.Email);
        }

		[Fact]
		public void CreateMember_WithSameEmail_ShouldThrowInvalidOperationException()
		{
			Member member = CreateMember();
			_memberRepository.CreateMember(member);

			Assert.Throws<InvalidOperationException>(() => _memberRepository.CreateMember(member));
        }

		[Fact]
		public void SaveMembers_ShouldSaveAllMembers()
		{
            const int TOTAL_NUMBER_OF_MEMBERS_TO_CREATE = 5;
            List<Member> members = new List<Member>();

            for (int i = 0; i < TOTAL_NUMBER_OF_MEMBERS_TO_CREATE; i++)
            {
                members.Add(CreateMember());
            }

            _memberRepository.SaveMembers(members);


            List<Member> createdMembers = _memberRepository.GetAllMembers();


            Assert.NotNull(createdMembers);
            Assert.Equal(TOTAL_NUMBER_OF_MEMBERS_TO_CREATE, createdMembers.Count);

            foreach (Member member in members)
            {
                Assert.Contains(createdMembers, m => m.Id == member.Id);
            }
        }
	}
}

