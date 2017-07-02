using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Extensions;
using Zzz.Core.Models;
using Zzz.Core.Services.General;
using Zzz.Core.Services.Data;
using Zzz.Core.Repositories;

namespace Zzz.Core.ViewModels
{
    public class PasswordOverviewViewModel : BaseViewModel, IPasswordOverviewViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
        private readonly IConnectionService _connectionService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<Password> _allPasswords;
        private bool _reloadData;

        public ObservableCollection<Password> AllPasswords
        {
            get { return _allPasswords; }
            set
            {
                _allPasswords = value;
                RaisePropertyChanged(() => AllPasswords);
            }
        }

        //public PasswordOverviewViewModel(IMvxMessenger messenger
        //    , IPasswordDataService passwordDataService
        //    , IConnectionService connectionService
        //    , IDialogService dialogService) : base(messenger)
        //{
        //    _passwordDataService = passwordDataService;
        //    _connectionService = connectionService;
        //    _dialogService = dialogService;
        //}

        public PasswordOverviewViewModel(IMvxMessenger messenger
            , IPasswordDataService passwordDataService
            , IConnectionService connectionService) : base(messenger)
        {
            //_passwordDataService = new PasswordDataService(new PasswordRepository());
            //_connectionService = new ConnectionService();
            _passwordDataService = passwordDataService;
            _connectionService = connectionService;
        }

        public void Init(bool reloadData = false)
        {
            _reloadData = reloadData;
        }

        public override async void Start()
        {
            base.Start();

            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            await LoadPasswords();

            //if (_connectionService.CheckOnline())
            //{
            //    await LoadPasswords();
            //}
            //else
            //{
            //    await _dialogService.ShowAlertAsync("No internet available", "Zzz...", "OK");
            //}
        }

        internal async Task LoadPasswords()
        {
            AllPasswords = (await _passwordDataService.GetAllPasswords()).ToObservableCollection();
        }

        public IMvxCommand ShowPasswordDetailsCommand
        {
            get
            {
                return new MvxCommand<Password>(selectedPassword =>
                {
                    ShowViewModel<PasswordDetailViewModel>
                    (new { passwordId = selectedPassword.Id });
                });
            }
        }
    }
}
