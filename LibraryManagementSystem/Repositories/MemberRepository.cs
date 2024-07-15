using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
	public class MemberRepository : BaseRepository
	{
        public MemberRepository(IFileContext<Book> fileContext, string filePath = "members.json") : base(fileContext, filePath) { }

        public List<Member> GetAllMembers()
        {
            throw new NotImplementedException();
        }

        public Member GetMemberById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Member CreateMember(Member member)
        {
            throw new NotImplementedException();
        }

        public Member UpdateMember(Member member, Guid id)
        {
            throw new NotImplementedException();
        }

        public Member DeleteMember(Guid id) {
            throw new NotImplementedException();
        }

        public void SaveMembers(List<Member> members)
        {
            throw new NotImplementedException();
        }
    }

}

