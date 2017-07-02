using Android;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Zzz.Core.Models;

namespace Zzz.Droid.Adapters
{
    public class OverviewListAdapter : BaseAdapter<TableItem>
    {
        List<TableItem> items;
        Activity context;
        public OverviewListAdapter(Activity context, List<TableItem> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override TableItem this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = context.LayoutInflater.Inflate(Resource.Layout.ListViewWithIconTemplate, null);
            //view.FindViewById<TextView>(Resource.Id.itemName).Text = item.Heading;
            //view.FindViewById<TextView>(Resource.Id.itemDescription).Text = item.SubHeading;
            //view.FindViewById<ImageView>(Resource.Id.itemIcon).SetImageResource(item.ImageResourceId);
            return view;
        }
    }
}