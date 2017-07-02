using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Zzz.Core.Contracts.ViewModels;

namespace Zzz.Core.ViewModels
{
    public class MainViewModel : MvxViewModel, IMainViewModel
    {
        private readonly Lazy<PasswordOverviewViewModel> _passwordOverviewViewModel;

        public PasswordOverviewViewModel PasswordOverviewViewModel => _passwordOverviewViewModel.Value;

        public MainViewModel()
        {
            _passwordOverviewViewModel = new Lazy<PasswordOverviewViewModel>(Mvx.IocConstruct<PasswordOverviewViewModel>);
        }

        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
        }

        public void ShowPasswordOverview()
        {
            ShowViewModel<PasswordOverviewViewModel>();
        }
    }
}
