using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Tests.UnitTests.Services
{
    public partial class MemberServiceTests
    {

        [Fact]
        public void DeleteMember_ShouldRemoveMemberSuccessfully()
        {
            Member bookToDelete = CreateMember();
            _memberService.RegisterMember(bookToDelete);

            _memberService.DeleteMember(bookToDelete.Id);

            Member? nullMember = _memberService.GetMember(bookToDelete.Id);

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