using System.Collections.Generic;
using System.Threading.Tasks;
using Zzz.Core.Models;

namespace Zzz.Core.Contracts.Repositories
{
    public interface IPasswordRepository
    {
        Task<List<Password>> GetAllPasswords();

        Task<Password> GetPasswordById(string passwordId);

        Task<Password> SavePassword(Password password);

        Task<Password> DeletePassword(Password password);

        Task<List<Group>> GetAllGroups();

        Task<Group> GetGroupById(string groupId);

        Task<Group> GetGroupByName(string groupName);

        Task<Group> SaveGroup(Group group);

        Task<Group> DeleteGroup(Group group);

        bool HasMasterSecret(string name);

        MasterSecret GetMasterSecret(string name);

        Task<MasterSecret> SaveMasterSecret(MasterSecret masterSecret);
    }
}
