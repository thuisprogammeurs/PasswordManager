using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;

namespace Zzz.Core.ViewModels
{
    public class SelectAuthMethodViewModel : BaseViewModel<AuthSetting, AuthSetting>, ISelectAuthMethodViewModel
    {
        public SelectAuthMethodViewModel(IMvxMessenger messenger, IMvxNavigationService navigation) : base(messenger, navigation)
        {
            SelectClassicAuthCommand = new MvxCommand(SelectClassicAuth);
            SelectPictureAuthCommand = new MvxCommand(SelectPictureAuth);
        }

        public IMvxCommand SelectClassicAuthCommand { get; private set; }

        public IMvxCommand SelectPictureAuthCommand { get; private set; }

        private async void SelectClassicAuth()
        {
            AuthSetting authSetting = new AuthSetting()
            {
                MainAuthentication = AuthOption.Classic
            };

            await Close(authSetting);
        }

        private async void SelectPictureAuth()
        {
            AuthSetting authSetting = new AuthSetting()
            {
                MainAuthentication = AuthOption.Picture
            };

            await Close(authSetting);
        }
    }
}
