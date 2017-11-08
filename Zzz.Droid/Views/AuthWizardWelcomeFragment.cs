using Android.OS;
using Android.Runtime;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Binding.Droid.BindingContext;
using Zzz.Core.ViewModels;
using MvvmCross.Droid.Support.V4;
using Plugin.Fingerprint;

namespace Zzz.Droid.Views
{
    [MvxFragment(typeof(AuthWizardViewModel), Resource.Id.content_frame, true)]
    [Register("zzz.droid.views.AuthWizardWelcomeFragment")]
    public class AuthWizardWelcomeFragment : MvxFragment<AuthWizardWelcomeViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            CrossFingerprint.SetDialogFragmentType<FingerPrintDialog>();

            return this.BindingInflate(Resource.Layout.AuthWizardWelcomeView, null);
        }
    }
}