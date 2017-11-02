using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Extensions;
using Zzz.Core.Models;

namespace Zzz.Core.ViewModels
{
    public class ClassicAuthViewModel : BaseViewModel<AuthSetting, AuthSetting>, IClassicAuthViewModel
    {
        const string _cFirstEntryTitle = "Please set your master password:";
        const string _cFirstEntryActionButtonText = "Set";
        const string _cReenterTitle = "Reenter your master password:";
        const string _cReenterActionButtonText = "Confirm";

        private readonly ILoginService _loginService;
        private readonly IDialogService _dialogService;
        private string _firstEntryPassword;
        private bool _firstEntry;
        private AuthSetting _authSetting;

        public ClassicAuthViewModel(IMvxMessenger messenger, IMvxNavigationService navigation, ILoginService loginService, IDialogService dialogService) : base(messenger, navigation)
        {
            _loginService = loginService;
            _dialogService = dialogService;

            ConfirmPasswordCommand = new MvxCommand(ConfirmPassword);
            CancelCommand = new MvxCommand(CancelAndExit);

            // Init.
            _firstEntry = true;

            InitializeText();
        }

        public IMvxCommand ConfirmPasswordCommand { get; private set; }

        public IMvxCommand CancelCommand { get; private set; }

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

        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            InitializeText();
        }

        private void ConfirmPassword()
        {
            if (_firstEntry)
            {
                if (MeetPasswordRequirements())
                {
                    _firstEntry = false;
                    _firstEntryPassword = Password;

                    InitializeText();
                }
            }
            else
            {
                if (IsPasswordMatched())
                {
                    SaveAndExit();
                }
            }
        }

        private async void SaveAndExit()
        {
            if (_authSetting == null)
            {
                _authSetting = new AuthSetting()
                {
                    IsOk = true,
                    CurrentWizardStep = WizardSteps.ClassicAuth
                };
            }

            await Close(_authSetting);
        }

        private async void CancelAndExit()
        {
            if (_authSetting == null)
            {
                _authSetting = new AuthSetting()
                {
                    IsOk = false,
                    CurrentWizardStep = WizardSteps.ClassicAuth
                };
            }

            await Close(_authSetting);
        }

        private bool IsPasswordMatched()
        {
            bool isValid = true;

            if (_firstEntryPassword != Password)
            {
                _dialogService.ShowAlertAsync("The password doesn't match. Please reenter your master password.", "Warning", "OK");
                isValid = false;
            }

            return isValid;
        }

        private bool MeetPasswordRequirements()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(Password) || string.IsNullOrWhiteSpace(Password))
            {
                _dialogService.ShowAlertAsync("Please enter a password.", "Warning", "OK");
                isValid = false;
            }

            if (isValid)
            {
                if (Password.Length < 8)
                {
                    _dialogService.ShowAlertAsync("The minimum password length is 8 characters.", "Warning", "OK");
                    isValid = false;
                }
            }

            return isValid;
        }

        private void InitializeText()
        {
            if (_firstEntry)
            {
                TitleText = _cFirstEntryTitle;
                ActionButtonText = _cFirstEntryActionButtonText;
            }
            else
            {
                TitleText = _cReenterTitle;
                ActionButtonText = _cReenterActionButtonText;
            }
        }
    }
}
