using System.Collections.Generic;
using System.Threading.Tasks;
using Zzz.Core.Models;

namespace Zzz.Core.Contracts.Services
{
    public interface IPasswordDataService
    {
        Task<List<Password>> GetAllPasswords();

        Task<Password> GetPasswordById(string passwordId);

        Task<List<Group>> GetAllGroups();

        Task<Group> GetGroupById(string groupId);

        Task<Group> GetGroupByName(string groupName);

        Task<Password> SavePassword(Password password);

        Task<Group> SaveGroup(Group group);
    }
}
