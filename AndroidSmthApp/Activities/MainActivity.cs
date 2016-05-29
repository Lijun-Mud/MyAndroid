using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;

using AndroidSmthApp.Fragments;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;

namespace AndroidSmthApp.Activities
{
    [Activity(Label = "Home" ,MainLauncher =true, LaunchMode = LaunchMode.SingleTop, Icon = "@drawable/Icon")]
    public class MainActivity : BaseActivity
    {

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        protected override int LayoutResource
        {
            get
            {
                return Resource.Layout.main;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            drawerLayout = this.FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

            var title=this.FindViewById<TextView>(Resource.Id.nav_view_title);

            //Set hamburger items menu
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            //SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            //SupportActionBar.SetDisplayShowHomeEnabled(false);
            //SupportActionBar.SetDisplayShowTitleEnabled(true);
            ////SupportActionBar.SetTitle("My Profile");
            //SupportActionBar.SetDisplayUseLogoEnabled(false);
            //SupportActionBar.SetIcon(null);
            //SupportActionBar.SetLogo(null);

            //setup navigation view
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);

            //handle navigation
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);
                ListItemClicked(e.MenuItem.ItemId);

                //switch (e.MenuItem.ItemId)
                //{
                //    case Resource.Id.nav_home_1:
                //        ListItemClicked(0);
                //        break;
                //    case Resource.Id.nav_home_2:
                //        ListItemClicked(1);
                //        break;
                //}

                Toast.MakeText(this.ApplicationContext, "You selected: " + e.MenuItem.TitleFormatted, ToastLength.Long).Show();
                drawerLayout.CloseDrawers();
            };


            //if first time you will want to go ahead and click first item.
            if (savedInstanceState == null)
            {
                ListItemClicked((int)MenuType.LoginMenu);
            }
        }

        int oldPosition = -1;
        private void ListItemClicked(int position)
        {
            //this way we don't load twice, but you might want to modify this a bit.
            if (position == oldPosition)
                return;

            oldPosition = position;

            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case Resource.Id.nav_home_1:
                    fragment = LoginFragment.NewInstance();
                    break;
                case Resource.Id.nav_home_2:
                    fragment = Fragment2.NewInstance();
                    break;
                case (int)MenuType.LoginMenu:
                    fragment = LoginFragment.NewInstance();
                    break;
                case (int)MenuType.Top10Menu:
                    fragment = Top10Fragment.NewInstance();
                    break;
            }

            SupportFragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }

    public enum MenuType
    {
        LoginMenu=Resource.Id.nav_login,
        Top10Menu=Resource.Id.nav_top10
    }
}

