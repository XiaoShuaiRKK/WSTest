using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.ViewPager.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Session2SApp.Droid
{
    public class HomeFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var view = inflater.Inflate(Resource.Layout.home_fragment, container, false);
            var viewPager = view.FindViewById<ViewPager>(Resource.Id.banner_view);
            var viewBottom = view.FindViewById<LinearLayout>(Resource.Id.banner_indicator);
            int[] bannerImages =
            {
                Resource.Drawable.banner_two,
                Resource.Drawable.banner_thr,
                Resource.Drawable.banner_four
            };

            viewPager.Adapter = new BannerAdapter(Activity, bannerImages);
            // 添加小圆点
            AddBottom(viewBottom, bannerImages.Length);
            viewPager.AddOnPageChangeListener(new PageChangeListener(viewBottom));
            return view;
            
        }

        private void AddBottom(LinearLayout layout,int count)
        {
            layout.RemoveAllViews();
            for(int i = 0;i < count; i++)
            {
                var dot = new TextView(Activity);
                dot.Text = ".";
                dot.TextSize = 24;
                dot.SetTextColor(Android.Graphics.Color.ParseColor(i == 0 ? "#FF4081" : "#AAAAAA"));
                layout.AddView(dot);
            }
        }

        private class PageChangeListener : Java.Lang.Object, ViewPager.IOnPageChangeListener
        {
            private LinearLayout bottom;
            public PageChangeListener(LinearLayout bottom)
            {
                this.bottom = bottom;
            }

            void ViewPager.IOnPageChangeListener.OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
            {
                
            }

            void ViewPager.IOnPageChangeListener.OnPageScrollStateChanged(int state)
            {
                
            }

            void ViewPager.IOnPageChangeListener.OnPageSelected(int position)
            {
                for(int i = 0;i < bottom.ChildCount; i++)
                {
                    var dot = (TextView)bottom.GetChildAt(i);
                    dot.SetTextColor(Android.Graphics.Color.ParseColor(i == position ? "#FF4081" : "#AAAAAA"));
                }
            }
        }

    }
}