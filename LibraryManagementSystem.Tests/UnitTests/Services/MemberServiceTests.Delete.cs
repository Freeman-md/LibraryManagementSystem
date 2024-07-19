using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
    public partial class MemberServiceTests
    {

        [Fact]
        public void DeleteMember_ShouldRemoveMemberSuccessfully()
        {
            var randomEmail = $"user{Guid.NewGuid()}@example.com";
            Member memberToDelete = CreateMember(email: randomEmail);
            _memberService.RegisterMember(memberToDelete);

            _memberService.DeleteMember(memberToDelete.Id);

            Member? nullMember = _memberService.GetMember(memberToDelete.Id);

            Assert.Null(nullMember);
        }

        [Fact]
        public void DeleteMember_WhenMemberDoesNotExist_ShouldThrowArgumentException()
        {
            Member notSavedMember = CreateMember();

            Assert.Throws<ArgumentException>(() => _memberService.DeleteMember(notSavedMember.Id));
        }

    }


}