using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Threading.Tasks;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;
using Zzz.Core.Messages;
using System;
using System.Threading;
using MvvmCross.Platform;
using MvvmCross.Core.Navigation;

namespace Zzz.Core.ViewModels
{
    public class PasswordGeneratorViewModel : BaseViewModel<Password, PasswordGenerator>, IPasswordGeneratorViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
        private readonly IPasswordGeneratorService _passwordGeneratorService;
        private int _passwordLength;
        private bool _isIncludeCharacter;
        private bool _isIncludeNumber;
        private bool _isIncludeSpecialCharacter;
        private bool _isCharacterEnabled;
        private bool _isNumberEnabled;
        private bool _isSpecialCharacterEnabled;
        private PasswordGenerator _selectedPasswordGenerator;

        public PasswordGeneratorViewModel(IMvxMessenger messenger, IMvxNavigationService navigation, IPasswordDataService passwordDataService, IPasswordGeneratorService passwordGeneratorService) : base(messenger, navigation)
        {
            _passwordDataService = passwordDataService;
            _passwordGeneratorService = passwordGeneratorService;
        }

        public PasswordGenerator SelectedPasswordGenerator
        {
            get { return _selectedPasswordGenerator; }
            set
            {
                _selectedPasswordGenerator = value;
                RaisePropertyChanged(() => SelectedPasswordGenerator);
            }
        }

        public int PasswordLength
        {
            get { return _passwordLength; }
            set
            {
                _passwordLength = value;
                SelectedPasswordGenerator.PasswordLength = _passwordLength;
                RaisePropertyChanged(() => PasswordLength);

                RegeneratePassword();
            }
        }

        public bool IsIncludeCharacter
        {
            get { return _isIncludeCharacter; }
            set
            {
                _isIncludeCharacter = value;
                SelectedPasswordGenerator.IsIncludeCharacter = _isIncludeCharacter;
                RaisePropertyChanged(() => IsIncludeCharacter);

                SetCheckboxStatus();

                RegeneratePassword();
            }
        }

        public bool IsCharacterEnabled
        {
            get { return _isCharacterEnabled; }
            set
            {
                _isCharacterEnabled = value;
                RaisePropertyChanged(() => IsCharacterEnabled);
            }
        }

        public bool IsIncludeNumber
        {
            get { return _isIncludeNumber; }
            set
            {
                _isIncludeNumber = value;
                SelectedPasswordGenerator.IsIncludeNumber = _isIncludeNumber;
                RaisePropertyChanged(() => IsIncludeNumber);

                SetCheckboxStatus();

                RegeneratePassword();
            }
        }

        public bool IsNumberEnabled
        {
            get { return _isNumberEnabled; }
            set
            {
                _isNumberEnabled = value;
                RaisePropertyChanged(() => IsNumberEnabled);
            }
        }

        public bool IsIncludeSpecialCharacter
        {
            get { return _isIncludeSpecialCharacter; }
            set
            {
                _isIncludeSpecialCharacter = value;
                SelectedPasswordGenerator.IsIncludeSpecialCharacter = _isIncludeSpecialCharacter;
                RaisePropertyChanged(() => IsIncludeSpecialCharacter);

                SetCheckboxStatus();

                RegeneratePassword();
            }
        }

        public bool IsSpecialCharacterEnabled
        {
            get { return _isSpecialCharacterEnabled; }
            set
            {
                _isSpecialCharacterEnabled = value;
                RaisePropertyChanged(() => IsSpecialCharacterEnabled);
            }
        }

        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            if (_selectedPasswordGenerator == null)
            {
                _selectedPasswordGenerator = new PasswordGenerator()
                {
                    PasswordLength = 20,
                    IsIncludeCharacter = true,
                    IsIncludeNumber = true,
                    IsIncludeSpecialCharacter = true
                };
            }

            PasswordLength = SelectedPasswordGenerator.PasswordLength;
            IsIncludeCharacter = SelectedPasswordGenerator.IsIncludeCharacter;
            IsIncludeNumber = SelectedPasswordGenerator.IsIncludeNumber;
            IsIncludeSpecialCharacter = SelectedPasswordGenerator.IsIncludeSpecialCharacter;

            RegeneratePassword();
        }

        public IMvxCommand RegenerateCommand
        {
            get
            {
                return new MvxCommand(RegeneratePassword);
            }
        }

        public IMvxCommand SelectCommand
        {
            get
            {
                return new MvxCommand(SelectPassword);
            }
        }

        public IMvxCommand CancelCommand
        {
            get
            {
                return new MvxCommand(() => Close(this));
            }
        }

        private async void RegeneratePassword()
        {
            SelectedPasswordGenerator = await _passwordGeneratorService.GeneratePassword(SelectedPasswordGenerator);
        }

        private async void SelectPassword()
        {
            await Close(SelectedPasswordGenerator);
        }

        private void SetCheckboxStatus()
        {
            if ((IsIncludeCharacter == true) && (IsIncludeNumber == false) && (IsIncludeSpecialCharacter == false))
            {
                IsCharacterEnabled = false;
            }
            else
            {
                IsCharacterEnabled = true;
            }

            if ((IsIncludeNumber == true) && (IsIncludeCharacter == false) && (IsIncludeSpecialCharacter == false))
            {
                IsNumberEnabled = false;
            }
            else
            {
                IsNumberEnabled = true;
            }

            if ((IsIncludeSpecialCharacter == true) && (IsIncludeCharacter == false) && (IsIncludeNumber == false))
            {
                IsSpecialCharacterEnabled = false;
            }
            else
            {
                IsSpecialCharacterEnabled = true;
            }
        }
    }
}
