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
using Newtonsoft.Json;
using AndroidSmthApp.Model;
using AndroidSmthApp.Adapter;

namespace AndroidSmthApp.Fragments
{
    public class ArticleContentFragment : Fragment
    {
        private Context _context;
        private List<ArticleThread> _threads;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static ArticleContentFragment NewInstance()
        {
            return new ArticleContentFragment { Arguments = new Bundle() };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore= base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.smth_aticle_content, null);
            _context = view.Context;

            var listview = view.FindViewById<ListView>(Resource.Id.listView_article_content);

            _threads = JsonConvert.DeserializeObject<List<ArticleThread>>(ResourceData.Thread);
            listview.Adapter = new ArticleThreadAdapter(_context, _threads);

            return view;
        }

    }
}