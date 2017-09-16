using Realms;
using System;

namespace Zzz.Core.Models.Orm
{
    public class MasterSecretOrm : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public int NumberOfAttemptsLeft { get; set; }

        public DateTimeOffset LastAttempt { get; set; }

        public int NumberOfAttemptsLeftBeforeLockout { get; set; }

        public bool IsLockedOut { get; set; }
    }
}
