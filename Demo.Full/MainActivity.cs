using Android.App;
using Android.Widget;
using Android.OS;
using Com.Ramotion.Expandingcollection;
using Demo.Full.Controls;
using Demo.Full.Main;
using Android.Views;
using Android.Widget;
using System;
using Android.Content;
using System.Collections.Generic;
using Demo.Full.Main.pojo;
using Demo.Full.Main.view;
using Android.Graphics;
using Android.Graphics.Drawables;

namespace Demo.Full
{
    [Activity(Label = "Demo.Full", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ECPagerView ecPagerView;
        private ItemsCountView ItemCountView;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            InitInterface();
        }

        public void InitInterface()
        {
            ExampleDataset exampledata = new ExampleDataset();
            IList<IECCardData> dataset = exampledata.GetDataset();
            var adapter = new DemoECPagerViewAdapter(this, dataset);


            ecPagerView = (ECPagerView)FindViewById(Resource.Id.ec_pager_element);

            adapter.HeadCardClicked += delegate
            {
                ecPagerView.Toggle();
            };

            ecPagerView.SetPagerViewAdapter(adapter);

            // Add background switcher to pager view
            ecPagerView.SetBackgroundSwitcherView(FindViewById<ECBackgroundSwitcherView>(Resource.Id.ec_bg_switcher_element));


            ItemCountView = (ItemsCountView)FindViewById(Resource.Id.items_count_view);
            ItemCountView.Update(0, 0, 5);
            ecPagerView.CardSelected += EcPagerView_CardSelected;
        }

        private void EcPagerView_CardSelected(object sender, ECPagerView.CardSelectedEventArgs e)
        {
            ItemCountView.Update(e.P0, e.P1, e.P2);
        }

        public override void OnBackPressed()
        {
            if (!ecPagerView.Collapse())
                base.OnBackPressed();
        }




        public class DemoECPagerViewAdapter : ECPagerViewAdapter
        {
            public event EventHandler HeadCardClicked;

            public DemoECPagerViewAdapter(Context context, IList<IECCardData> items) : base(context, items)
            {

            }

            public override void InstantiateCard(LayoutInflater p0, ViewGroup head, ListView list, IECCardData data)
            {
                // Data object for current card
                CardData cardData = (CardData)data;


                // Set adapter and items to current card content list
                list.Adapter = new CommentArrayAdapter(head.Context, cardData.ListItems as List<Comment>);
                // Also some visual tuning can be done here
                //list.Divider = (Drawable) Resource.Drawable.list_divider;
                list.SetBackgroundColor(Color.White);
                list.SetSelection(Color.Transparent);
                list.CacheColorHint = Color.Transparent;

                //View gradient = new View(head.Context);
                //gradient.LayoutParameters = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.MatchParent, AbsListView.LayoutParams.MatchParent);
                //gradient.SetBackgroundDrawable((Drawable)Resource.Drawable.card_head_gradient);
                //head.AddView(gradient);

                p0.Inflate(Resource.Layout.simple_head, head);
                
                TextView title = (TextView)head.FindViewById(Resource.Id.title);
                title.Text = cardData.HeadTitle;
                ImageView avatar = (ImageView)head.FindViewById(Resource.Id.avatar);
                avatar.SetImageResource((int)cardData.PersonPictureResource);
                TextView name = (TextView)head.FindViewById(Resource.Id.name);
                name.Text = cardData.PersonName;
                TextView message = (TextView)head.FindViewById(Resource.Id.message);
                message.Text = cardData.PersonMessage;
                TextView viewcount = (TextView)head.FindViewById(Resource.Id.socialViewsCount);
                viewcount.Text = cardData.PersonViewsCount+"";
                TextView likecount = (TextView)head.FindViewById(Resource.Id.socialLikesCount);
                likecount.Text = cardData.PersonLikesCount+"";
                TextView commentcount = (TextView)head.FindViewById(Resource.Id.socialCommentsCount);
                commentcount.Text = cardData.PersonLikesCount+"";

                //FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WrapContent, FrameLayout.LayoutParams.WrapContent);
                //layoutParams.Gravity = GravityFlags.Center;
                ////head.AddView(title, layoutParams);

                head.Click -= Head_Click;
                head.Click += Head_Click;
            }

            private void Head_Click(object sender, EventArgs e)
            {
                HeadCardClicked?.Invoke(this, EventArgs.Empty);
            }
        }
    }

}

