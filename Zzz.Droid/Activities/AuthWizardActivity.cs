using Android.App;
using Android.OS;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;
using Zzz.Core.ViewModels;

namespace Zzz.Droid.Activities
{
    [Activity(Label = "Auth Wizard Activity"
        , Theme = "@style/AppTheme.Base"
        , LaunchMode = LaunchMode.SingleTop
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize
        , Name = "zzz.droid.activities.AuthWizardActivity")]
    public class AuthWizardActivity : MvxCachingFragmentCompatActivity<AuthWizardViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.AuthWizardView);
        }
    }
}