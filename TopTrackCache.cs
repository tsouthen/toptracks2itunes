using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopTracks2iTunes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TopTrackCache
    {
        [JsonProperty]
        private Dictionary<string, string> m_artistMap;
        [JsonProperty]
        private Dictionary<string, IList<TopTrack>> m_topTracks;

        private static char[] m_starts = new char[] { '(', '[', '{' };
        private static char[] m_ends = new char[] { ')', ']', ']' };

        public async Task<TopTrack> GetTopTrack(string artist, string trackName)
        {
            //get adjusted artist name
            if (m_artistMap == null)
                m_artistMap = new Dictionary<string, string>();

            string adjustedArtist = null;
            if (!m_artistMap.TryGetValue(artist, out adjustedArtist))
            {
                adjustedArtist = await Utils.GetArtistCorrection(artist);
                if (string.IsNullOrEmpty(adjustedArtist))
                    return null;

                m_artistMap[artist] = adjustedArtist;
            }

            //get top tracks
            if (m_topTracks == null)
                m_topTracks = new Dictionary<string, IList<TopTrack>>();

            IList<TopTrack> tracks = null;
            if (!m_topTracks.TryGetValue(adjustedArtist, out tracks))
            {
                IEnumerable<TopTrack> enumer = await Utils.GetTopTracks(adjustedArtist, 20);
                if (enumer == null)
                    return null;

                tracks = new List<TopTrack>(enumer);
                TopTrack one = tracks.FirstOrDefault(t => t.Rank == 1);
                one.PlayCountPercent = 100;
                foreach (TopTrack track in tracks)
                {
                    track.Name = track.Name.Trim().ToLower();
                    track.PlayCountPercent = track.PlayCount * 100 / one.PlayCount;
                }

                m_topTracks[adjustedArtist] = tracks;
            }

            //find track
            trackName = trackName.ToLower().Trim();
            TopTrack first = tracks.FirstOrDefault(t => t.Name == trackName);
            if (first != null)
                return first;

            //try again removing any bracketted text e.g. [Live]
            int startIdx = trackName.IndexOfAny(m_starts);
            if (startIdx >= 0)
            {
                int endIdx = trackName.IndexOfAny(m_ends);
                if (endIdx > startIdx)
                {
                    trackName = trackName.Remove(startIdx, endIdx - startIdx + 1).Trim();
                    TopTrack test = tracks.FirstOrDefault(t => t.Name == trackName);
                    if (test != null)
                        return test;
                }
            }
            return null;
        }

        public void Serialize(string fileName)
        {
            using (FileStream fs = File.Open(fileName, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, this);
            }
        }

        public static TopTrackCache Deserialize(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
                return new TopTrackCache();

            try
            {
                using (FileStream fs = File.Open(fileName, FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                using (JsonReader jr = new JsonTextReader(sr))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return serializer.Deserialize<TopTrackCache>(jr);
                }

            }
            catch (Exception)
            {
                //try to delete offending serialized file
                try
                {
                    File.Delete(fileName);
                }
                catch (Exception)
                {
                }
                return new TopTrackCache();
            }
        }
    }
}
