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
using Newtonsoft.Json;

namespace AndroidSmthApp.Model
{
    public class Article
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string authorId { get; set; }
        public string time { get; set; }
        public string subject { get; set; }
        public string Board { get; set; }
        public string count { get; set; }
    }
}