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

namespace AndroidSmthApp.Adapter
{
    public class ArticleListAdapter : BaseAdapter<Article>
    {

        private Context _context;
        private List<Article> _items;

        public ArticleListAdapter(Context context,List<Article> items):base()
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

        public override Article this[int position]
        {
            get
            {
                return _items[position];
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            ArticleListAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as ArticleListAdapterViewHolder;

            if (holder == null)
            {
                holder = new ArticleListAdapterViewHolder();
                var inflater = _context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                view = inflater.Inflate(Resource.Layout.Listview_row, parent, false);
                //holder.Title = view.FindViewById<TextView>(Resource.Id.text);
                view.Tag = holder;
            }

            //fill in your items
            var item = _items[position];
            view.FindViewById<TextView>(Resource.Id.RowSmallText).Text = item.Board;
            view.FindViewById<TextView>(Resource.Id.RowDetaillText).Text = item.subject;

            return view;
        }
    }

    class ArticleListAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}