using System.Collections.Generic;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Plugin.Fingerprint.Dialog;
using Android.Widget;

namespace Zzz.Droid.Views
{
    public class FingerPrintDialog : FingerprintDialogFragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            // Set the background color.
            view.Background = new ColorDrawable(Color.Black);

            // Hide the fallback button.
            View btnFallback = view.FindViewById<Button>(Resource.Id.fingerprint_btnFallback);
            btnFallback.Visibility = ViewStates.Gone;

            return view;
        }
    }
}