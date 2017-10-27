namespace Zzz.Core.Models
{
    public class AuthSetting
    {
        public AuthOption MainAuthentication { get; set; }

        public AuthOption BackupAuthentication { get; set; }
    }

    public enum AuthOption
    {
        Classic = 0
        , Picture
        , FingerPrint
        , None
    }
}
