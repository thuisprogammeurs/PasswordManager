using Realms;

namespace Zzz.Core.Models.Orm
{
    public class PasswordGeneratorOrm : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int PasswordLength { get; set; }

        public bool IsIncludeCharacter { get; set; }

        public bool IsIncludeNumber { get; set; }

        public bool IsIncludeSpecialCharacter { get; set; }
    }
}
