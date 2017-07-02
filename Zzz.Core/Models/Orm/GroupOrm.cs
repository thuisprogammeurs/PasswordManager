using Realms;

namespace Zzz.Core.Models.Orm
{
    public class GroupOrm : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public int IconId { get; set; }
    }
}
