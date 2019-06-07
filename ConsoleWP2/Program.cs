using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Xml;

namespace ConsoleWP2
{
    class WeatherRequesting
    {
        
        private const string APIKEY = "55440b826467866f466ab454e86f4b3d";
        private XmlDocument xmlDocument;
        private string CitytURL;
        //private string location = "London";
        private string location;

        public WeatherRequesting (string location)
        {
            SetCityURL(location);
            xmlDocument = GetXML(CitytURL);
        }


        private void SetCityURL(string location)
        {
            CitytURL = "http://api.openweathermap.org/data/2.5/weather?q="
                + location + "&mode=xml&units=metric&APPID=" + APIKEY;
        }


        private XmlDocument GetXML(string CurrentURL)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(CurrentURL);
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
            {
                var response = streamReader.ReadToEnd();
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(response);
                return xmlDocument;
            }
        }

            static void Main(string[] args)
        {
            Console.Write("Введите свой город: ");
            string location = Console.ReadLine();
            
            //string location = "London";
            WeatherRequesting City = new WeatherRequesting("location");
            XmlDocument xmlDocument = City.xmlDocument;
            string temperature = xmlDocument.DocumentElement.SelectSingleNode("//temperature").Attributes["value"].Value;
            var humidity = xmlDocument.DocumentElement.SelectSingleNode("//humidity").Attributes["value"].Value;
            var sunRise = xmlDocument.DocumentElement.SelectSingleNode("//city").SelectSingleNode("//sun").Attributes["rise"].Value;
            var sunSet = xmlDocument.DocumentElement.SelectSingleNode("//city").SelectSingleNode("//sun").Attributes["set"].Value;


            Console.WriteLine("location=" + location);
            Console.WriteLine("temperature=" + temperature);
            Console.WriteLine("humidity=" + humidity);
            Console.WriteLine("sunRise=" + sunRise);
            Console.WriteLine("sunSet=" + sunSet);
            Console.WriteLine(DateTime.Now.ToString());
            var Path = DateTime.Now.ToString("d-M-yyyy HH-mm-ss") + ".txt";
            File.AppendAllText(Path, "location=" + location + "\t temperature=" + temperature + "\t humidity=" + humidity + "\t sunRise=" + sunRise + "\t sunSet=" + sunSet);
            //City.xmlDocument.Save(Console.Out);
            //City.xmlDocument.Save('');
            Console.ReadKey();
        }
    }
}
