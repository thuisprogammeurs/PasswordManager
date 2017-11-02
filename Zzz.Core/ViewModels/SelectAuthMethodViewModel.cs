using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;

namespace Zzz.Core.ViewModels
{
    public class SelectAuthMethodViewModel : BaseViewModel<AuthSetting, AuthSetting>, ISelectAuthMethodViewModel
    {
        private AuthSetting _authSetting;

        public SelectAuthMethodViewModel(IMvxMessenger messenger, IMvxNavigationService navigation) : base(messenger, navigation)
        {
            SelectClassicAuthCommand = new MvxCommand(SelectClassicAuth);
            SelectPictureAuthCommand = new MvxCommand(SelectPictureAuth);
        }

        public IMvxCommand SelectClassicAuthCommand { get; private set; }

        public IMvxCommand SelectPictureAuthCommand { get; private set; }

        private async void SelectClassicAuth()
        {
            if (_authSetting == null)
            {
                _authSetting = new AuthSetting()
                {
                    MainAuthentication = AuthOption.Classic,
                    IsOk = true,
                    CurrentWizardStep = WizardSteps.SelectAuthMethod
                };
            }

            await Close(_authSetting);
        }

        private async void SelectPictureAuth()
        {
            if (_authSetting == null)
            {
                _authSetting = new AuthSetting()
                {
                    MainAuthentication = AuthOption.Picture,
                    IsOk = true,
                    CurrentWizardStep = WizardSteps.SelectAuthMethod
                };
            }

            await Close(_authSetting);
        }
    }
}
