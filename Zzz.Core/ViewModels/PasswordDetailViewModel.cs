using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;
using Zzz.Core.Extensions;
using Zzz.Core.Messages;
using MvvmCross.Core.Navigation;

namespace Zzz.Core.ViewModels
{
    public class PasswordDetailViewModel : BaseViewModel, IPasswordDetailViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
        private readonly IMvxNavigationService _navigationService;
        private Password _selectedPassword;
        private ObservableCollection<Group> _allGroups;
        private string _passwordId;

        public Password SelectedPassword
        {
            get { return _selectedPassword; }
            set
            {
                _selectedPassword = value;

                RaisePropertyChanged(() => SelectedPassword);
            }
        }

        public Group SelectedGroup
        {
            get
            {
                return SelectedPassword.PasswordGroup;
            }
            set
            {
                SelectedPassword.PasswordGroup = value;
                RaisePropertyChanged(() => SelectedGroup);
            }
        }

        public ObservableCollection<Group> AllGroups
        {
            get { return _allGroups; }
            set
            {
                _allGroups = value;
                RaisePropertyChanged(() => AllGroups);
            }
        }

        public PasswordDetailViewModel(IMvxMessenger messenger, IPasswordDataService passwordDataService, IMvxNavigationService navigation) : base(messenger)
        {
            _passwordDataService = passwordDataService;
            _navigationService = navigation;

            //InitializeMessenger();
        }

        //private void InitializeMessenger()
        //{
        //    Messenger.Subscribe<PasswordCopiedMessage>(OnPasswordCopiedMessage);
        //}

        //private void OnPasswordCopiedMessage(PasswordCopiedMessage message)
        //{
        //    SelectedPassword.Description = message.Password;
        //    RaisePropertyChanged(() => SelectedPassword);
        //}

        public void Init(string passwordId = "")
        {
            _passwordId = passwordId;

            if (_passwordId == string.Empty)
            {
                SelectedPassword = new Password();
                SelectedGroup = new Group();
            }
        }

        public override async void Start()
        {
            base.Start();
            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            if (_passwordId != string.Empty)
            {
                SelectedPassword = await _passwordDataService.GetPasswordById(_passwordId);
            }

            await LoadGroups();

            if (SelectedPassword != null)
            {
                SelectedGroup = await _passwordDataService.GetGroupById(SelectedPassword.PasswordGroup.Id);
            }
        }

        internal async Task LoadGroups()
        {
            AllGroups = (await _passwordDataService.GetAllGroups()).ToObservableCollection();
        }

        public IMvxCommand SaveCommand
        {
            get
            {
                return new MvxCommand(SavePassword);
            }
        }

        public IMvxCommand CancelCommand
        {
            get
            {
                return new MvxCommand(() => Close(this));
                //return new MvxCommand(() => ShowViewModel<PasswordOverviewViewModel>());
            }
        }

        public IMvxCommand DeleteCommand
        {
            get
            {
                return new MvxCommand(DeletePassword);
            }
        }

        public IMvxCommand GeneratePasswordCommand
        {
            get
            {
                return new MvxCommand(GeneratePassword);
            }
        }

        private async void SavePassword()
        {
            Password password = await _passwordDataService.SavePassword(SelectedPassword);
            ShowViewModel<PasswordOverviewViewModel>(new { reloadData = true });
        }

        private async void DeletePassword()
        {
            Password password = await _passwordDataService.DeletePassword(SelectedPassword);
            ShowViewModel<PasswordOverviewViewModel>(new { reloadData = true });
        }

        private async void GeneratePassword()
        {
            var result = await _navigationService.Navigate<PasswordGeneratorViewModel, Password, PasswordGenerator>(SelectedPassword);

            if (result != null)
            {
                SelectedPassword.Description = result.GeneratedPassword;
                RaisePropertyChanged(() => SelectedPassword);
            }
        }
    }
}
