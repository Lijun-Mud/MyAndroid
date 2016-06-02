using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidSmthApp.Model;
using AndroidSmthApp.Extension;

namespace AndroidSmthApp.Adapter
{
    public class ArticleThreadAdapter : BaseAdapter<ArticleThread>
    {
        private Context _context;
        private List<ArticleThread> _items;

        public ArticleThreadAdapter(Context context,List<ArticleThread> items):base()
        {
            this._context = context;
            _items = items;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public override ArticleThread this[int position]
        {
            get
            {
                return _items[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            ArticleThreadAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as ArticleThreadAdapterViewHolder;

            if (holder == null)
            {
                holder = new ArticleThreadAdapterViewHolder();
                var inflater = _context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.listview_row_thread, parent, false);
                //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
                view.Tag = holder;
            }

            //fill in your items
            var item = _items[position];
            view.FindViewById<TextView>(Resource.Id.article_content_author).Text = item.AuthorId;
            view.FindViewById<TextView>(Resource.Id.article_content_rank).Text = item.AttachmentCount;
            view.FindViewById<TextView>(Resource.Id.article_content_time).Text = item.Time.ToDate().ToShow();
            view.FindViewById<TextView>(Resource.Id.article_content_content).Text = item.Body;

            return view;
        }
    }   

    class ArticleThreadAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}