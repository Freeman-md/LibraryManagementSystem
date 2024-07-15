using System;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Repositories
{
	public partial class MemberRepositoryTests
	{
		[Fact]
		public void CreateMember_ShouldCreateMemberSuccessfully()
		{
			Member member = CreateMember();
			_memberRepository.CreateMember(member);

			Member createdMember = _memberRepository.GetMemberById(member.Id);

			Assert.NotNull(createdMember);
			Assert.Equal(createdMember.Name, member.Name);
            Assert.Equal(createdMember.Email, member.Email);
        }

		[Fact]
		public void CreateMember_WithDuplicateId_ShouldThrowInvalidOperationException()
		{
			Guid id = Guid.NewGuid();
			Member member1 = CreateMember(id);
            Member member2 = CreateMember(id);
			_memberRepository.CreateMember(member1);

			Assert.Throws<InvalidOperationException>(() => _memberRepository.CreateMember(member2));
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

