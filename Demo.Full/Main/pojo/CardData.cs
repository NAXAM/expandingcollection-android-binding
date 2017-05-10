using System;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Ramotion.Expandingcollection;
using Java.Lang;
using System.Collections.Generic;
using System.Collections;

namespace Demo.Full.Main.pojo
{
    public class CardData : Java.Lang.Object, IECCardData
    {

        public string HeadTitle { get; set; }
        public Integer HeadBackgroundResource { get; set; }
        public Integer MainBackgroundResource { get; set; }

        public Integer PersonPictureResource { get; set; }
        public string PersonName { get; set; }
        public string PersonMessage { get; set; }
        public int PersonViewsCount { get; set; }
        public int PersonCommentsCount { get; set; }
        public int PersonLikesCount { get; set; }
        public IList ListItems { get; set; }
        
        public CardData()
        {
            Random rnd = new Random();
            this.PersonViewsCount = 50 + rnd.Next(950);
            this.PersonCommentsCount = 35 + rnd.Next(170);
            this.PersonLikesCount = 10 + rnd.Next(1000);
        }
    }
}