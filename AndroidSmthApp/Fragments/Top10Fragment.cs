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
        private Context _context;
        private List<Article> _articles;

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
            var view = inflater.Inflate(Resource.Layout.smth_article_list, null);
            _context = view.Context;

            var listview=view.FindViewById<ListView>(Resource.Id.listView_ArticleList);

            _articles = JsonConvert.DeserializeObject<List<Article>>(ResourceData.Top10);
            listview.Adapter = new ArticleListAdapter(_context, _articles);
            listview.ItemClick += Listview_ItemClick;
            return view;
        }

        private void Listview_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var parent = Activity as Activities.MainActivity;
            var item = _articles[e.Position];
            Toast.MakeText(this._context, "You selected: " + item.Subject, ToastLength.Short).Show();
        }
    }
}