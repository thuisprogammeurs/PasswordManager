using PCLCrypto;
using System.Threading.Tasks;

namespace Zzz.Core.Contracts.Services
{
    public interface ISecurityService
    {
        Task<string> HashData(string data);

        Task<bool> IsHashEqual(string referenceHashedData, string newData);

        Task<string> Encrypt(string data, ICryptographicKey key);

        Task<string> Decrypt(string data, ICryptographicKey key);

        Task<string> GenerateKey(string data);
    }
}
