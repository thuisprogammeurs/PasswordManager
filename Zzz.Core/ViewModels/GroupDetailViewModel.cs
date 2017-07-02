using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;
using Zzz.Core.Services.Data;
using Zzz.Core.Repositories;
using Zzz.Core.Extensions;

namespace Zzz.Core.ViewModels
{
    public class GroupDetailViewModel : BaseViewModel, IGroupDetailViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
        private Group _selectedGroup;
        private string _groupId;

        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged(() => SelectedGroup);
            }
        }
        public GroupDetailViewModel(IMvxMessenger messenger, IPasswordDataService passwordDataService) : base(messenger)
        {
            //_passwordDataService = new PasswordDataService(new PasswordRepository());
            _passwordDataService = passwordDataService;
        }

        public void Init(string groupId)
        {
            _groupId = groupId;

            if (_groupId == string.Empty)
            {
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
            SelectedGroup = await _passwordDataService.GetGroupById(_groupId);
        }

        public IMvxCommand SaveCommand
        {
            get
            {
                return new MvxCommand(SaveGroup);
            }
        }

        public IMvxCommand CancelCommand
        {
            get
            {
                return new MvxCommand(() => ShowViewModel<GroupOverviewViewModel>());
            }
        }

        private async void SaveGroup()
        {
            Group group = await _passwordDataService.SaveGroup(SelectedGroup);
            ShowViewModel<GroupOverviewViewModel>(new { reloadData = true });
        }
    }
}
