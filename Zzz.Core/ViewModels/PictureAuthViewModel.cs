using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Zzz.Core.Contracts.Services;
using Zzz.Core.Contracts.ViewModels;
using Zzz.Core.Extensions;
using Zzz.Core.Models;

namespace Zzz.Core.ViewModels
{
    public class PictureAuthViewModel : BaseViewModel<AuthSetting, AuthSetting>, IPictureAuthViewModel
    {
        private readonly IPasswordDataService _passwordDataService;
        private ObservableCollection<AuthPicture> _allPictures;
        private string _picturePassword;
        private string _imageName;
        private List<AuthPicture> _selectedAuthPictures;
        private AuthSetting _authSetting;

        public PictureAuthViewModel(IMvxMessenger messenger, IMvxNavigationService navigation, IPasswordDataService passwordDataService) : base(messenger, navigation)
        {
            _passwordDataService = passwordDataService;

            // Init.
            PicturePassword = "Empty";

            ConfirmCommand = new MvxCommand(Confirm);
            PictureSelectedCommand = new MvxCommand<AuthPicture>(PictureSelected);
            ReplayCommand = new MvxCommand(Replay);
        }

        public IMvxCommand ConfirmCommand { get; private set; }

        public IMvxCommand PictureSelectedCommand { get; private set; }

        public IMvxCommand ReplayCommand { get; private set; }

        public ObservableCollection<AuthPicture> AllPictures
        {
            get { return _allPictures; }
            set
            {
                _allPictures = value;
                RaisePropertyChanged(() => AllPictures);
            }
        }

        public string PicturePassword
        {
            get { return _picturePassword; }
            set
            {
                _picturePassword = value;
                RaisePropertyChanged(() => PicturePassword);
            }
        }

        public string ImageName
        {
            get { return _imageName; }
            set
            {
                _imageName = value;
                RaisePropertyChanged(() => ImageName);
            }
        }

        public override async void Start()
        {
            base.Start();

            await ReloadDataAsync();
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            await LoadAuthPictures();
        }

        internal async Task LoadAuthPictures()
        {
            AllPictures = (await _passwordDataService.GetAllAuthPictures()).ToObservableCollection();
        }

        private async void Confirm()
        {
            if (_authSetting == null)
            {
                _authSetting = new AuthSetting()
                {
                    MainAuthentication = AuthOption.Picture,
                    IsOk = true,
                    CurrentWizardStep = WizardSteps.PictureAuth
                };
            }

            await Close(_authSetting);
        }

        private async void PictureSelected(AuthPicture authPicture)
        {
            if (_selectedAuthPictures == null) _selectedAuthPictures = new List<AuthPicture>();

            _selectedAuthPictures.Add(authPicture);
        }

        private async void Replay()
        {
            if (_selectedAuthPictures != null)
            {
                foreach(AuthPicture picture in _selectedAuthPictures)
                {
                    ImageName = picture.IconName;
                    await Task.Delay(1000);
                    ImageName = string.Empty;
                }
            }
        }
    }
}
