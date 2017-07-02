using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using Zzz.Core.ViewModels;
using Zzz.Droid.Activities;
using Zzz.Droid.Extensions;

namespace Zzz.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.GroupDetailFragment")]
    public class GroupDetailFragment : MvxFragment<GroupDetailViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View groupDetailView = this.BindingInflate(Resource.Layout.GroupDetailView, null);

            return groupDetailView;
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