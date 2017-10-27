using MvvmCross.Core.Navigation;
using MvvmCross.Plugins.Messenger;
using Zzz.Core.Contracts.ViewModels;

namespace Zzz.Core.ViewModels
{
    public class FingerPrintViewModel : BaseViewModel, IFingerPrintViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public FingerPrintViewModel(IMvxMessenger messenger, IMvxNavigationService navigation) : base(messenger)
        {
            _navigationService = navigation;
        }
    }
}
