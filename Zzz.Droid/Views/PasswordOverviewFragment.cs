using System;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using Zzz.Core.ViewModels;
using Zzz.Droid.Activities;
using Zzz.Droid.Extensions;
using MvvmCross.Droid.Support.V4;

namespace Zzz.Droid.Views
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.PasswordOverviewFragment")]
    public class PasswordOverviewFragment : BaseFragment<PasswordOverviewViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Show the hamburger menu.
            ShowHamburgerMenu = true;
            // Screen title.
            ((MainActivity)Activity).Title = "Password Overview";

            return base.OnCreateView(inflater, container, savedInstanceState);
            //return this.BindingInflate(Resource.Layout.PasswordOverviewView, null);
        }

        //public override void OnViewCreated(View view, Bundle savedInstanceState)
        //{
        //    base.OnViewCreated(view, savedInstanceState);
        //    (this.Activity as MainActivity).SetCustomTitle("Password Overview");
        //}

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
                return Resource.Layout.PasswordOverviewView;
            }
        }
    }
}