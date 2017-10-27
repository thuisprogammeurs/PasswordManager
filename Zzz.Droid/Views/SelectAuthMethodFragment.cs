using System;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platform.WeakSubscription;
using MvvmCross.Droid.Support.V4;
using Zzz.Core.ViewModels;

namespace Zzz.Droid.Views
{
    [MvxFragment(typeof(AuthWizardViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.SelectAuthMethodFragment")]
    public class SelectAuthMethodFragment : BaseFragment<SelectAuthMethodViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return base.OnCreateView(inflater, container, savedInstanceState);
        }

        protected override int FragmentId
        {
            get
            {
                return Resource.Layout.GroupOverviewView;
            }
        }
    }
}