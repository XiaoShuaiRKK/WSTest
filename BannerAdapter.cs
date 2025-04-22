using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.ViewPager.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Session2SApp.Droid
{
    internal class BannerAdapter : PagerAdapter
    {

        Context context;
        int[] imageIds;

        public BannerAdapter(Context context, int[] imageIds)
        {
            this.context = context;
            this.imageIds = imageIds;
        }

        //Fill in cound here, currently 0
        public override int Count => imageIds.Length;

        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            container.RemoveView((View)@object);
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            var imageView = new ImageView(context);
            imageView.SetImageResource(imageIds[position]);
            imageView.SetScaleType(ImageView.ScaleType.CenterCrop);
            container.AddView(imageView);
            return imageView;
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object obj)
        {
            return view == obj;
        }
    }

}