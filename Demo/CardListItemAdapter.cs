using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Ramotion.Expandingcollection;

namespace Demo
{
    public class CardListItemAdapter : ECCardContentListItemAdapter
    {
        public CardListItemAdapter(Context context, List<String> objects)
            : base(context, Resource.Layout.list_item, objects)
        {
        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder viewHolder;
            View rowView = convertView;

            if (rowView == null)
            {
                LayoutInflater inflater = LayoutInflater.From(parent.Context);
                rowView = inflater.Inflate(Resource.Layout.list_item, null);
                viewHolder = new ViewHolder();
                viewHolder.ItemText = rowView.FindViewById<TextView>(Resource.Id.list_item_text);
                rowView.Tag = viewHolder;
            }
            else
            {
                viewHolder = (ViewHolder)rowView.Tag;
            }

            string item = (string) GetItem(position);
            if (item != null)
            {
                viewHolder.ItemText.Text = (item);
            }
            return rowView;
        }

        class ViewHolder : Java.Lang.Object
        {
            public TextView ItemText { get; set; }
        }
    }
}