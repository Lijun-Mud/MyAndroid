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

namespace AndroidSmthApp.Model
{
    public class Attachment
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Pos { get; set; }
    }
}