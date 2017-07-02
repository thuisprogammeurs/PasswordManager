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

namespace Zzz.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.PasswordDetailFragment")]
    public class PasswordDetailFragment : MvxFragment<PasswordDetailViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View passwordDetailView = this.BindingInflate(Resource.Layout.PasswordDetailView, null);

            PasswordDetailViewModel passwordDetailViewModel = (PasswordDetailViewModel)ViewModel;
            if (passwordDetailViewModel.SelectedPassword != null)
            {
                var groupSpinner = passwordDetailView.FindViewById<MvxAppCompatSpinner>(Resource.Id.drpGroup);
                int itemPosition = 0;
                string selectedGroupId = passwordDetailViewModel.SelectedGroup.Id;
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

            return passwordDetailView;

        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            (this.Activity as MainActivity).SetCustomTitle("Password details");
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