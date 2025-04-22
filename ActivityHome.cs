using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Session2SApp.Droid
{
    [Activity(Label = "ActivityHome", MainLauncher = true)]
    public class ActivityHome : Activity
    {
        private FrameLayout contentFrame;
        private LinearLayout tabHome, tabOrder;
        private TextView tabHomeTxt, tabOrderTxt;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_home);
            //Create your application here
            contentFrame = FindViewById<FrameLayout>(Resource.Id.home_content);
            tabHome = FindViewById<LinearLayout>(Resource.Id.tab_home);
            tabHomeTxt = FindViewById<TextView>(Resource.Id.tab_home_tv);
            tabOrder = FindViewById<LinearLayout>(Resource.Id.tab_order);
            tabOrderTxt = FindViewById<TextView>(Resource.Id.tab_order_tv);
            SwitchFragment(new HomeFragment(), tabHomeTxt);

            tabHome.Click += (sender, e) =>
            {
                SwitchFragment(new HomeFragment(), tabHomeTxt);
            };
            tabOrder.Click += (sender, e) =>
            {
                SwitchFragment(new OrderFragment(), tabOrderTxt);
            };
        }

        private void SwitchFragment(Fragment fragment, TextView seletedTab)
        {
            ClearTabSelection();
            seletedTab.Selected = true;
            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.home_content, fragment)
                .Commit();
        }

        private void ClearTabSelection()
        {
            tabHomeTxt.Selected = false;
            tabOrderTxt.Selected = false;
        }
    }
}