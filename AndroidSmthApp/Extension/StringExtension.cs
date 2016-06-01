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
    public static class StringExtension
    {
        private static readonly DateTime StartDatetime=new DateTime(1970,1,1);
        public static DateTime ToDate(this string date)
        {
            return StartDatetime.AddSeconds(Convert.ToInt32(date));
        }
    }
}