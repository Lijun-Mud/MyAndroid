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
            _threads.ForEach(item => item.Body = item.Body.Length>100? item.Body.Substring(1, 99): item.Body);
            var count = 0;
            _threads.ForEach(item => item.AttachmentCount = (++count).ToString());
            listview.Adapter = new ArticleThreadAdapter(_context, _threads);
            listview.ScrollStateChanged += Listview_ScrollStateChanged;
            //listview.Scroll += Listview_Scroll;

            //View footer = ((LayoutInflater)this.GetSystemService(Context.LayoutInflaterService)).Inflate(Resource.Layout.footer_layout2, null, false);
            var footer = inflater.Inflate(Resource.Layout.footer, null);
            _footer = footer;
            //var footer = view.FindViewById<LinearLayout>(Resource.Id.footer_layout);
            listview.AddFooterView(footer);

            return view;
        }

        private void Listview_Scroll(object sender, AbsListView.ScrollEventArgs e)
        {
            var listview = sender as ListView;
            var adapter = listview.Adapter as ArticleThreadAdapter;

            var firstIndex = e.FirstVisibleItem;
            var visibleCount = e.VisibleItemCount;
            var lastShownItemIndex = firstIndex + visibleCount;

            System.Diagnostics.Debug.WriteLine(listview.LastVisiblePosition+"\t@@@\t" + firstIndex+"\t"+ visibleCount);
        }

        private View _footer;
        private bool IsLoading = false;
        private int PreviousTotalItemCount = 0;
        public int visibleThreshold = 1;
        public static int ItemsPerRequest = 10;
        public int currentPage = 0;

        private void Listview_ScrollStateChanged(object sender, AbsListView.ScrollStateChangedEventArgs e)
        {
            var listview = sender as ListView;
            var adapter = listview.Adapter as ArticleThreadAdapter;
            //System.Diagnostics.Debug.WriteLine(e.ScrollState);

            int last = listview.LastVisiblePosition+1;
            int totalItemCount = listview.Adapter.Count-1;

            if (IsLoading && totalItemCount > PreviousTotalItemCount)
            {
                IsLoading = false;
                PreviousTotalItemCount = totalItemCount;
                ++currentPage;
            }

            if ((last + visibleThreshold) > totalItemCount && !IsLoading)
            {
                System.Diagnostics.Debug.WriteLine(e.ScrollState+ "\t@@@\t" + " more" + last+"\t"+ _footer.WindowVisibility);
                //Callback(ItemsPerRequest * currentPage);
                //IsLoading = true;
            }
        }
    }
}