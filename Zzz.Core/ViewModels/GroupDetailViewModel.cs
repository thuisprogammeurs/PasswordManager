using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Core.ViewModels;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Models;
using Zzz.Core.Extensions;
using MvvmValidation;
using System.Collections.Generic;

namespace Zzz.Core.ViewModels
{
    public class GroupDetailViewModel : BaseViewModel, IGroupDetailViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
        private readonly IDialogService _dialogService;
        private Group _selectedGroup;
        private string _groupId;
        private ObservableDictionary<string, string> _validationErrors;

        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged(() => SelectedGroup);
            }
        }

        public ObservableDictionary<string, string> ValidationErrors
        {
            get { return _validationErrors;  }
            set {
                _validationErrors = value;
                RaisePropertyChanged(() => ValidationErrors);
            }
        }

        public GroupDetailViewModel(IMvxMessenger messenger, IPasswordDataService passwordDataService, IDialogService dialogService) : base(messenger)
        {
            _passwordDataService = passwordDataService;
            _dialogService = dialogService;
        }

        public void Init(string groupId = "")
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
                return new MvxCommand(() => Close(this));
            }
        }

        public IMvxCommand DeleteCommand
        {
            get
            {
                return new MvxCommand(DeleteGroup);
            }
        }

        private async void SaveGroup()
        {
            if (!IsValid())
            {
                return;
            }

            Group group = await _passwordDataService.SaveGroup(SelectedGroup);
            ShowViewModel<GroupOverviewViewModel>(new { reloadData = true });
        }

        private async void DeleteGroup()
        {
            if (CanDelete())
            {
                Group group = await _passwordDataService.DeleteGroup(SelectedGroup);
                ShowViewModel<GroupOverviewViewModel>(new { reloadData = true });
            }
            else
            {
                await _dialogService.ShowAlertAsync("This password group is in use. Please remove the passwords linked to this password group first.", "Cannot be deleted", "OK");
            }
        }

        private bool CanDelete()
        {
            // Waiting for result from an async function.
            List<Password> passwords = _passwordDataService.GetAllPasswordsByGroupId(SelectedGroup.Id).Result;

            bool result = true;
            if (passwords.Count > 0)
            {
                result = false;
            }

            return result;
        }

        private bool IsValid()
        {
            var validator = new ValidationHelper();
            validator.AddRequiredRule(() => SelectedGroup.Name, "Name is required.");
            validator.AddRequiredRule(() => SelectedGroup.Description, "Description is required.");

            var result = validator.ValidateAll();

            ValidationErrors = result.AsObservableDictionary();

            return result.IsValid;
        }
    }
}
