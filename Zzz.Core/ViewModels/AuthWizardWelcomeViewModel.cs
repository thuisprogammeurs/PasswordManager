using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;

namespace Zzz.Core.ViewModels
{
    public class AuthWizardWelcomeViewModel : BaseViewModel<AuthSetting, AuthSetting>, IAuthWizardViewModel
    {
        public AuthWizardWelcomeViewModel(IMvxMessenger messenger, IMvxNavigationService navigation) : base(messenger, navigation)
        {
            SelectNextCommand = new MvxCommand(SelectNext);
        }

        public IMvxCommand SelectNextCommand { get; private set; }

        private async void SelectNext()
        {
            AuthSetting authSetting = new AuthSetting()
            {
                IsNext = true
            };

            await Close(authSetting);
        }
    }
}
