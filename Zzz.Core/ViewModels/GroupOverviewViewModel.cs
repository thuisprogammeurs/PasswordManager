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
    public class GroupOverviewViewModel : BaseViewModel, IGroupOverviewViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
        private ObservableCollection<Group> _allGroups;

        public ObservableCollection<Group> AllGroups
        {
            get { return _allGroups; }
            set
            {
                _allGroups = value;
                RaisePropertyChanged(() => AllGroups);
            }
        }

        public GroupOverviewViewModel(IMvxMessenger messenger, IPasswordDataService passwordDataService) : base(messenger)
        {
            //_passwordDataService = new PasswordDataService(new PasswordRepository());
            _passwordDataService = passwordDataService;
        }

        public void Init(bool reloadData = false)
        {

        }

        public override async void Start()
        {
            base.Start();

            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            await LoadGroups();
        }

        internal async Task LoadGroups()
        {
            AllGroups = (await _passwordDataService.GetAllGroups()).ToObservableCollection();
        }

        public IMvxCommand ShowGroupDetailsCommand
        {
            get
            {
                return new MvxCommand<Group>(selectedGroup =>
                {
                    ShowViewModel<GroupDetailViewModel>
                    (new { groupId = selectedGroup.Id });
                });
            }
        }
    }
}
