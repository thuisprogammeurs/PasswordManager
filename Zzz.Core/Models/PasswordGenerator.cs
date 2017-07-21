namespace Zzz.Core.Models
{
    public class PasswordGenerator
    {
        public int Id { get; set; }

        public int PasswordLength { get; set; }

        public bool IsIncludeCharacter { get; set; }

        public bool IsIncludeNumber { get; set; }

        public bool IsIncludeSpecialCharacter { get; set; }

        public string GeneratedPassword { get; set; }
    }
}
