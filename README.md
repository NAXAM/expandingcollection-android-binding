# Xamarin Android Binding Library
Xamarin Binding Library for [Ramotion ExpandingCollection](https://github.com/Ramotion/expanding-collection-android)

```
Install-Package Naxam.Ramotion.Expandingcollection.Droid
```

## How to use

In your AXML
```xml
<com.ramotion.expandingcollection.ECBackgroundSwitcherView
      android:id="@+id/ec_bg_switcher_element"
      android:layout_width="match_parent"
      android:layout_height="match_parent" />

<com.ramotion.expandingcollection.ECPagerView xmlns:ec="http://schemas.android.com/apk/res-auto"
    android:id="@+id/ec_pager_element"
    android:layout_width="match_parent"
    android:layout_height="wrap_content"
    android:layout_centerInParent="true"
    ec:cardHeaderHeightExpanded="150dp"
    ec:cardHeight="200dp"
    ec:cardWidth="250dp" />
```

Create your Data
```C#
public class CardDataImpl : Java.Lang.Object, IECCardData
{
    public Integer HeadBackgroundResource { get; set; }

    public Integer MainBackgroundResource { get; set; }

    public IList ListItems { get; set; }

    public string CardTitle { get; set; }

    public static IList<IECCardData> GenerateExampleData()
    {
        IList<IECCardData> list = new List<IECCardData>();
        var res = new int[] {
            Resource.Drawable.attractions,
            Resource.Drawable.attractions_head,
            Resource.Drawable.city_scape,
            Resource.Drawable.city_scape_head,
            Resource.Drawable.nature,
            Resource.Drawable.nature_head
        };

        for (int i = 0; i < 3; i++)
        {
            var resIndex = i % 3;
            var title = $"Card {i + 1}";
            list.Add(new CardDataImpl {
                HeadBackgroundResource = (Integer) res[resIndex * 2],
                MainBackgroundResource = (Integer)res[resIndex * 2 + 1],
                CardTitle = title,
                ListItems = CreateItemsList(title)
            });
        }
        return list;
    }

    private static IList CreateItemsList(string cardName)
    {
        return new List<string> {
            cardName + " - Item 1",
            cardName + " - Item 2",
            cardName + " - Item 3",
            cardName + " - Item 4",
            cardName + " - Item 5",
            cardName + " - Item 6",
            cardName + " - Item 7"
        };
    }
}
```

Create your Adapter
```C#
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
```

In your Activity
```C#
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
```
