using System;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Repositories
{
	public partial class MemberRepositoryTests
	{
		[Fact]
		public void GetAllMembers_ShouldReturnAllMembers()
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

			foreach(Member member in members) {
				Assert.Contains(createdMembers, m => m.Id == member.Id);
			}
		}

		[Fact]
		public void GetAllMembers_WhenNoMembersExist_ShouldReturnEmptyList()
		{
            List<Member> members = _memberRepository.GetAllMembers();

			Assert.NotNull(members);
			Assert.Empty(members);
        }

		[Fact]
		public void GetMember_ByExistingId_ShouldReturnMember()
		{
			Member member = CreateMember();
			_memberRepository.CreateMember(member);

			Member? foundMember = _memberRepository.GetMemberById(member.Id);

			Assert.NotNull(foundMember);
			Assert.Equal(foundMember.Name, member.Name);
            Assert.Equal(foundMember.Address, member.Address);
        }

		[Fact]
		public void GetMember_WhenMemberDoesNotExist_ShouldReturnNull()
		{
			Guid id = Guid.NewGuid();

            Member? nullMember = _memberRepository.GetMemberById(id);

            Assert.Null(nullMember);
        }
	}
}

