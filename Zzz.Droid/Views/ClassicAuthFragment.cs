using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using Zzz.Core.ViewModels;
using MvvmCross.Droid.Support.V4;

namespace Zzz.Droid.Views
{
    [MvxFragment(typeof(AuthWizardViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.ClassicAuthFragment")]
    public class ClassicAuthFragment : MvxFragment<ClassicAuthViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            return this.BindingInflate(Resource.Layout.ClassicAuthView, null);
        }
    }
}