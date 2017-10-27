using Realms;

namespace Zzz.Core.Models.Orm
{
    public class AuthPictureOrm : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string IconName { get; set; }

        public string SecretCode { get; set; }

        public int SortOrder { get; set; }
    }
}
