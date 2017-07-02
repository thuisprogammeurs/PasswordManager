using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;
using Zzz.Core.Extensions;

namespace Zzz.Core.ViewModels
{
    public class PasswordDetailViewModel : BaseViewModel, IPasswordDetailViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
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

        public PasswordDetailViewModel(IMvxMessenger messenger, IPasswordDataService passwordDataService) : base(messenger)
        {
            //_passwordDataService = new PasswordDataService(new PasswordRepository());
            _passwordDataService = passwordDataService;
        }

        public void Init(string passwordId)
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
            SelectedPassword = await _passwordDataService.GetPasswordById(_passwordId);

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
                return new MvxCommand(() => ShowViewModel<PasswordOverviewViewModel>());
            }
        }

        private async void SavePassword()
        {
            Password password = await _passwordDataService.SavePassword(SelectedPassword);
            ShowViewModel<PasswordOverviewViewModel>(new { reloadData = true });
        }
    }
}
