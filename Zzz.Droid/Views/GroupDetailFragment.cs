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
    public class GroupDetailFragment : MvxFragment<GroupDetailViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            // Show the options menu.
            HasOptionsMenu = true;

            View groupDetailView = this.BindingInflate(Resource.Layout.GroupDetailView, null);

            return groupDetailView;
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

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).SetCustomTitle("Group details");
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
    }
}