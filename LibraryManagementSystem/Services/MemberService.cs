using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
	public class MemberService
	{
		private MemberRepository _memberRepository;
		
		public MemberService(MemberRepository memberRepository)
		{
			_memberRepository = memberRepository;
		}

        public List<Member> GetAllMembers()
        {
            throw new NotImplementedException();
        }

		public Member RegisterMember(Member member)
		{
			throw new NotImplementedException();
		}

        public Member GetMember(Guid id)
        {
            throw new NotImplementedException();
        }

        public Member GetMember(string email)
        {
            throw new NotImplementedException();
        }

        public Member UpdateMember(Member member, Guid id)
        {
            throw new NotImplementedException();
        }

        public Member DeleteMember(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

