using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using Zzz.Core.ViewModels;
using Zzz.Core.Models;
using Zzz.Droid.Activities;
using Zzz.Droid.Extensions;
using MvvmCross.Droid.Support.V7.AppCompat.Widget;
using Android.Content;

namespace Zzz.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.PasswordGeneratorFragment")]
    public class PasswordGeneratorFragment : BaseFragment<PasswordGeneratorViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Show the hamburger menu.
            PasswordGeneratorViewModel passwordGeneratorViewModel = (PasswordGeneratorViewModel)ViewModel;
            if (passwordGeneratorViewModel.IsLaunchedFromNavMenu)
            {
                ShowHamburgerMenu = true;
            }
            else
            {
                ShowHamburgerMenu = false;
            }
            // Show the options menu.
            HasOptionsMenu = true;
            // Screen title.
            ((MainActivity)Activity).Title = "Password Generator";

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            PasswordGeneratorViewModel passwordGeneratorViewModel = (PasswordGeneratorViewModel)ViewModel;
            if (passwordGeneratorViewModel.IsLaunchedFromNavMenu)
            {
                inflater.Inflate(Resource.Menu.toolbar_menu_generate_only, menu);
            }
            else
            {
                inflater.Inflate(Resource.Menu.toolbar_menu_generate, menu);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_refresh:
                    ViewModel.RegenerateCommand.Execute(null);
                    return true;

                case Resource.Id.menu_copy:
                    return true;

                case Resource.Id.menu_ok:
                    ViewModel.SelectCommand.Execute(null);
                    return true;

                case Resource.Id.menu_cancel:
                    ViewModel.CancelCommand.Execute(null);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected override int FragmentId
        {
            get
            {
                return Resource.Layout.PasswordGeneratorView;
            }
        }
    }
}