using System;
using System.Threading.Tasks;
using PCLCrypto;
using Zzz.Core.Contracts.Services;
using System.Text;

namespace Zzz.Core.Services.Security
{
    public class SecurityService : ISecurityService
    {
        IAsymmetricKeyAlgorithmProvider asym = WinRTCrypto.AsymmetricKeyAlgorithmProvider.OpenAlgorithm(AsymmetricAlgorithm.RsaPkcs1);
        IHashAlgorithmProvider hash = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Sha1);

        public SecurityService()
        {
        }

        public async Task<string> Decrypt(string dataToBeDecrypted, ICryptographicKey key)
        {
            var encrypted = Convert.FromBase64String(dataToBeDecrypted);
            var decrypted = WinRTCrypto.CryptographicEngine.Decrypt(key, encrypted);
            var decryptedString = Encoding.UTF8.GetString(decrypted, 0, decrypted.Length);

            return await Task.FromResult(decryptedString);
        }

        public async Task<string> Encrypt(string dataToBeEncrypted, ICryptographicKey key)
        {
            var plain = Encoding.UTF8.GetBytes(dataToBeEncrypted);
            var encrypted = WinRTCrypto.CryptographicEngine.Encrypt(key, plain);
            var encryptedString = Convert.ToBase64String(encrypted);

            return await Task.FromResult(encryptedString);
        }

        public async Task<string> GenerateKey(string data)
        {
            var key = asym.CreateKeyPair(512);
            var publicKey = key.ExportPublicKey();
            var publicKeyString = Convert.ToBase64String(publicKey);

            return await Task.FromResult(publicKeyString);
        }

        /// <summary>
        /// Hash the input string.
        /// </summary>
        /// <param name="data">String to be hashed</param>
        /// <returns>Hashed string</returns>
        public async Task<string> HashData(string data)
        {
            var plain = Encoding.UTF8.GetBytes(data);
            var hashed = hash.HashData(plain);
            var hashedString = Convert.ToBase64String(hashed);

            return await Task.FromResult(hashedString);
        }

        /// <summary>
        /// Hash the input string and compare with the hash reference.
        /// </summary>
        /// <param name="hashReference">Hash reference</param>
        /// <param name="data">String to be compared</param>
        /// <returns><see langword="true"/> if the input string matches with the hash reference, false otherwise</returns>
        public async Task<bool> IsHashEqual(string hashReference, string data)
        {
            string hashedString = await HashData(data);

            bool result = false;
            if (hashReference == hashedString)
            {
                result = true;
            }

            return await Task.FromResult(result);
        }
    }
}
