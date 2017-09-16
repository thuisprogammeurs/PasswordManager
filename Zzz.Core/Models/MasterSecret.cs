using System;

namespace Zzz.Core.Models
{
    public class MasterSecret
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public int NumberOfAttemptsLeft { get; set; }

        public DateTime LastAttempt { get; set; }

        public int NumberOfAttemptsLeftBeforeLockout { get; set; }

        public bool IsLockedOut { get; set; }
    }
}
