using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using Plugin.Fingerprint.Abstractions;
using System.Threading.Tasks;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;

namespace Zzz.Core.ViewModels
{
    public class FingerPrintQuestionViewModel : BaseViewModel<AuthSetting, AuthSetting>, IAuthWizardViewModel
    {
        private AuthSetting _authSetting;

        public FingerPrintQuestionViewModel(IMvxMessenger messenger, IMvxNavigationService navigation) : base(messenger, navigation)
        {
            SelectYesCommand = new MvxCommand(SelectYes);
            SelectNoCommand = new MvxCommand(SelectNo);
        }

        public IMvxCommand SelectYesCommand { get; private set; }
        public IMvxCommand SelectNoCommand { get; private set; }

        private async void SelectYes()
        {
            var fpService = Mvx.Resolve<IFingerprint>(); // or use dependency injection and inject IFingerprint

            var dialogConfig = new AuthenticationRequestConfiguration("Prove you have Zzz fingers!")
            { // all optional
                CancelTitle = "Cancel",
                FallbackTitle = null,
                AllowAlternativeAuthentication = false
            };

            var result = await fpService.AuthenticateAsync(dialogConfig);
            if (result.Authenticated)
            {
                if (_authSetting == null)
                {
                    _authSetting = new AuthSetting()
                    {
                        IsOk = true,
                        CurrentWizardStep = WizardSteps.UseFingerPrint
                    };
                }
            }
            else
            {
                if (_authSetting == null)
                {
                    _authSetting = new AuthSetting()
                    {
                        IsOk = false,
                        CurrentWizardStep = WizardSteps.UseFingerPrint
                    };
                }
            }

            await Close(_authSetting);
        }

        private async void SelectNo()
        {
            if (_authSetting == null)
            {
                _authSetting = new AuthSetting()
                {
                    IsOk = false,
                    CurrentWizardStep = WizardSteps.UseFingerPrint
                };
            }

            await Close(_authSetting);
        }
    }
}
