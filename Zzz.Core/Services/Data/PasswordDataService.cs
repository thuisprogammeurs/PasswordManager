using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zzz.Core.Contracts.Repositories;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Models;

namespace Zzz.Core.Services.Data
{
    public class PasswordDataService : IPasswordDataService
    {
        private readonly IPasswordRepository _passwordRepository;
        public PasswordDataService(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public virtual async Task<List<Password>> GetAllPasswords()
        {
            return await _passwordRepository.GetAllPasswords();
        }

        public async Task<Password> GetPasswordById(string passwordId)
        {
            return await _passwordRepository.GetPasswordById(passwordId);
        }

        public virtual async Task<List<Group>> GetAllGroups()
        {
            return await _passwordRepository.GetAllGroups();
        }

        public async Task<Group> GetGroupById(string groupId)
        {
            return await _passwordRepository.GetGroupById(groupId);
        }

        public async Task<Group> GetGroupByName(string groupName)
        {
            return await _passwordRepository.GetGroupByName(groupName);
        }

        public async Task<Password> SavePassword(Password password)
        {
            return await _passwordRepository.SavePassword(password);
        }

        public async Task<Group> SaveGroup(Group group)
        {
            return await _passwordRepository.SaveGroup(group);
        }
    }
}
