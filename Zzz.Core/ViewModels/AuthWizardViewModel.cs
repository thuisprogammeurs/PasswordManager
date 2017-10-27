using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;

namespace Zzz.Core.ViewModels
{
    public class AuthWizardViewModel : BaseViewModel, IAuthWizardViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private enum WizardSteps { Intro = 0, SelectAuthMethod, ClassicAuth, PictureAuth };
        private WizardSteps currentWizardStep;


        public AuthWizardViewModel(IMvxMessenger messenger, IMvxNavigationService navigation) : base(messenger)
        {
            _navigationService = navigation;

            // Init.
            currentWizardStep = WizardSteps.Intro;

            NextStepCommand = new MvxCommand(MoveNext);
            PreviousStepCommand = new MvxCommand(MovePrevious);
        }

        public IMvxCommand NextStepCommand { get; private set; }

        public IMvxCommand PreviousStepCommand { get; private set; }

        private async void MoveNext()
        {
            AuthSetting authSetting = new AuthSetting();
            authSetting = await _navigationService.Navigate<PictureAuthViewModel, AuthSetting, AuthSetting>(authSetting);

            if (authSetting != null)
            {
                currentWizardStep = WizardSteps.SelectAuthMethod;
            }
        }

        private async void MovePrevious()
        {
            AuthSetting authSetting = new AuthSetting();
            authSetting = await _navigationService.Navigate<SelectAuthMethodViewModel, AuthSetting, AuthSetting>(authSetting);

            if (authSetting != null)
            {
                currentWizardStep = WizardSteps.SelectAuthMethod;
            }
        }
    }
}
