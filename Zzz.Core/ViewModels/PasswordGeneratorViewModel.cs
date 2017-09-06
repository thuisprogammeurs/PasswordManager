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

                RegeneratePassword();
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

                RegeneratePassword();
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

                RegeneratePassword();
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

            _passwordLength = SelectedPasswordGenerator.PasswordLength;

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
    }
}
