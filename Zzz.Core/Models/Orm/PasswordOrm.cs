using Realms;

namespace Zzz.Core.Models.Orm
{
    public class PasswordOrm : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string AccessAddress { get; set; }

        public string Notes { get; set; }

        public GroupOrm PasswordGroup { get; set; }
    }
}
