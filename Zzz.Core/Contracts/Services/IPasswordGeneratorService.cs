using System.Collections.Generic;
using System.Threading.Tasks;
using Zzz.Core.Models;

namespace Zzz.Core.Contracts.Services
{
    public interface IPasswordGeneratorService
    {
        Task<PasswordGenerator> GeneratePassword(PasswordGenerator passwordGenerator);
    }
}
