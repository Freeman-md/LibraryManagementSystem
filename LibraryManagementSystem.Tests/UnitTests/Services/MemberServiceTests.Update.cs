using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Services;

public partial class MemberServiceTests
{
[Fact]
		public void UpdateMember_ShouldUpdateMemberSuccessfully()
		{
            var randomEmail = $"user{Guid.NewGuid()}@example.com";
            Member member = CreateMember(email: randomEmail);
			_memberService.RegisterMember(member);
            Member updatedMember = CreateMember(name: "James Bond", email: "jamesbond@gmail.com");

			_memberService.UpdateMember(updatedMember, member.Id);
            Member? result = _memberService.GetMember(member.Id);

			Assert.NotNull(result);
			Assert.Equal(updatedMember.Name, result.Name);
            Assert.Equal(updatedMember.Name, result.Name);
        }

		[Fact]
        public void UpdateMember_WithNullItem_ShouldThrowArgumentNullException()
        {
            var randomEmail = $"user{Guid.NewGuid()}@example.com";
            Member member = CreateMember(email: randomEmail);
            _memberService.RegisterMember(member);
            Member? updatedMember = null;

			Assert.Throws<ArgumentNullException>(() => _memberService.UpdateMember(updatedMember!, member.Id));
        }

        [Fact]
        public void UpdateMember_WhenMemberDoesNotExist_ShouldThrowArgumentException()
        {
            Guid id = Guid.NewGuid();
            Member updatedMember = CreateMember();

            Assert.Throws<ArgumentException>(() => _memberService.UpdateMember(updatedMember, id));
        }
}
