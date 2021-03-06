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
        public string AuthorId { get; set; }
        public string Time { get; set; }
        public string Subject { get; set; }
        public string Board { get; set; }
        public string Count { get; set; }
    }
}