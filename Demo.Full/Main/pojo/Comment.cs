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

namespace Demo.Full.Main.pojo
{
    public class Comment : Java.Lang.Object
    {
        public int commentPersonPictureRes { get; set; }
        public string commentPersonName { get; set; }
        public string commentText { get; set; }
        public string commentDate { get; set; }
    }
}