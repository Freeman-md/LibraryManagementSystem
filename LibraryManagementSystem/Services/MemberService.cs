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
            return _memberRepository.GetAllMembers();
        }

		public Member RegisterMember(Member member)
		{
			ValidateMember(member);

            return _memberRepository.CreateMember(member);
		}

        public Member? GetMember(Guid id)
        {
            return _memberRepository.GetMember(id);
        }

        public Member? GetMember(string email)
        {
            return _memberRepository.GetMember(email);
        }

        public Member UpdateMember(Member member, Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMember(Guid id)
        {
            _memberRepository.DeleteMember(id);
        }

        private static void ValidateMember(Member member) {
            if (member == null) {
                throw new ArgumentNullException(nameof(member));
            }

            if (string.IsNullOrWhiteSpace(member.Name))
            {
                throw new ArgumentException("Member name cannot be empty.", nameof(member.Name));
            }

            if (string.IsNullOrWhiteSpace(member.Email))
            {
                throw new ArgumentException("Member email cannot be empty.", nameof(member.Email));
            }
        }
    }
}

