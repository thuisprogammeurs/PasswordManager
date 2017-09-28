using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using Zzz.Core.ViewModels;
using Zzz.Droid.Activities;
using Zzz.Droid.Extensions;
using Android.Widget;

namespace Zzz.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.GroupDetailFragment")]
    public class GroupDetailFragment : BaseFragment<GroupDetailViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Show the hamburger menu.
            ShowHamburgerMenu = false;
            // Show the options menu.
            HasOptionsMenu = true;
            // Screen title.
            ((MainActivity)Activity).Title = "Group Details";

            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            bool isEditMode = false;
            GroupDetailViewModel groupDetailViewModel = (GroupDetailViewModel)ViewModel;
            if (groupDetailViewModel.SelectedGroup != null)
            {
                if (groupDetailViewModel.SelectedGroup.Id != null)
                {
                    isEditMode = true;
                }
            }

            if (isEditMode)
            {
                inflater.Inflate(Resource.Menu.toolbar_menu_edit, menu);
            }
            else
            {
                inflater.Inflate(Resource.Menu.toolbar_menu_add, menu);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_save:
                    ViewModel.SaveCommand.Execute(null);
                    return true;

                case Resource.Id.menu_cancel:
                    ViewModel.CancelCommand.Execute(null);
                    return true;

                case Resource.Id.menu_delete:
                    ViewModel.DeleteCommand.Execute(null);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnResume()
        {
            ViewModel.Start();
            base.OnResume();
        }

        public override void OnStop()
        {
            base.OnStop();
        }

        protected override int FragmentId
        {
            get
            {
                return Resource.Layout.GroupDetailView;
            }
        }
    }
}