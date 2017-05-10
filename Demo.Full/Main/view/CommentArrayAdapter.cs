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
using Android.Util;


using Com.Ramotion.Expandingcollection;
using Demo.Full.Main.pojo;

namespace Demo.Full.Main.view
{
    public class CommentArrayAdapter : ECCardContentListItemAdapter
    {
        private Context context;
        private List<Comment> myList;

        public CommentArrayAdapter(Context context,List<Comment> myList) : base(context, Resource.Layout.list_element, myList)
        {

        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder viewHolder;
            View rowView = convertView;

            if (rowView == null)
            {
                LayoutInflater inflater = LayoutInflater.From(parent.Context);

                rowView = inflater.Inflate(Resource.Layout.list_element, null);
                // configure view holder
                viewHolder = new ViewHolder();
                viewHolder.date = (TextView)rowView.FindViewById(Resource.Id.firstLineDate);
                viewHolder.line1 = (TextView)rowView.FindViewById(Resource.Id.firstLine);
                viewHolder.line2 = (TextView)rowView.FindViewById(Resource.Id.secondLine);
                viewHolder.icon = (ImageView)rowView.FindViewById(Resource.Id.icon);
                rowView.Tag=viewHolder;
            }
            else
            {
                viewHolder = (ViewHolder)rowView.Tag;
            }

            Comment objectItem = (Comment)GetItem(position);
            if (objectItem != null)
            {
                viewHolder.line1.Text = objectItem.commentPersonName;
                viewHolder.line2.Text = objectItem.commentText;
                viewHolder.date.Text = objectItem.commentDate;
                viewHolder.icon.SetImageResource(objectItem.commentPersonPictureRes);
            }

            return rowView;
        }

        class ViewHolder : Java.Lang.Object
        {
            public TextView date { get; set; }
            public TextView line1 { get; set; }
            public TextView line2 { get; set; }
            public ImageView icon { get; set; }
        }
    }

}