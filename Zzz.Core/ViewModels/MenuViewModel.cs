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
        public MvxCommand<MenuItem> MenuItemSelectCommand => new MvxCommand<MenuItem>(OnMenuEntrySelect);
        public ObservableCollection<MenuItem> MenuItems { get; }

        public event EventHandler CloseMenu;

        public MenuViewModel(IMvxMessenger messenger) : base(messenger)
        {
            MenuItems = new ObservableCollection<MenuItem>();
            CreateMenuItems();
        }

        private void CreateMenuItems()
        {
            MenuItems.Add(new MenuItem
            {
                Title = "Show passwords",
                ViewModelType = typeof(PasswordOverviewViewModel),
                Option = MenuOption.PasswordOverview,
                IsSelected = true
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Show password groups",
                ViewModelType = typeof(GroupOverviewViewModel),
                Option = MenuOption.GroupOverview,
                IsSelected = false
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Show password generator",
                ViewModelType = typeof(PasswordGeneratorViewModel),
                Option = MenuOption.PasswordGenerator,
                IsSelected = false
            });
        }

        private void OnMenuEntrySelect(MenuItem item)
        {
            ShowViewModel(item.ViewModelType);
            RaiseCloseMenu();
        }

        private void RaiseCloseMenu()
        {
            CloseMenu?.Invoke(this, EventArgs.Empty);
        }
    }
}
