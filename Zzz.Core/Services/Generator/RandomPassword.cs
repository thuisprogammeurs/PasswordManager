using System;

namespace Zzz.Core.Services.Generator
{
    public class RandomPassword
    {
        public int PasswordLength { get; set; }

        public bool IsIncludeCharacter { get; set; }

        public bool IsIncludeNumber { get; set; }

        public bool IsIncludeSpecialCharacter { get; set; }

        public RandomPassword()
        {
            PasswordLength = 20;
            IsIncludeCharacter = true;
            IsIncludeNumber = true;
            IsIncludeSpecialCharacter = true;
        }

        public RandomPassword(int passwordLength, bool isIncludeCharacter, bool isIncludeNumber, bool isIncludeSpecialCharacter)
        {
            PasswordLength = passwordLength;
            IsIncludeCharacter = isIncludeCharacter;
            IsIncludeNumber = isIncludeNumber;
            IsIncludeSpecialCharacter = isIncludeSpecialCharacter;
        }

        public string GenerateRandomPassword()
        {
            return GenerateRandomPassword(PasswordLength);
        }

        public string GenerateRandomPassword(int passwordLength)
        {
            const string cCharacters = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            const string cNumber = "0123456789";
            const string cSpecialCharacters = "!@$?_-%#";

            string allowedChars = string.Empty;
            if (IsIncludeCharacter)
            {
                allowedChars += cCharacters;
            }
            if (IsIncludeNumber)
            {
                allowedChars += cNumber;
            }
            if (IsIncludeSpecialCharacter)
            {
                allowedChars += cSpecialCharacters;
            }

            char[] chars = new char[PasswordLength];
            Random rd = new Random();

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
