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

namespace DataRepository.Library.Model
{
    public class channel
    {
        public string title { get; set; }
        public string source { get; set; }
        public List<region> item { get; set; }
    }

    public class region
    {
        public string id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public record record { get; set; }
    }

    public class record
    {
        [System.Xml.Serialization.XmlAttribute("timestamp")]
        public string Timestamp { get; set; }
        [System.Xml.Serialization.XmlElement(ElementName = "reading")]
        public List<PsiReading> PsiReadings { get; set; }
    }

    public class PsiReading
    {
        [System.Xml.Serialization.XmlAttribute("type")]
        public string Type { get; set; }
        [System.Xml.Serialization.XmlAttribute("value")]
        public string Value { get; set; }
    }
}