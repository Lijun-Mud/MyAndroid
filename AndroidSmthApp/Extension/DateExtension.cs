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

namespace AndroidSmthApp.Extension
{
    public static class DateExtension
    {
        public static string ToShow(this DateTime date)
        {
            if (date.Date == DateTime.Now.Date) return date.ToString("HH:mm:ss");
            return date.ToString("MM/dd HH:mm:ss");
        }
    }
}