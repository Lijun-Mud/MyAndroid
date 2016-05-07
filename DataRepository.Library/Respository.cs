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

namespace DataRepository.Library
{
    public class Respository
    {
        public Channel ReadPsi()
        {

            {
                //var child = new DemoChildMode[] { new DemoChildMode { Description = "c1" }, new DemoChildMode { Description = "c2" } }.ToList();
                //var list = new DemoModel[] { new DemoModel { Id = 1, Name = "a",item=new DemoItemMode{Children=child} }, new DemoModel { Id = 2, Name = "b", Children = child } }.ToList();
                //XmlSerializer serializer = new XmlSerializer(typeof(List<DemoModel>));
                //StringWriter stream=new StringWriter();
                //serializer.Serialize(stream, list);
                //System.Diagnostics.Debug.WriteLine( stream.ToString());
            }

            {
                var serializer = new XmlSerializer(typeof(Channel));
                object result;

                using (TextReader reader = new StringReader(Resource1.psi))
                {
                    result = serializer.Deserialize(reader);
                }
                return result as Channel;
            }
           

            //var request = HttpWebRequest.Create(@"http://www.nea.gov.sg/api/WebAPI?dataset=psi_update&keyref=781CF461BB6606ADEA01E0CAF8B35274629823F3B9F56626");
            //request.ContentType = "application/json";
            //request.Method = "GET";

            //using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            //{
            //    if (response.StatusCode != HttpStatusCode.OK)
            //        Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
            //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            //    {
            //        var content = reader.ReadToEnd();
            //        if (string.IsNullOrWhiteSpace(content))
            //        {
            //            Console.Out.WriteLine("Response contained empty body...");
            //        }
            //        else
            //        {
            //            Console.Out.WriteLine("Response Body: \r\n {0}", content);
            //        }

            //    }
            //}
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