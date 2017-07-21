using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Threading.Tasks;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;

namespace Zzz.Core.ViewModels
{
    public class PasswordGeneratorViewModel : BaseViewModel, IPasswordGeneratorViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
        private readonly IPasswordGeneratorService _passwordGeneratorService;
        private int _passwordLength;
        private PasswordGenerator _selectedPasswordGenerator;

        public PasswordGeneratorViewModel(IMvxMessenger messenger, IPasswordDataService passwordDataService, IPasswordGeneratorService passwordGeneratorService) : base(messenger)
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
        }
    }
}
