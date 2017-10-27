using Android.App;
using Android.OS;
using Android.Content.PM;
using MvvmCross.Droid.Support.V7.AppCompat;
using Zzz.Core.ViewModels;

namespace Zzz.Droid.Activities
{
    [Activity(Label = "Login Activity"
        , Theme = "@style/AppTheme.Login"
        , LaunchMode = LaunchMode.SingleTop
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize
        , Name = "zzz.droid.activities.LoginActivity")]
    public class LoginActivity : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginView);
        }
    }
}