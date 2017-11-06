namespace Zzz.Core.Models
{
    public class AuthSetting
    {
        public AuthOption MainAuthentication { get; set; }

        public AuthOption BackupAuthentication { get; set; }

        public WizardSteps CurrentWizardStep { get; set; }

        public bool IsOk { get; set; }
    }

    public enum AuthOption
    {
        Classic = 0
        , Picture
        , FingerPrint
        , None
    }

    public enum WizardSteps { Intro = 0, SelectAuthMethod, ClassicAuth, PictureAuth, UseFingerPrint, FingerPrintAuth };
}
