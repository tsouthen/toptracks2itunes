using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using System.Xml.Linq;

namespace TopTracks2iTunes
{
    public class TopTrack
    {
        public int Rank;
        public string Name;
        //public string Mbid;
        public int PlayCount;
        public int PlayCountPercent;
        //public int Listeners;
        //public string Url;
    }

    public class Utils
    {
        const string API_KEY = "c0ba84716192db1b359eea7f2fc2fac2";

        public static async Task<string> GetResponse(string url)
        {
            var handler = new ModernHttpClient.NativeMessageHandler();
            handler.AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip;
            var httpClient = new System.Net.Http.HttpClient(handler);

            var response = await httpClient.GetAsync(new Uri(url));

            response.EnsureSuccessStatusCode();
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(responseStream, Encoding.GetEncoding("iso-8859-1")))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static string GetTopTracksUrl(string artist, int limit)
        {
            var url = string.Format("http://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&api_key={0}&limit={1}&artist={2}",
                API_KEY, limit, System.Web.HttpUtility.UrlEncode(artist));
            return url;
        }

        public static IEnumerable<TopTrack> GetTopTracks(string xml)
        {
            var result = XElement.Parse(xml);
            if (result != null)
            {
                var query = from item in result.Descendants().Descendants("track")
                            select new TopTrack
                            {
                                Rank = int.Parse(item.Attribute("rank").Value),
                                Name = (string)item.Element("name"),
                                //Mbid = (string)item.Element("mbid"),
                                PlayCount = int.Parse(item.Element("playcount").Value),
                                //Listeners = int.Parse(item.Attribute("listeners").Value),
                                //Url = (string)item.Element("url"),
                            };
                return query;
            }
            return new List<TopTrack>(); //an empty list
        }

        public static async Task<IEnumerable<TopTrack>> GetTopTracks(string artist, int limit)
        {
            string url = GetTopTracksUrl(artist, limit);
            var xml = await GetResponse(url);
            var items = GetTopTracks(xml);
            return items;
        }

        public static string GetArtistCorrectionUrl(string artist)
        {
            var url = string.Format("http://ws.audioscrobbler.com/2.0/?method=artist.getcorrection&api_key={0}&artist={1}",
                API_KEY, System.Web.HttpUtility.UrlEncode(artist));
            return url;
        }

        public static string GetArtistCorrection(string artist, string xml)
        {
            var result = XElement.Parse(xml);
            if (result != null)
            {
                if (result.Attribute("status").Value != "ok")
                    return null;

                XElement artistNode = result.XPathSelectElement("corrections/correction/artist");
                if (artistNode != null)
                    return (string) artistNode.Element("name");
            }
            return artist;
        }

        public static async Task<string> GetArtistCorrection(string artist)
        {
            string url = GetArtistCorrectionUrl(artist);
            var xml = await GetResponse(url);
            if (string.IsNullOrEmpty(xml))
                return artist;

            string corrected = GetArtistCorrection(artist, xml);
            return corrected;
        }
    }
}
