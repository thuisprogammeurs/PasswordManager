using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
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
            if (_authSetting == null)
            {
                _authSetting = new AuthSetting()
                {
                    IsOk = true,
                    CurrentWizardStep = WizardSteps.UseFingerPrint
                };
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
