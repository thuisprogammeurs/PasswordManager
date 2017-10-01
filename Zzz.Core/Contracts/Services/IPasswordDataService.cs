using System.Collections.Generic;
using System.Threading.Tasks;
using Zzz.Core.Models;

namespace Zzz.Core.Contracts.Services
{
    public interface IPasswordDataService
    {
        Task<List<Password>> GetAllPasswords();

        Task<Password> GetPasswordById(string passwordId);

        Task<List<Password>> GetAllPasswordsByGroupId(string groupId);

        Task<Password> SavePassword(Password password);

        Task<Password> DeletePassword(Password password);

        Task<List<Group>> GetAllGroups();

        Task<Group> GetGroupById(string groupId);

        Task<Group> GetGroupByName(string groupName);

        Task<Group> SaveGroup(Group group);

        Task<Group> DeleteGroup(Group group);
    }
}
