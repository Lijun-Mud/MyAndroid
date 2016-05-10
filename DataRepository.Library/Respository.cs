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
using System.Net;
using System.IO;
using System.Xml.Serialization;
using DataRepository.Library.Model;
using System.Threading.Tasks;

namespace DataRepository.Library
{
    public class Respository
    {
        public async Task<Channel> ReadPsi()
        {
            return GetPsiInformationFromResouce();
            //var request = HttpWebRequest.Create(@"http://www.nea.gov.sg/api/WebAPI?dataset=psi_update&keyref=781CF461BB6606ADEA01E0CAF8B35274629823F3B9F56626");
            //request.ContentType = "application/json";
            //request.Method = "GET";

            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    if (response.StatusCode != HttpStatusCode.OK)
            //        throw new Exception(string.Format("Error fetching data. Server returned status code: {0}", response.StatusCode));
            //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //    {
            //        var content = reader.ReadToEnd();
            //        if (string.IsNullOrWhiteSpace(content)) throw new Exception(string.Format("Response contained empty body."));
            //        var serializer = new XmlSerializer(typeof(Channel));
            //        using (TextReader textReader = new StringReader(content))
            //        {
            //            return serializer.Deserialize(textReader) as Channel;
            //        }
            //    }
            //}
        }

        private static Channel GetPsiInformationFromResouce()
        {
            {
                //var child = new DemoChildMode[] { new DemoChildMode { Description = "c1" }, new DemoChildMode { Description = "c2" } }.ToList();
                //var list = new DemoModel[] { new DemoModel { Id = 1, Name = "a",item=new DemoItemMode{Children=child} }, new DemoModel { Id = 2, Name = "b", Children = child } }.ToList();
                //XmlSerializer serializer = new XmlSerializer(typeof(List<DemoModel>));
                //StringWriter stream=new StringWriter();
                //serializer.Serialize(stream, list);
                //System.Diagnostics.Debug.WriteLine( stream.ToString());
            }
            System.Threading.Thread.Sleep(5000);
            //throw new ArgumentException("abc");
            var serializer = new XmlSerializer(typeof(Channel));
            using (var reader = new StringReader(Resource1.psi))
            {
                return serializer.Deserialize(reader) as Channel;
            }
        }
    }

    public class DemoModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public DemoItemMode item { get; set; }
        public List<DemoChildMode> Children { get; set; }
    }

    public class DemoItemMode
    {
        public List<DemoChildMode> Children { get; set; }
    }

    public class DemoChildMode
    {
        public String Description { get; set; }
    }
}