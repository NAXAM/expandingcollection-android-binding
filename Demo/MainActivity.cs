using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Com.Ramotion.Expandingcollection;
using System.Collections.Generic;
using Android.Views;
using System;
using Android.Content;
using System.Collections;
using Android.Graphics;

namespace Demo
{
    [Activity(Label = "Demo", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        ECPagerView ecPagerView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);// Get pager from layout
            ecPagerView = FindViewById<ECPagerView>(Resource.Id.ec_pager_element);

            // Generate example dataset
            IList<IECCardData> dataset = CardDataImpl.GenerateExampleData();

            var adapter = new DemoECPagerViewAdapter(this, dataset);
            adapter.HeadCardClicked += delegate { 
                ecPagerView.Toggle();
            };
            ecPagerView.SetPagerViewAdapter(adapter);
            
            // Add background switcher to pager view
            ecPagerView.SetBackgroundSwitcherView(FindViewById<ECBackgroundSwitcherView>(Resource.Id.ec_bg_switcher_element));
        }
        
        public override void OnBackPressed()
        {
            if (!ecPagerView.Collapse())
                base.OnBackPressed();
        }
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
            CardDataImpl cardData = (CardDataImpl)data;


            // Set adapter and items to current card content list
            list.Adapter = new CardListItemAdapter(head.Context, cardData.ListItems as List<string>);
            // Also some visual tuning can be done here
            list.SetBackgroundColor(Color.White);

            // Here we can create elements for head view or inflate layout from xml using inflater service
            TextView cardTitle = new TextView(head.Context);
            cardTitle.Text = cardData.CardTitle;
            cardTitle.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);

            FrameLayout.LayoutParams layoutParams = new FrameLayout.LayoutParams(FrameLayout.LayoutParams.WrapContent, FrameLayout.LayoutParams.WrapContent);
            layoutParams.Gravity = GravityFlags.Center;
            head.AddView(cardTitle, layoutParams);

            head.Click -= Head_Click;
            head.Click += Head_Click;
        }

        private void Head_Click(object sender, EventArgs e)
        {
            HeadCardClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}

