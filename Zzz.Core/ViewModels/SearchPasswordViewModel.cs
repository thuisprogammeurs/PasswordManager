using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Extensions;
using Zzz.Core.Models;

namespace Zzz.Core.ViewModels
{
    public class SearchPasswordViewModel : BaseViewModel, ISearchPasswordViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
        private readonly IConnectionService _connectionService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<Password> _allPasswords;

        public ObservableCollection<Password> AllPasswords
        {
            get { return _allPasswords; }
            set
            {
                _allPasswords = value;
                RaisePropertyChanged(() => AllPasswords);
            }
        }

        public SearchPasswordViewModel(
            IMvxMessenger messenger
            , IPasswordDataService passwordDataService
            , IConnectionService connectionService
            , IDialogService dialogService) : base(messenger)
        {
            _passwordDataService = passwordDataService;
            _connectionService = connectionService;
            _dialogService = dialogService;
        }

        public override async void Start()
        {
            base.Start();

            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            if (_connectionService.CheckOnline())
            {
                await LoadPasswords();
            }
            else
            {
                await _dialogService.ShowAlertAsync("No internet available", "Zzz...", "OK");
            }
        }

        internal async Task LoadPasswords()
        {
            AllPasswords = (await _passwordDataService.GetAllPasswords()).ToObservableCollection();
        }
    }
}
