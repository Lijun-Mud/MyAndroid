using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Runtime;

namespace AndroidApp3.Fragments
{
    public class Fragment2 : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
            // Create your fragment here
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.menu_share1, menu);

            var shareItem = menu.FindItem(Resource.Id.action_share);

            //actionProvider = MenuItemCompat.GetActionProvider(shareItem).JavaCast<Android.Widget.ShareActionProvider>();
            actionProvider = MenuItemCompat.GetActionProvider(shareItem).JavaCast<Android.Support.V7.Widget.ShareActionProvider>();

            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, "Time to share some text!");

            actionProvider.SetShareIntent(intent);

            base.OnCreateOptionsMenu(menu, inflater);
        }

        Android.Support.V7.Widget.ShareActionProvider actionProvider;

        public static Fragment2 NewInstance()
        {
            var frag2 = new Fragment2 { Arguments = new Bundle() };
            return frag2;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            //this.Activity.Title = System.DateTime.Now.ToString();
            return inflater.Inflate(Resource.Layout.fragment2, null);
        }
    }
}