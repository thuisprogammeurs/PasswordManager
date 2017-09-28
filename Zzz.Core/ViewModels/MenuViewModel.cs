using System;
using System.Collections.ObjectModel;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Messenger;
using Zzz.Core.Models.App;
using Zzz.Core.Utility;

namespace Zzz.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public MenuViewModel(IMvxMessenger messenger) : base(messenger)
        {

        }

        public IMvxCommand ShowPasswordCommand
        {
            get { return new MvxCommand(ShowPasswordExecuted); }
        }

        private void ShowPasswordExecuted()
        {
            ShowViewModel<PasswordOverviewViewModel>();
        }

        public IMvxCommand ShowPasswordGroupCommand
        {
            get { return new MvxCommand(ShowPasswordGroupExecuted); }
        }

        private void ShowPasswordGroupExecuted()
        {
            ShowViewModel<GroupOverviewViewModel>();
        }

        public IMvxCommand ShowPasswordGeneratorCommand
        {
            get { return new MvxCommand(ShowPasswordGeneratorExecuted); }
        }

        private void ShowPasswordGeneratorExecuted()
        {
            ShowViewModel<PasswordGeneratorViewModel>();
        }

        public IMvxCommand ShowSettingCommand
        {
            get { return new MvxCommand(ShowSettingExecuted); }
        }

        private void ShowSettingExecuted()
        {
            //ShowViewModel<SettingViewModel>();
        }

        public IMvxCommand ExitCommand
        {
            get { return new MvxCommand(ExitExecuted); }
        }

        private void ExitExecuted()
        {
            // Exit application.
        }
    }
}
