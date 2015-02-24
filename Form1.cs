using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTunesLib;
using System.IO;
using System.Threading;

namespace TopTracks2iTunes
{
    public partial class Form1 : Form
    {
        private iTunesApp m_itunes = null;
        private TopTrackCache m_cache = null;

        public Form1()
        {
            InitializeComponent();
            PopulatePlaylistCombo();
        }

        private iTunesApp App
        {
            get
            {
                if (m_itunes == null)
                    m_itunes = new iTunesApp();

                return m_itunes;
            }
        }

        private void PopulatePlaylistCombo()
        {
            this.playlistComboBox.DisplayMember = "Name";
            this.playlistComboBox.Items.Clear();
            int idx = 0;
            int selIdx = 0;
            foreach (var playlist in GetLibraryPlaylists())
            {
                if (playlist.Name == "Music")
                    selIdx = idx;
                this.playlistComboBox.Items.Add(playlist);
                idx++;
            }
            if (this.playlistComboBox.Items.Count > 0)
                this.playlistComboBox.SelectedIndex = selIdx;
        }

        private class ProgressItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
        }

        private CancellationTokenSource Cancel { get; set; }

        private async void clearCommentsButton_Click(object sender, EventArgs e)
        {
            if (this.progressBar.Visible)
            {
                if (this.Cancel.IsCancellationRequested)
                    return;
                this.Cancel.Cancel();
                return;
            }
            IITPlaylist playList = this.playlistComboBox.SelectedItem as IITPlaylist;
            if (playList == null)
                return;

            int numTracks = playList.Tracks.Count;
            int clearCount = 0;
            this.messagesTextBox.Clear();
            this.messagesTextBox.AppendText(string.Format("Processing {0} tracks...\r\n", App.LibraryPlaylist.Tracks.Count));

            //ProgressForm progressForm = new ProgressForm();
            Progress<ProgressItem> progress = new Progress<ProgressItem>(item =>
            {
                this.messagesTextBox.AppendText(string.Format("Clearing comments for {0}\r\n", item.Text));
                int prog = (100 * item.Value) / numTracks;
                this.progressBar.Value = prog;
                clearCount++;
                //progressForm.ProgressText = item.Text;
                //progressForm.CurrentProgress = prog;
            });

            //progressForm.Activated += async (s, args) =>
            //    {
            //        bool completed = await ClearCommentsAsync(playList, progress, progressForm.CancellationToken);
            //        if (completed)
            //            progressForm.Close();
            //    };
            //progressForm.ShowDialog(this);
            
            this.progressBar.Visible = true;
            string text = this.clearCommentsButton.Text;
            this.clearCommentsButton.Text = "Stop";
            this.Cancel = new CancellationTokenSource();
            
            bool completed = await ClearCommentsAsync(playList, progress, this.Cancel.Token);

            this.messagesTextBox.AppendText(string.Format("Cleared comments for {0} tracks\r\n", clearCount));
            this.progressBar.Visible = false;
            this.clearCommentsButton.Text = text;
        }

        private Task<bool> ClearCommentsAsync(IITPlaylist playlist, IProgress<ProgressItem> progress, CancellationToken ct)
        {
            return Task.Run(() =>
            {
                return ClearComments(playlist, progress, ct);
            });
        }

        private bool ClearComments(IITPlaylist playlist, IProgress<ProgressItem> progress, CancellationToken ct)
        {
            int idx = 0;

            foreach (IITTrack track in playlist.Tracks)
            {
                if (ct.IsCancellationRequested)
                    return false;

                // only work with files
                IITFileOrCDTrack currTrack = track as IITFileOrCDTrack;
                if (currTrack != null && currTrack.Kind == ITTrackKind.ITTrackKindFile)                    
                {
                    progress.Report(new ProgressItem() { Text = currTrack.Name, Value = idx + 1 });
                    if (!string.IsNullOrEmpty(currTrack.Comment))
                        currTrack.Comment = string.Empty;
                }
                idx++;
            }
            return true;
        }

        private IEnumerable<IITPlaylist> GetLibraryPlaylists()
        {
            foreach (IITSource source in App.Sources)
            {
                if (source.Kind == ITSourceKind.ITSourceKindLibrary)
                {
                    foreach (IITPlaylist playList in source.Playlists)
                    {
                        yield return playList;
                    }
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Progress<int>
                this.messagesTextBox.Clear();
                this.messagesTextBox.AppendText(string.Format("Getting Top {1} tracks for {0}...\r\n", this.artistTextBox.Text, (int)this.trackLimit.Value));
                
                //var url = Utils.GetTopTracksUrl(this.artistTextBox.Text, (int) this.trackLimit.Value);
                //var xml = await Utils.GetResponse(url);
                //var items = Utils.GetTopTracks(xml);

                var artist = await Utils.GetArtistCorrection(this.artistTextBox.Text);
                if (artist != this.artistTextBox.Text)
                    this.messagesTextBox.AppendText(string.Format("Artist corrected from {0} to {1}\r\n", this.artistTextBox.Text, artist));
                var items = await Utils.GetTopTracks(artist, (int)this.trackLimit.Value);
                if (items.Count() == 0)
                {
                    this.messagesTextBox.AppendText("No tracks found");
                }
                else
                {
                    foreach (var track in items)
                    {
                        this.messagesTextBox.AppendText(string.Format("{1}: {0}\r\n", track.Name, track.Rank));
                    }
                }
            }
            catch (Exception ex)
            {
                this.messagesTextBox.AppendText("Exception: " + ex.Message);
            }
        }

        private async Task SetTopTracksComment(IITPlaylist playlist, IProgress<ProgressItem> progress, CancellationToken ct)
        {
            try
            {
                int idx = 0;
                string fileName = @"%temp%\TopTracks2iTunes.json";
                fileName = System.Environment.ExpandEnvironmentVariables(fileName);
                if (m_cache == null)
                    m_cache = TopTrackCache.Deserialize(fileName);

                foreach (IITTrack track in playlist.Tracks)
                {
                    if (ct.IsCancellationRequested)
                        break;

                    //only work with files, and only worry about ones where we have Artist, Name and not 
                    //already tagged as Top
                    bool nullComment = string.IsNullOrEmpty(track.Comment);
                    IITFileOrCDTrack currTrack = track as IITFileOrCDTrack;
                    if (currTrack != null && currTrack.Kind == ITTrackKind.ITTrackKindFile &&
                        !string.IsNullOrEmpty(currTrack.Name) && !string.IsNullOrEmpty(currTrack.Artist) &&
                        (nullComment || (!nullComment && !currTrack.Comment.Contains("Top:"))))
                    {
                        progress.Report(new ProgressItem() { Text = currTrack.Name, Value = idx + 1 });
                        var topTrack = await m_cache.GetTopTrack(currTrack.Artist, currTrack.Name);
                        if (topTrack != null && topTrack.Rank != 0 && topTrack.Rank <= 20)
                        {
                            int top = CeilingToNearest5(topTrack.Rank);
                            int playPercent = RoundToNearest5(topTrack.PlayCountPercent);
                            currTrack.Comment = string.Format("Top:{0:00},Rank:{1:00},PlayCount:{2:00}", top, topTrack.Rank, playPercent);
                        }
                    }
                    idx++;
                }

                m_cache.Serialize(fileName);
            }
            catch (Exception ex)
            {
                this.messagesTextBox.AppendText("Exception: " + ex.Message);
            }
        }

        private static int Round(int number, int rounding)
        {
            double numToRound = (double)number / (double)rounding;
            return (int)Math.Round(numToRound) * rounding;
        }

        private static int RoundToNearest5(int number)
        {
            return Round(number, 5);
        }

        private static int Ceiling(int number, int ceiling)
        {
            double numToCeiling = (double)number / (double)ceiling;
            return (int)Math.Ceiling(numToCeiling) * ceiling;
        }

        private static int CeilingToNearest5(int number)
        {
            return Ceiling(number, 5);
        }

        private async void setTopTracksButton_Click(object sender, EventArgs e)
        {
            if (this.progressBar.Visible)
            {
                if (this.Cancel.IsCancellationRequested)
                    return;
                this.Cancel.Cancel();
                return;
            }
            IITPlaylist playList = this.playlistComboBox.SelectedItem as IITPlaylist;
            if (playList == null)
                return;

            int numTracks = playList.Tracks.Count;
            int count = 0;
            this.messagesTextBox.Clear();
            this.messagesTextBox.AppendText(string.Format("Processing {0} tracks...\r\n", App.LibraryPlaylist.Tracks.Count));

            Progress<ProgressItem> progress = new Progress<ProgressItem>(item =>
            {
                this.messagesTextBox.AppendText(string.Format("Setting top track comment for {0}\r\n", item.Text));
                int prog = (100 * item.Value) / numTracks;
                this.progressBar.Value = prog;
                count++;
            });


            this.progressBar.Visible = true;
            string text = this.setTopTracksButton.Text;
            this.setTopTracksButton.Text = "Stop";
            this.Cancel = new CancellationTokenSource();

            await SetTopTracksComment(playList, progress, this.Cancel.Token);

            this.messagesTextBox.AppendText(string.Format("Set comments for {0} tracks\r\n", count));
            this.progressBar.Visible = false;
            this.setTopTracksButton.Text = text;
        }
    }
}
