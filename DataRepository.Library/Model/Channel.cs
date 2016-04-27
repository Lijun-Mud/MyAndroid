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
    //[System.Xml.Serialization.XmlRoot("channel")]
    public class channel
    {
        [System.Xml.Serialization.XmlElement("title")]
        public string Title { get; set; }
        [System.Xml.Serialization.XmlElement("source")]
        public string Source { get; set; }        
        [System.Xml.Serialization.XmlArray("item")]
        public List<region> Item { get; set; }
    }

    public class region
    {
        [System.Xml.Serialization.XmlElement("id")]
        public string Id { get; set; }
        [System.Xml.Serialization.XmlElement("latitude")]
        public string Latitude { get; set; }
        [System.Xml.Serialization.XmlElement("longitude")]
        public string Longitude { get; set; }
        [System.Xml.Serialization.XmlElement("record")]
        public PsiRecord Record { get; set; }
    }

    public class PsiRecord
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