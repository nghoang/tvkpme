using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TVKing2
{
    public delegate void DeClickYoutubeChannel(int index);
    public delegate void DeLoadChannel(bool force_reload = false);
    public delegate void DeReLoadLoadChannel();
    public partial class YoutubeVideoList : UserControl, IYoutubeSearch
    {
        public DeClickYoutubeChannel deClickYoutubeChannel;
        public List<YoutubeObject> youtube_videos;
        int left_margin = 7;
        int top_margin = 7;
        int image_width = 138;
        int image_heigh = 103;
        int item_width = 150;
        int item_heigh = 140;
        YoutubeSearch ys = new YoutubeSearch();

        List<PictureBox> main_images;
        List<TVKLabel> main_titles;

        public YoutubeVideoList()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            btLoadMore.Left = this.Width - bar.Width - btLoadMore.Width - 5;
            btLoadMore.Top = this.Height - btLoadMore.Height - 5;
            barSearching.Left = this.Width - bar.Width - barSearching.Width - btStop.Width - 12;
            barSearching.Top = this.Height - barSearching.Height - 8;
            btStop.Top = btLoadMore.Top;
            btStop.Left = this.Width - bar.Width - btStop.Width - 5;
            LoadChannel();
        }

        int last_hor_items = 0;
        int last_ver_items = 0;

        public void ReLoadLoadChannel()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DeReLoadLoadChannel(ReLoadLoadChannel));
                return;
            }
            bar.Value = 0;
            bar.Minimum = 0;
            last_hor_items = 0;
            last_ver_items = 0;
            LoadChannel();
            timer1.Enabled = true;
        }

        private void LoadChannel(bool force_reload = false)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DeLoadChannel(LoadChannel), force_reload);
                return;
            }
            bar.Left = this.Width - bar.Width;
            bar.Top = 0;
            bar.Height = this.Height;

            if (youtube_videos == null)
                return;

            int hor_items = 0;
            int ver_items = 0;
            hor_items = (int)Math.Floor(this.Width / (decimal)(item_width + left_margin));
            ver_items = (int)Math.Floor(this.Height / (decimal)(item_heigh + top_margin));
            bar.Maximum = (int)Math.Floor((double)youtube_videos.Count / (double)(hor_items * ver_items));

            if (bar.Value == bar.Maximum)
            {
                btLoadMore.Visible = true;
            }
            else
            {
                btLoadMore.Visible = false;
            }
            main_images = new List<PictureBox>();
            main_titles = new List<TVKLabel>();

            bar.Value = cur_bar;
            if (last_hor_items == hor_items && last_ver_items == ver_items && force_reload == false)
            {
                return;
            }
            last_hor_items = hor_items;
            last_ver_items = ver_items;

            int cC = 0;
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[cC] is TVKingScrollbar || Controls[cC] is DevExpress.XtraEditors.SimpleButton || Controls[cC] is ProgressBar)
                {
                    cC++;
                    continue;
                }
                if (Controls[cC] != null)
                {
                    //Controls[cC].Dispose();
                    Controls.RemoveAt(cC);
                }
            }
            int curr_adding_item = 0;
            for (int i = 0; i < ver_items; i++)
            {
                for (int j = 0; j < hor_items; j++)
                {
                    int cur_item = curr_adding_item + bar.Value * (hor_items * ver_items);
                    if (cur_item >= youtube_videos.Count)
                    {
                        return;
                    }
                    //Console.WriteLine(cur_item + " " + youtube_videos[cur_item].title);
                    PictureBox img = new PictureBox();
                    Label txt = new Label();

                    img.Cursor = Cursors.Hand;
                    img.SizeMode = PictureBoxSizeMode.StretchImage;
                    img.LoadAsync("http://" + youtube_videos[cur_item].image);
                    img.Left = left_margin + j * (left_margin + item_width);
                    img.Top = top_margin + i * (top_margin + item_heigh);
                    img.Width = image_width;
                    img.Height = image_heigh;
                    img.Name = "img_" + curr_adding_item;
                    img.MouseClick += new MouseEventHandler(img_MouseClick);
                    main_images.Add(img);
                    Controls.Add(img);

                    txt.Text = CrawlerLib.Net.Utility.HtmlDecode(youtube_videos[cur_item].title);
                    //Console.WriteLine(txt.Text);
                    txt.Top = img.Top + img.Height + 4;
                    txt.Left = img.Left;
                    txt.BackColor = System.Drawing.Color.Transparent;
                    txt.ForeColor = System.Drawing.Color.DarkGray;
                    txt.Width = img.Width;
                    Controls.Add(txt);

                    curr_adding_item++;
                }
            }
        }

        void img_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox img = (PictureBox)sender;
            string index = img.Name.Replace("img_", "");
            int i = int.Parse(index);
            deClickYoutubeChannel.Invoke(i);
        }

        int cur_bar = 0;

        private void bar_Scroll(object sender, EventArgs e)
        {
            if (bar.Value == cur_bar)
                return;
            cur_bar = bar.Value;
            last_hor_items = 0;
            last_ver_items = 0;
            LoadChannel();
        }

        private void btLoadMore_Click(object sender, EventArgs e)
        {

        }

        public void IYS_BeginSearching()
        {
            
        }

        public void IYS_Stop()
        {
            barSearching.Visible = false;
            btStop.Visible = false;
            btLoadMore.Visible = true;
        }

        public void IYS_ProgressSearching(int percent)
        {
            Console.WriteLine(percent);
            barSearching.Value = percent;
        }

        public void IYS_FinishedSearching(List<YoutubeObject> _youtube_videos)
        {
            barSearching.Visible = false;
            btStop.Visible = false;
            youtube_videos.AddRange(_youtube_videos);
            LoadChannel(true);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (youtube_videos == null)
            {
                youtube_videos = new List<YoutubeObject>();
            }
            btLoadMore.Visible = false;
            barSearching.Value = 3;
            barSearching.Visible = true;
            btStop.Visible = true;
            ys = new YoutubeSearch();
            ys.callback = this;
            //ys.ContinueSearching();
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(ys.ContinueSearching));
            th.Start();
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            ys.stoping = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadChannel(true);
            timer1.Enabled = false;
        }
    }
}
