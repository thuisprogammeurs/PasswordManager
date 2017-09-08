using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Zzz.Core.Contracts.Services;
using Zzz.Core.ViewModels;

namespace Zzz.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        /// <summary>
        /// The login service.
        /// </summary>
        private readonly ILoginService _loginService;

        public AppStart(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async void Start(object hint = null)
        {
            // If your application uses a secure API this first call attempts to log the user into the application
            // using any credentials stored from a previous session.  If there are
            // none stored we should present the login screen, else go straight into the app
            if (_loginService.Login())
            {
                ShowViewModel<MainViewModel>();
            }
            else
            {
                ShowViewModel<LoginViewModel>();
            }
        }
    }
}
