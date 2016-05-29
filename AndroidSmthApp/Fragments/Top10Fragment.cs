using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidSmthApp.Adapter;
using Newtonsoft.Json;
using AndroidSmthApp.Model;

namespace AndroidSmthApp.Fragments
{
    public class Top10Fragment : Fragment
    {
        private Button _loginButton;
        private Context _context;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static Top10Fragment NewInstance()
        {
            return new Top10Fragment { Arguments = new Bundle() };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.SmthArticleList, null);
            _context = view.Context;

            var listview=view.FindViewById<ListView>(Resource.Id.listView_ArticleList);

            var items = JsonConvert.DeserializeObject<List<Article>>(ResourceData.Top10);
            listview.Adapter = new ArticleListAdapter(_context, items);
            listview.ItemClick += Listview_ItemClick;
            return view;
        }

        private void Listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            Toast.MakeText(this._context, "You selected: " + e.Position, ToastLength.Short).Show();
        }
    }
}