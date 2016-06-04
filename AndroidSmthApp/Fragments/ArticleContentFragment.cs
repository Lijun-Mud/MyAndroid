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
using System.Threading.Tasks;

namespace AndroidSmthApp.Fragments
{
    public class ArticleContentFragment : Fragment
    {
        private Context _context;
        private List<ArticleThread> _threads;
        private ProgressBar _progressBar;
        private ListView _listView;
        private Android.App.Activity _activity;
        private ArticleThreadAdapter _adapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override void OnAttach(Android.App.Activity activity)
        {
            _activity = activity;
            base.OnAttach(activity);
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
            _listView = listview;

            _threads = JsonConvert.DeserializeObject<List<ArticleThread>>(ResourceData.Thread);
            _threads.ForEach(item => item.Body = item.Body.Length>100? item.Body.Substring(1, 99): item.Body);
            var count = 0;
            _threads.ForEach(item => item.AttachmentCount = (++count).ToString());
            listview.Adapter = new ArticleThreadAdapter(_context, _threads);
            _adapter = listview.Adapter as ArticleThreadAdapter;
            listview.ScrollStateChanged += Listview_ScrollStateChanged;
            //listview.Scroll += Listview_Scroll;

            var footer = inflater.Inflate(Resource.Layout.footer, null);
            _progressBar=footer.FindViewById<ProgressBar>(Resource.Id.footer_progressbar);
            _footer = footer;
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
        private bool isWorking = false;

        private async void Listview_ScrollStateChanged(object sender, AbsListView.ScrollStateChangedEventArgs e)
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

            if ((last + visibleThreshold) > totalItemCount && !IsLoading&& _footer.WindowVisibility==ViewStates.Visible&&!isWorking)
            {
                isWorking = true;
                System.Diagnostics.Debug.WriteLine(e.ScrollState+ "\t@@@\t" + " more" + last+"\t"+ _footer.WindowVisibility);
                //var pr = new Android.App.ProgressDialog(_context);
                //pr.SetMessage("Login...");
                //pr.SetCancelable(false);
                //pr.Show();
                _progressBar.Visibility = ViewStates.Visible;

                await Task.Factory.StartNew(() => BigLongImportantMethodAsync());

                var threads = JsonConvert.DeserializeObject<List<ArticleThread>>(ResourceData.Thread);
                threads.ForEach(item => item.Body = item.Body.Length > 100 ? item.Body.Substring(1, 99) : item.Body);
                var count = _listView.Adapter.Count - 1;
                threads.ForEach(item => item.AttachmentCount = (++count).ToString());

                _adapter.Add(threads);
                _adapter.NotifyDataSetChanged();

                _progressBar.Visibility = ViewStates.Invisible;
                isWorking = false;
                //pr.Hide();
                //Callback(ItemsPerRequest * currentPage);
                //IsLoading = true;
            }
        }

        private async void BigLongImportantMethodAsync()
        {
            System.Threading.Thread.Sleep(3000);
            _activity.RunOnUiThread(() =>
            {

            });
        }
    }
}