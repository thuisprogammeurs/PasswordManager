using System;

using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Shared.Fragments;
using MvvmCross.Droid.Support.V7.AppCompat;
using System.Collections.Generic;
using Zzz.Core.ViewModels;
using Zzz.Core.Models;
using Zzz.Droid.Adapters;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using MvvmCross.Droid.Support.V4;

namespace Zzz.Droid.Activities
{
    [Activity(Label = "Main Activity"
        , Theme = "@style/AppTheme.Base"
        , LaunchMode = LaunchMode.SingleTop
        , ScreenOrientation = ScreenOrientation.Portrait
        , Name = "zzz.droid.activities.MainActivity")]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        public DrawerLayout DrawerLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainView);

            DrawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            if (bundle == null)
                ViewModel.ShowMenu();

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DrawerLayout.OpenDrawer(GravityCompat.Start);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void ShowBackButton()
        {
            //TODO Tell the toggle to set the indicator off
            //this.DrawerToggle.DrawerIndicatorEnabled = false;

            //Block the menu slide gesture
            DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
        }

        private void ShowHamburguerMenu()
        {
            //TODO set toggle indicator as enabled 
            //this.DrawerToggle.DrawerIndicatorEnabled = true;

            //Unlock the menu sliding gesture
            DrawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
        }

        public override void OnBackPressed()
        {
            if (DrawerLayout != null && DrawerLayout.IsDrawerOpen(GravityCompat.Start))
                DrawerLayout.CloseDrawers();
            else
                base.OnBackPressed();
        }

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null) return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }
    }
}

