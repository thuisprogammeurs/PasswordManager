using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Threading.Tasks;
using Zzz.Core.Contracts.Services;

namespace Zzz.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService _loginService;

        private readonly IDialogService _dialogService;

        const string _cFirstTimeTitle = "Please specify your master password:";
        const string _cFirstTimeActionButtonText = "Confirm";
        const string _cLoginTitle = "Please enter your master password:";
        const string _cLoginActionButtonText = "Login";

        public LoginViewModel(IMvxMessenger messenger, ILoginService loginService, IDialogService dialogService) : base(messenger)
        {
            _loginService = loginService;
            _dialogService = dialogService;

            IsLoading = false;

            InitializeText();
        }

        private string _titleText;
        public string TitleText
        {
            get
            {
                return _titleText;
            }

            set
            {
                SetProperty(ref _titleText, value);
                RaisePropertyChanged(() => TitleText);
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                SetProperty(ref _password, value);
                RaisePropertyChanged(() => Password);
            }
        }

        private string _actionButtonText;
        public string ActionButtonText
        {
            get
            {
                return _actionButtonText;
            }

            set
            {
                SetProperty(ref _actionButtonText, value);
                RaisePropertyChanged(() => ActionButtonText);
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            InitializeText();
        }

        private IMvxCommand _loginCommand;
        //public virtual IMvxCommand LoginCommand
        //{
        //    get
        //    {
        //        _loginCommand = _loginCommand ?? new MvxCommand(AttemptLogin, CanExecuteLogin);
        //        return _loginCommand;
        //    }
        //}

        public IMvxCommand LoginCommand
        {
            get
            {
                return new MvxCommand(CheckPassword);
            }
        }

        private async void CheckPassword()
        {
            if (CanExecuteLogin())
            {
                AttemptLogin();
            }
        }

        private void AttemptLogin()
        {
            if (_loginService.IsFirstTime())
            {
                _loginService.SetMasterPassword(Password);

                ShowViewModel<MainViewModel>();
            }
            else
            {
                if (_loginService.Login(Password))
                {
                    ShowViewModel<MainViewModel>();
                }
                else
                {
                    _dialogService.ShowAlertAsync("We were unable to log you in!", "Login Failed", "OK");
                    Password = string.Empty;
                }
            }
        }

        private bool CanExecuteLogin()
        {
            return (!string.IsNullOrEmpty(Password) || !string.IsNullOrWhiteSpace(Password));
        }

        private void InitializeText()
        {
            if (_loginService.IsFirstTime())
            {
                TitleText = _cFirstTimeTitle;
                ActionButtonText = _cFirstTimeActionButtonText;
            }
            else
            {
                TitleText = _cLoginTitle;
                ActionButtonText = _cLoginActionButtonText;
            }
        }
    }
}
