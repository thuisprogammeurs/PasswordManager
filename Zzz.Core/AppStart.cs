using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Zzz.Core.Contracts.Services;
using Zzz.Core.ViewModels;

namespace Zzz.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        public async void Start(object hint = null)
        {
            ShowViewModel<MainViewModel>();
        }
    }
}
