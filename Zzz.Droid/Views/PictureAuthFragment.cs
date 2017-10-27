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
    [MvxFragment(typeof(AuthWizardViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.PictureAuthFragment")]
    public class PictureAuthFragment : MvxFragment<PictureAuthViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //return base.OnCreateView(inflater, container, savedInstanceState);
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.PictureAuthView, null);
        }

        //protected override int FragmentId
        //{
        //    get
        //    {
        //        return Resource.Layout.PictureAuthView;
        //    }
        //}
    }
}