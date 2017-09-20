using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace Zzz.Droid
{
    [Activity(
        MainLauncher = true,
        Label = "@string/ApplicationName",
        Theme = "@style/AppTheme.Splash", NoHistory = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}