using System;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using System.Collections;
using Demo.Full.Main.pojo;
using Com.Ramotion.Expandingcollection;
using Java.Lang;
using System.Collections.Generic;

namespace Demo.Full.Main.view
{
    public class ExampleDataset : Java.Lang.Object, IECCardData
    {
        public IList<IECCardData> dataset { get; set; }
        public IList comments { get; set; }

        public ExampleDataset()
        {
            dataset = new List<IECCardData>(5);
            comments = new List<Comment>(10);
            
            var Attractions = new int[] {
                Resource.Drawable.attractions,
                Resource.Drawable.city_scape,
                Resource.Drawable.cuisine,
                Resource.Drawable.nature,
                Resource.Drawable.night_life
            };

            var AttractionsHead = new int[] {
                Resource.Drawable.attractions_head,
                Resource.Drawable.city_scape_head,
                Resource.Drawable.cuisine_head,
                Resource.Drawable.nature_head,
                Resource.Drawable.night_life_head
            };

            var HeadTitle = new string[] {
                "Attractions",
                "City Scape",
                "Cuisine",
                "Nature",
                "Night Life"
            };

            var PersonalName = new string[] {
                "Marjorie Ellis",
                "Mattew Jordan",
                "Ross Rodriguez",
                "Tina Caldwell",
                "Wallace Sutton"
            };

            var PersonalMessage = new string[] {
                "Usus de bassus buxum, desiderium index!",
                "Solems manducare, tanquam neuter verpa.",
                "Magnum lacteas ducunt ad orexis.",
                "Nunquam perdere clabulare.",
                "Cur adelphis studere?"
            };

            var PersonalImage = new int[] {
                Resource.Drawable.marjorie_ellis,
                Resource.Drawable.mattew_jordan,
                Resource.Drawable.ross_rodriguez,
                Resource.Drawable.tina_caldwell,
                Resource.Drawable.wallace_sutton
            };

            for (int i = 0; i < 10; i++)
            {
                comments.Add(new Comment
                {
                    commentPersonPictureRes = PersonalImage[i%5],
                    commentPersonName = PersonalName[i%5],
                    commentText = PersonalMessage[i%5],
                    commentDate = "may "+i+", 1965"
                });
            }

            for (int i = 0; i < 5; i++)
            {
                CardData item = new CardData
                {
                    MainBackgroundResource = (Integer)Attractions[i],
                    HeadBackgroundResource = (Integer)AttractionsHead[i],
                    HeadTitle = HeadTitle[i],
                    PersonMessage = PersonalMessage[i],
                    PersonName = PersonalName[i],
                    PersonPictureResource = (Integer)PersonalImage[i],
                    ListItems = comments,
                };

                dataset.Add(item);
            }
            
        }

        public IList<IECCardData> GetDataset()
        {
            return dataset;
        }

        Integer IECCardData.HeadBackgroundResource => throw new NotImplementedException();

        IList IECCardData.ListItems => throw new NotImplementedException();

        Integer IECCardData.MainBackgroundResource => throw new NotImplementedException();

    }
}