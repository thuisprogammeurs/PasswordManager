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
using Android.Widget;

namespace Zzz.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.PasswordDetailFragment")]
    public class PasswordDetailFragment : MvxFragment<PasswordDetailViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            // Show the options menu.
            HasOptionsMenu = true;

            View passwordDetailView = this.BindingInflate(Resource.Layout.PasswordDetailView, null);

            PasswordDetailViewModel passwordDetailViewModel = (PasswordDetailViewModel)ViewModel;
            if (passwordDetailViewModel.SelectedPassword != null)
            {
                if (passwordDetailViewModel.SelectedPassword.Id != null)
                {
                    var groupSpinner = passwordDetailView.FindViewById<MvxAppCompatSpinner>(Resource.Id.drpGroup);
                    int itemPosition = 0;
                    string selectedGroupId = passwordDetailViewModel.SelectedGroup.Id;
                    if (selectedGroupId != null)
                    {
                        foreach (Group group in passwordDetailViewModel.AllGroups)
                        {
                            if (group.Id == selectedGroupId)
                            {
                                groupSpinner.SetSelection(itemPosition);
                                break;
                            }
                            itemPosition++;
                        }
                    }
                }
            }

            return passwordDetailView;

        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);

            bool isEditMode = false;
            PasswordDetailViewModel passwordDetailViewModel = (PasswordDetailViewModel)ViewModel;
            if (passwordDetailViewModel.SelectedPassword != null)
            {
                if (passwordDetailViewModel.SelectedPassword.Id != null)
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
            (this.Activity as MainActivity).SetCustomTitle("Password Details");
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