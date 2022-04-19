using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
// -------------------------------------------------------------------------
// if using .NET Framework
// https://docs.microsoft.com/en-us/dotnet/api/system.web.script.serialization.javascriptserializer?view=netframework-4.8
// This requires including the reference to System.Web.Extensions in your project
using System.Web.Script.Serialization;
using Newtonsoft.Json;
// -------------------------------------------------------------------------
namespace KursnaListaGrafikon
{
    class Program
    {
        static void ApiLoader(string[] args)
        {

            string QUERY_URL = "https://www.alphavantage.co/query?function=FX_INTRADAY&from_symbol=EUR&to_symbol=USD&interval=5min&apikey=demo";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {


                Dictionary<dynamic, dynamic> mydictionary = new Dictionary<dynamic, dynamic>();
                var text = client.DownloadString(queryUri);
                mydictionary = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(text);
                Console.WriteLine(mydictionary["Time Series FX (5min)"]);
                // List<string> datumi = new List<string>();
                // Dictionary<string,string> kurs = new Dictionary<string,string>();
                // List<Dictionary<string, string>> ohcl = new List<Dictionary<string, string>>();
                // foreach (var o in mydictionary["Time Series FX (5min)"])
                // {


                //     datumi.Add(o.Name);

                //     foreach (var i in o.Value)
                //     {
                //         if (kurs.ContainsKey(i.Name))
                //         {
                //             ohcl.Add(kurs);

                //             kurs.Clear();
                //         }

                //         else {
                //             kurs.Add(i.Name, i.Value.ToString());

                //         }
                //     }


                // }
                //foreach(var  dict in ohcl)
                // {
                //     foreach (var k in dict)
                //         Console.WriteLine(k);
                // }

                // //foreach(String datum in datumi)
                // //Console.WriteLine(datum);

            }
        }
    }
}
