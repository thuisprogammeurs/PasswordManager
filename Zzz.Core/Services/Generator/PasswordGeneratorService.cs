using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zzz.Core.Contracts.Repositories;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Models;

namespace Zzz.Core.Services.Generator
{
    public class PasswordGeneratorService : IPasswordGeneratorService
    {
        public virtual async Task<PasswordGenerator> GeneratePassword(PasswordGenerator passwordGenerator)
        {
            if (passwordGenerator == null)
            {
                passwordGenerator = new PasswordGenerator();
                passwordGenerator.PasswordLength = 20;
                passwordGenerator.IsIncludeCharacter = true;
                passwordGenerator.IsIncludeNumber = true;
                passwordGenerator.IsIncludeSpecialCharacter = true;
            }

            RandomPassword randomPassword = new RandomPassword(
                passwordGenerator.PasswordLength, passwordGenerator.IsIncludeCharacter
                , passwordGenerator.IsIncludeNumber, passwordGenerator.IsIncludeSpecialCharacter);

            passwordGenerator.GeneratedPassword = randomPassword.GenerateRandomPassword();

            //PronounceablePassword pronounceablePassword = new PronounceablePassword(
            //    passwordGenerator.PasswordLength, passwordGenerator.IsIncludeCharacter
            //    , passwordGenerator.IsIncludeNumber, passwordGenerator.IsIncludeSpecialCharacter);

            //passwordGenerator.GeneratedPassword = pronounceablePassword.GeneratePronounceablePassword();

            return await Task.FromResult(passwordGenerator);
        }
    }
}
