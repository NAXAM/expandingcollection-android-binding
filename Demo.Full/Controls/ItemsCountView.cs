using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;
using Android.Animation;

namespace Demo.Full.Controls
{
    public class ItemsCountView : LinearLayout
    {
        private TextSwitcher textSwitcher;
        private TextView textView;

        public ItemsCountView(Context context) : base(context)
        {
            InitInterface(context);
        }

        public ItemsCountView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitInterface(context);
        }

        private void InitInterface(Context context)
        {
            textSwitcher = new TextSwitcher(context);
            textSwitcher.AddView(CreateViewForTextSwitcher(context));
            textSwitcher.AddView(CreateViewForTextSwitcher(context));

            AddView(textSwitcher, new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));

            textView = new TextView(context);
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.M)
            {
                textView.SetTextAppearance(Resource.Style.positionIndicator);
            }
            else
            {
                textView.SetTextAppearance(context, Resource.Style.positionIndicator);
            }

            AddView(textView, new LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
        }

        private TextView CreateViewForTextSwitcher(Context context)
        {
            TextView textView = new TextView(context);
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.M)
            {
                textView.SetTextAppearance(Resource.Style.positionIndicatorCurrent);
            }
            else
            {
                textView.SetTextAppearance(context, Resource.Style.positionIndicatorCurrent);
            }
            textView.LayoutParameters = new TextSwitcher.LayoutParams(TextSwitcher.LayoutParams.WrapContent, TextSwitcher.LayoutParams.WrapContent);
            return textView;
        }

        public void Update(int newPosition, int oldPosition, int totalElements)
        {
            textView.Text = " / " + totalElements;
            int offset = (int)(textSwitcher.Height * 0.75);
            int duration = 250;
            if (newPosition > oldPosition)
            {

                textSwitcher.Animation = (CreatePositionAnimation(-offset, 0, 0f, 1f, duration));
                textSwitcher.Animation = (CreatePositionAnimation(0, offset, 1f, 0f, duration));
            }
            else if (oldPosition > newPosition)
            {
                textSwitcher.Animation = (CreatePositionAnimation(offset, 0, 0f, 1f, duration));
                textSwitcher.Animation = (CreatePositionAnimation(0, -offset, 1f, 0f, duration));
            }
            textSwitcher.SetText(""+(newPosition + 1));
        }

        private Animation CreatePositionAnimation(int fromY, int toY, float fromAlpha, float toAlpha, int duration)
        {
            TranslateAnimation translate = new TranslateAnimation(0, 0, fromY, toY);
            translate.Duration=duration;

            AlphaAnimation alpha = new AlphaAnimation(fromAlpha, toAlpha);
            alpha.Duration=duration;

            AnimationSet set = new AnimationSet(true);

            //i can't resolve under line
            //set.SetInterpolator(new ITimeInterpolator());
            set.AddAnimation(translate);
            set.AddAnimation(alpha);
            return set;
        }
    }
}