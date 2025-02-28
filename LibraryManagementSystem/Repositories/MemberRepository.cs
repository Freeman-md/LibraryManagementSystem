﻿using System;
using LibraryManagementSystem.FileContexts;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories
{
	public class MemberRepository : BaseRepository<Member>
	{
        public MemberRepository(IFileContext<Member> fileContext, string filePath = "members.json") : base(fileContext, filePath) { }

        public List<Member> GetAllMembers()
        {
            return _fileContext.ReadFromFile(_filePath);
        }

        public Member? GetMember(Guid id)
        {
            List<Member> members = GetAllMembers();

            return members.FirstOrDefault<Member>((member) => member.Id == id);
        }

        public Member? GetMember(string email)
        {
            List<Member> members = GetAllMembers();

            return members.FirstOrDefault(member => member.Email.Equals(email, StringComparison.OrdinalIgnoreCase));;
        }

        public Member CreateMember(Member member)
        {
            List<Member> members = GetAllMembers();

            if (GetMember(member.Email) != null)
            {
                throw new InvalidOperationException($"Member with email: {member.Email} already exists.");
            }

            members.Add(member);

            SaveMembers(members);

            return member;
        }

        public Member UpdateMember(Member updatedMember, Guid memberId)
        {
            List<Member> members = GetAllMembers();
            Member? existingMember = members.FirstOrDefault(member => member.Id == memberId);

            if (existingMember == null)
            {
                throw new ArgumentException("Member does not exist.", nameof(memberId));
            }

            existingMember.Name = updatedMember.Name;
            existingMember.Email = updatedMember.Email;
            existingMember.PhoneNumber = updatedMember.PhoneNumber;
            existingMember.Address = updatedMember.Address;

            SaveMembers(members);

            return existingMember;
        }

        public void DeleteMember(Guid memberId) {
            List<Member> members = GetAllMembers();
            Member? member = members.FirstOrDefault(member => member.Id == memberId);

            if (member == null)
            {
                throw new ArgumentException("Member does not exist.", nameof(memberId));
            }

            members.Remove(member);
            SaveMembers(members);
        }

        public void SaveMembers(List<Member> members)
        {
            _fileContext.WriteToFile(_filePath, members);
        }
    }

}

