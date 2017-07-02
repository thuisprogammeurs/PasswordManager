using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zzz.Core.Contracts.Repositories;
using Zzz.Core.Models;
using Zzz.Core.Models.Orm;
using Realms;
using ExpressMapper;
using ExpressMapper.Extensions;

namespace Zzz.Core.Repositories
{
    public class PasswordRepository : BaseRepository, IPasswordRepository
    {
        DatabaseHelper dbHelper;

        public PasswordRepository()
        {
            dbHelper = new DatabaseHelper();

            // TO BE REMOVED.
            dbHelper.GenerateFakeData();
        }

        public async Task<List<Group>> GetAllGroups()
        {
            List<GroupOrm> allGroupOrms = dbHelper.GetAllGroups();
            List<Group> result = allGroupOrms.Map<List<GroupOrm>, List<Group>>();

            return await Task.FromResult(result);
        }

        public async Task<List<Password>> GetAllPasswords()
        {
            List<PasswordOrm> allPasswordOrms = dbHelper.GetAllPasswords();
            List<Password> result = allPasswordOrms.Map<List<PasswordOrm>, List<Password>>();

            return await Task.FromResult(result);
        }

        public async Task<Group> GetGroupById(string groupId)
        {
            GroupOrm groupOrm = dbHelper.GetGroup(groupId);
            Group result = groupOrm.Map<GroupOrm, Group>();

            return await Task.FromResult(result);
        }

        public async Task<Group> GetGroupByName(string groupName)
        {
            GroupOrm groupOrm = dbHelper.GetGroupByName(groupName);
            Group result = groupOrm.Map<GroupOrm, Group>();

            return await Task.FromResult(result);
        }

        public async Task<Password> GetPasswordById(string passwordId)
        {
            PasswordOrm passwordOrm = dbHelper.GetPassword(passwordId);
            Password result = passwordOrm.Map<PasswordOrm, Password>();

            return await Task.FromResult(result);
        }

        public async Task<Password> SavePassword(Password password)
        {
            PasswordOrm passwordOrm = password.Map<Password, PasswordOrm>();
            dbHelper.UpdatePassword(passwordOrm);

            return await Task.FromResult(password);
        }

        public async Task<Group> SaveGroup(Group group)
        {
            GroupOrm groupOrm = group.Map<Group, GroupOrm>();
            dbHelper.UpdateGroup(groupOrm);

            return await Task.FromResult(group);
        }
    }
}
