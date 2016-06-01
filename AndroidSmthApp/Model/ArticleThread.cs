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
    public class ArticleThread
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("author_id")]
        public string AuthorId { get; set; }
        public string Flags { get; set; }
        public string Time { get; set; }
        [JsonProperty("attachment_list")]
        public Attachment[] Attachments { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        [JsonProperty("attachments")]
        public string AttachmentCount { get; set; }
    }
}