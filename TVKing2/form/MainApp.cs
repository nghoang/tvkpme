using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using CrawlerLib.Net;
using WMPLib;
using DevExpress.XtraBars.Ribbon;
using TVKing2.form;

namespace TVKing2
{
    delegate void DeCallbackChannelIcon(Channel ch, string icon_key);
    delegate void DeCallbackChannel(Channel ch);
    delegate void DeCallbackString(string res);
    delegate void DeCallbackYoutubeList(List<YoutubeObject> youtube_videos);
    delegate void DeCallbackInt(int res);
    delegate void DeCallback();


    public partial class MainApp : Form, IFormYoutube, IYoutubeSearch, IRegisterCheck
    {
        bool server_config_showlogo = false;
        bool server_config_ads = false;
        fmRegister fmReg = new fmRegister();
        ImageList flagList = new ImageList();
        List<Channel> streamList = new List<Channel>();
        List<Channel> webList = new List<Channel>();
        SystemUserCheck systemuser = new SystemUserCheck();

        //bool isLoadVideo = false;
        Channel currentPlayingChannel = null;
        ChannelFavorites favList = new ChannelFavorites();
        fmAdvertisement adv;
        bool isShownAds = false;
        bool isStop = false;
        bool isMoving = false;
        int currentX;
        int currentY;
        FlashPopup flashpop = new FlashPopup();
        bool isVideoPlaying = false;
        string defaultCountry = "";
        fmUpdate fmupdate;
        RequestTVKServer searchingRequest = new RequestTVKServer();
        TVKAdsRight tvkads;

        string cur_key = "";
        bool cur_country = false;
        bool cur_category = false;
        bool cur_most_watched = false;
        bool cur_last_watched = false;

        List<YoutubeObject> _youtube_videos;
        int current_youtubeid = 0;
        int max_youtubeid = 0;
        bool playingYoutube = false;
        bool is_small_mode = false;
        //bool BufferingYoutube = false;

        //[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //private static extern IntPtr CreateRoundRectRgn
        //(
        //int nLeftRect // x-coordinate of upper-left corner
        //,
        //int nTopRect // y-coordinate of upper-left corner
        //,
        //int nRightRect // x-coordinate of lower-right corner
        //,
        //int nBottomRect // y-coordinate of lower-right corner
        //,
        //int nWidthEllipse // height of ellipse
        //,
        //int nHeightEllipse // width of ellipse
        //);

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(global::TVKing2.Properties.Resources.form_top_left, 0, 0);
            g.DrawImage(global::TVKing2.Properties.Resources.form_top_right, this.Width - global::TVKing2.Properties.Resources.form_top_right.Width, 0);
            g.DrawLine(new Pen(Color.Black, 4), 0, 0, this.Width, 0);
            g.DrawLine(new Pen(Color.Black, 4), 0, 0, 0, this.Height);
            g.DrawLine(new Pen(Color.Black, 5), 0, this.Height, this.Width, this.Height);
            g.DrawLine(new Pen(Color.Black, 5), this.Width, 0, this.Width, this.Height);
            g.FillRectangle(new TextureBrush(global::TVKing2.Properties.Resources.form_top), 0, 0, this.Width, global::TVKing2.Properties.Resources.form_top.Height);
            this.SetStyle(ControlStyles.DoubleBuffer |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.UserPaint, true);
            base.OnPaint(e);
        }

        public MainApp()
        {
            flashpop.Show();
            this.Visible = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            //Control.CheckForIllegalCrossThreadCalls = false;
            trVolume.VolumeScroll += new TVKing2.TVKVolumeScrollEvent(tvkVolume1_VolumeScroll);
            tvkSeeking.EndVolumeScroll += new TVKEndVolumeScrollEvent(tvkSeeking_EndVolumeScroll);
        }

        void tvkVolume1_VolumeScroll(object sender, int value)
        {
            //player.Volume = trVolume.value;
            player_wmp.settings.volume = trVolume.value;
        }

        void tvkSeeking_EndVolumeScroll()
        {
            double duration = player_wmp.currentMedia.duration;
            IWMPControls control = (WMPLib.IWMPControls3)player_wmp.Ctlcontrols;
            double position = tvkSeeking.value * duration / 100;
            control.currentPosition = position;
        }

        private void AgreeUpdate()
        {
            fmupdate = new fmUpdate(this);
            fmupdate.Show();
        }

        private void LoadFlags()
        {
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\flags");
            FileInfo[] rgFiles = di.GetFiles("*.bmp");
            foreach (FileInfo fi in rgFiles)
            {
                Image img = Image.FromFile(fi.FullName);
                flagList.Images.Add(fi.Name.Split('.')[0].ToUpper(), img);
            }
            flagList.Images.Add("DEFAULT", global::TVKing2.Properties.Resources._default);
            flagList.ImageSize = new Size(22, 14);

            lvStream.SetImageList(flagList);
            lvWeb.SetImageList(flagList);
        }

        private void SwitchToMiniMode()
        {
            is_small_mode = true;
            this.MinimumSize = new Size(150, 150);

            this.Size = new Size(300, 200);

            //player.Top = 5;
            //player.Left = 0;
            player_flash.Top = 5;
            player_flash.Left = 0;
            player_wmp.Top = 5;
            player_wmp.Left = 0;
            lbMini.Location = new Point(0, 0);
            lbMini.Visible = true;

            //player.Width = this.Width;
            //player.Height = this.Height - 5;
            player_flash.Width = this.Width;
            player_flash.Height = this.Height - 5;
            player_wmp.Width = this.Width;
            player_wmp.Height = this.Height - 5;

            cbCountry.Visible = false;
            cbCategory.Visible = false;

            player_wmp.BringToFront();
            //player.BringToFront();
            player_flash.BringToFront();
            dragBar.BringToFront();
            lbMini.BringToFront();
            panel1.BringToFront();
        }

        private void SwitchToNormalMode()
        {
            is_small_mode = false;
            this.Size = new Size(891, 600);
            this.MinimumSize = new Size(891, 600);
            lbMini.Visible = false;

            cbCountry.Visible = true;
            cbCategory.Visible = true;

            dragBar.SendToBack();

            //player.SendToBack();
            player_flash.SendToBack();
            dragBar.SendToBack();

            LocateControls();
        }

        //locating controls
        public void LocateControls()
        {
            if (is_small_mode == true)
            {
                panel1.Left = this.Width - panel1.Width;
                panel1.Top = this.Height - panel1.Height;

                player_flash.Top = 0;
                player_flash.Left = 0;
                player_flash.Width = this.Width;
                player_flash.Height = this.Height;

                player_wmp.Top = 0;
                player_wmp.Left = 0;
                player_wmp.Width = this.Width;
                player_wmp.Height = this.Height;
                return;
            }
            //player.Left = pnInfo.Left;
            //player.Top = pnInfo.Top + pnInfo.Height + 7;
            //player.Width = pnInfo.Width;
            //player.Height = lvStream.Height - player.Top + lvStream.Top;
            pnInfo.Width = this.Width - pnInfo.Left - 15;

            lvStream.Height = this.Height - lvStream.Top - 90;

            int left_pos = pnInfo.Left;
            int top_pos = pnInfo.Top + pnInfo.Height + 7;
            int width_pos = pnInfo.Width;
            int hight_pos = lvStream.Height - top_pos + lvStream.Top;

            dragBar.Left = 200;
            dragBar.Width = iconMinimize.Left - 10 - dragBar.Left;

            lvWeb.Top = lvStream.Top;
            lvWeb.Left = lvStream.Left;
            lvWeb.Height = lvStream.Height;
            lvWeb.Width = lvStream.Width;

            player_flash.Top = top_pos;
            player_flash.Left = left_pos;
            player_flash.Width = width_pos;
            player_flash.Height = hight_pos;

            player_wmp.Top = top_pos;
            player_wmp.Left = left_pos;
            player_wmp.Width = width_pos;
            player_wmp.Height = hight_pos;

            tvkTrialScreen1.Size = player_wmp.Size;
            tvkTrialScreen1.Top = player_wmp.Top;
            tvkTrialScreen1.Left = player_wmp.Left;

            adsMain2.Size = player_wmp.Size;
            adsMain2.Top = player_wmp.Top;
            adsMain2.Left = player_wmp.Left;

            pnStatus.Top = lvWeb.Top + lvWeb.Height - pnStatus.Height;
            pnStatus.Width = player_wmp.Width;
            pnStatus.Left = player_wmp.Left;

            trVolume.Width = pictureBoxPause.Width + pictureBoxPlay.Width + pictureBoxStop.Width + 5;
            trVolume.Top = pnStatus.Top + pnStatus.Height + 5;
            trVolume.Left = pnStatus.Width / 2 + pnStatus.Left - trVolume.Width / 2;

            galleryControlYoutube.Top = top_pos;
            galleryControlYoutube.Left = left_pos;
            galleryControlYoutube.Width = width_pos;
            galleryControlYoutube.Height = hight_pos;
            galleryControlYoutube.Visible = false;

            pbErrorBG.Width = width_pos;
            pbErrorBG.Height = hight_pos;
            pbErrorBG.Top = top_pos;
            pbErrorBG.Left = left_pos;

            pbStopSearch.Left = pbStartSearch.Left;
            pbStopSearch.Top = pbStartSearch.Top;

            tvkTrialScreen1.Top = top_pos + (hight_pos - tvkTrialScreen1.Height) / 2;
            tvkTrialScreen1.Left = left_pos + (width_pos - tvkTrialScreen1.Width) / 2;

            pbRevFav.Top = pbAddFav.Top;
            pbRevFav.Left = pbAddFav.Left;

            txtTitle.Left = pictureBox1.Left - 4;

            pbDivider.Top = top_pos;
            pbDivider.Width = lvStream.Width;
            pbDivider.Left = lvStream.Left;
            pbDivider.Height = 4;

            dragBar.Left = 0;
            dragBar.Width = this.Width;

            cbCategory.BringToFront();
            cbCountry.BringToFront();
            panel1.BringToFront();


            pictureBoxPause.Left = trVolume.Left + 1;
            pictureBoxPlay.Left = pictureBoxPause.Left + pictureBoxPause.Width;
            pictureBoxStop.Left = pictureBoxPlay.Left + pictureBoxPlay.Width;
            pictureBoxPause.Top = trVolume.Top + trVolume.Height;
            pictureBoxPlay.Top = pictureBoxPause.Top;
            pictureBoxStop.Top = pictureBoxPlay.Top;

            pbFullScreen.Left = pnInfo.Left + 10;
            pbFullScreen.Top = pnInfo.Top + pnInfo.Height - 30;

            pbMinimize.Top = pbFullScreen.Top + 4;
            pbMinimize.Left = pbFullScreen.Left + pbFullScreen.Width + 10;

            pbBandwidth.Left = pbFullScreen.Left + 10;
            pbBandwidth.Top = pbFullScreen.Top - 18;

            pbAddFav.Top = pbFullScreen.Top;
            pbRevFav.Top = pbAddFav.Top;
            pbRevFav.Left = pbAddFav.Left;

            pictureBox11.Left = lvWeb.Left;
            pictureBox11.Top = lvWeb.Top + lvWeb.Height + 40;

            label2.Left = pictureBox11.Left + pictureBox11.Width + 5;
            label2.Top = lvWeb.Top + lvWeb.Height + 20;
            label1.Left = label2.Left;
            label3.Left = label2.Left;
            label1.Top = lvWeb.Top + lvWeb.Height + 40;
            label3.Top = lvWeb.Top + lvWeb.Height + 60;

            labelOnlineUser.Left = label2.Left + label2.Width + 5;
            labelInstalledUser.Left = labelOnlineUser.Left;
            labelTotalUser.Left = labelOnlineUser.Left;
            labelOnlineUser.Top = label2.Top;
            labelInstalledUser.Top = label1.Top;
            labelTotalUser.Top = label3.Top;

            webBrowser1.Left = labelTotalUser.Left + labelTotalUser.Width + 20;
            webBrowser1.Top = label2.Top;

            pictureBoxError.Left = pnStatus.Left + pnStatus.Width - pictureBoxError.Width;
            pictureBoxCopyRight.Left = pictureBoxError.Left - pictureBoxCopyRight.Width - 5;
            pictureBoxEmail.Left = pictureBoxCopyRight.Left - pictureBoxEmail.Width - 5;
            pictureBoxError.Top = pnStatus.Top + pnStatus.Height + 5;
            pictureBoxCopyRight.Top = pictureBoxError.Top;
            pictureBoxEmail.Top = pictureBoxCopyRight.Top;

            panel1.Left = this.Width - panel1.Width;
            panel1.Top = this.Height - panel1.Height;

            tvkSeeking.Top = pnStatus.Top + pnStatus.Height + 5;
            tvkSeeking.Left = trVolume.Left + trVolume.Width + 10;
            tvkSeeking.Width = pictureBoxEmail.Left - tvkSeeking.Left - 10;

            buttonBackYoutube.Top = tvkSeeking.Top + tvkSeeking.Height + 5;
            buttonBackYoutube.Left = tvkSeeking.Left + tvkSeeking.Width / 2 - (buttonBackYoutube.Width + buttonNextYoutube.Width + 5) / 2;
            buttonNextYoutube.Left = buttonBackYoutube.Left + buttonBackYoutube.Width + 5;
            buttonNextYoutube.Top = buttonBackYoutube.Top;

            iconClose.Top = 14;
            iconClose.Left = this.Width - iconClose.Width - 18;
            iconMaximize.Top = 14;
            iconMaximize.Left = iconClose.Left - iconMaximize.Width - 14;
            iconMinimize.Top = 14;
            iconMinimize.Left = iconMaximize.Left - iconMinimize.Width - 14;
        }

        private void LoadServerConfig()
        {
            RequestTVKServer sv = new RequestTVKServer();
            string content = sv.GetConfig();
            string t = ParserLib.GetBlockSingle(content, "show_logo", "", "", "", "", "", "").innerText.Trim();
            if (t == "1")
            {
                server_config_showlogo = true;
            }
            else
            {
                server_config_showlogo = false;
            }

            t = ParserLib.GetBlockSingle(content, "tvkads", "", "", "", "", "", "").innerText.Trim();
            if (t == "1")
            {
                server_config_ads = true;
            }
            else
            {
                server_config_ads = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //default seeking bar for youtube channel
            tvkSeeking.is_show_icons = false;
            tvkSeeking.value = 0;

            //show version on title
            labelVersion.Text = "Version: " + AppConst.Version;



            //check if those 2 files exists on same folder
            if (!Utility.IsFileExist("fav.txt"))
                Utility.WriteFile("fav.txt", "", true);
            if (!Utility.IsFileExist("log.txt"))
                Utility.WriteFile("log.txt", "", true);

            //load configuration parameters from server
            LoadServerConfig();

            //set default country
            defaultCountry = Utility.ReadAppRegistry("TVKing2", "default_country");
            if (defaultCountry == "")
            {
                defaultCountry = "India";
            }

            //checked register
            switch (AppConst.appType)
            {
                case RegisterType.Ads:
                    barTrialHours.Visible = false;
                    break;
                case RegisterType.Free:
                    lbLeftDays.Visible = false;
                    label10.Visible = false;
                    label11.Text = "Register to watch OnlineTV UNLIMITED for FREE!";
                    lbRegisterText.Text = AppConst.TrialHours + " Hours watching TV";
                    label11.Left = lbRegisterText.Left;
                    label11.Top = lbRegisterText.Top + 20;
                    break;
                case RegisterType.Hours:
                    lbLeftDays.Visible = false;
                    label10.Visible = false;
                    label11.Text = "You are using Trial version. Please Register to remove Time Limit";
                    lbRegisterText.Text = AppConst.TrialHours + " Hours watching TV";
                    label11.Left = lbRegisterText.Left;
                    label11.Top = lbRegisterText.Top + 20;
                    break;
                case RegisterType.HoursDaily:
                    barTrialHours.Visible = false;
                    break;
                case RegisterType.Trial:
                    barTrialHours.Visible = false;
                    break;
            }

            //search keyword text to empty
            lbListTitle.Text = "";

            //set events for customed lists
            cbCategory.IndexChanged += new TVKing2.TVKComboBoxIndexChangedEvent(tvkComboBox2_IndexChanged);
            cbCountry.IndexChanged += new TVKing2.TVKComboBoxIndexChangedEvent(cbCountry_IndexChanged);
            lvStream.RatingClick += new DeRatingClick(lvStream_RatingClick);
            lvWeb.RatingClick += new DeRatingClick(lvStream_RatingClick);
            lvStream.DefaultClick += new DeTVKDefaultClick(lvStream_DefaultClick);

            //set height for customed lists
            cbCategory.SetListHeigh(150);
            cbCountry.SetListHeigh(250);

            //set channel title to empty
            txtTitle.Text = "";

            //check register
            RegisterCheck reg = new RegisterCheck();
            bool registerCheck = reg.IsRegister();
            registerCheck = true;
            if (registerCheck == true)
            {
                registerToolStripMenuItem.Enabled = false;
                tvkLabel2.Visible = false;
                tvkPictureBox2.Visible = false;
                lbRegisterText.Visible = false;
                lbLeftDays.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                barTrialHours.Visible = false;
                switch (AppConst.appType)
                {
                    case RegisterType.Free:
                        registerToolStripMenuItem1.Text = "Free Version";
                        registerToolStripMenuItem1.Visible = true;
                        registerToolStripMenuItem1.Enabled = false;
                        break;
                    case RegisterType.Hours:
                    case RegisterType.Ads:
                    case RegisterType.HoursDaily:
                    case RegisterType.Trial:
                        registerToolStripMenuItem1.Text = "Registering";
                        registerToolStripMenuItem1.Visible = true;
                        if (reg.IsRegister() == true)
                            registerToolStripMenuItem1.Enabled = false;
                        else
                            registerToolStripMenuItem1.Enabled = true;

                        break;
                }
            }
            else
            {
                switch (AppConst.appType)
                {
                    case RegisterType.Ads:
                        lbRegisterText.Text = "Please register to watch TV Without Ads";
                        lbLeftDays.Text = "";
                        label10.Text = "";
                        adv = new fmAdvertisement();
                        adv.Show();
                        tmAdvMoving.Enabled = true;
                        break;
                    case RegisterType.Free:
                        break;
                    case RegisterType.Hours:
                        reg.SetTimeUsed();
                        int timeLeft = reg.GetTimeLeft();
                        if (timeLeft < 0)
                        {
                            fmReg.Show();
                        }
                        break;
                    case RegisterType.HoursDaily:
                        break;
                    case RegisterType.Trial:
                        if (reg.IsExpired() == true)
                        {
                            isStop = true;
                            fmReg.Show();
                            tvkTrialScreen1.Visible = true;
                            pbErrorBG.Visible = true;
                        }
                        lbLeftDays.Text = reg.GetDayLeft() + " Days";
                        break;
                }
            }

            //get country and category from server
            Thread thCountry = new Thread(new ThreadStart(LoadCountry));
            Thread thCategory = new Thread(new ThreadStart(LoadCategory));
            thCountry.Start();
            thCategory.Start();

            //load flags into memory
            LoadFlags();

            //create folder to store channel logos
            string logFolder = Directory.GetCurrentDirectory() + @"\logos";
            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

            //pre-set location of all controls in main form
            LocateControls();

            //show main logo on center of main form
            pbErrorBG.Visible = true;

            //set loaded flags for stream and web control
            lvStream.SetImageList(flagList);
            lvStream.Clear();
            lvWeb.SetImageList(flagList);
            lvWeb.Clear();

            //set some default properties for windows media player
            player_wmp.Visible = false;
            player_wmp.uiMode = "none";
            player_wmp.stretchToFit = true;

            //show stop button
            pbStopSearch.Visible = false;

            //check version and update if it is old version
            Thread thCheckVersion = new Thread(new ThreadStart(InitCheckVersion));
            thCheckVersion.Start();

            //auto searching by default country
            SearchQuery(defaultCountry, true, false);
            SelectLabel(labelIndia);

            //set timer auto play checking if there is channel to play automatically
            bool isAutoplay = true;
            if (Utility.ReadAppRegistry("TVKing2", "auto_play") == "0")
                isAutoplay = false;
            if (isAutoplay)
            {
                tmAutoPlay.Enabled = true;
            }

            //set register form to the top
            fmReg.TopMost = true;

            //timer checking channel bandwidth is on
            tmBandwidth.Enabled = true;

            //download list of watchable channels
            DownloadCheckedList();

            if (server_config_ads == true)
            {
                tvkads = new TVKAdsRight(AppConst.ServerURL);
                tvkads.Show();
                timerRightBanner.Enabled = true;
            }

            //close startup flash 
            flashpop.Close();

            //get user harddrive id and submit to server
            systemuser.deFinishedGetSystemUser = new DeFinishedGetSystemUser(CallbackFinishedGetSystemUser);
            Thread threadSystemUser = new Thread(new ThreadStart(systemuser.RequestToServer));
            threadSystemUser.Start();
            timerSystemUser.Enabled = true;
            GetUserEmail();

            //put more text on title to indicate version type
            switch (AppConst.appType)
            {
                case RegisterType.Ads:
                    break;
                case RegisterType.Free:
                    break;
                case RegisterType.Hours:
                    break;
                case RegisterType.HoursDaily:
                    if (reg.IsRegister() == false)
                    {
                        labelVersion.Text += " - 10 Minutes FREE watching";
                        Graphics g = labelVersion.CreateGraphics();
                        labelVersion.Width = (int)g.MeasureString(labelVersion.Text, labelVersion.Font).Width;
                        g.Dispose();
                    }
                    break;
                case RegisterType.Trial:
                    break;
            }
        }


        public void CallbackFinishedGetSystemUser(string content)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallbackString(CallbackFinishedGetSystemUser), content);
                return;
            }
            if (content.IndexOf("#") > 0)
            {
                labelOnlineUser.Text = content.Split('#')[1];
                labelInstalledUser.Text = content.Split('#')[0];
                if (content.Split('#').Length > 2)
                {
                    labelTotalUser.Text = content.Split('#')[2];
                    labelTotalUser.Visible = true;
                }

                labelOnlineUser.Visible = true;
                labelInstalledUser.Visible = true;
            }
        }

        void lvStream_RatingClick(object sender, string id, string name)
        {
            RatingForm rf = new RatingForm(id, name);
            rf.Show();
        }

        void lvStream_DefaultClick(object sender)
        {
            RemoveDefaultChannel();
            lvStream.DefaultItem = "";
        }

        void cbCountry_IndexChanged(object sender, int newIndex)
        {
            if (cbCountry.SelectedIndex != 0)
            {
                cbCategory.SelectedIndex = 0;
                SearchQuery((string)cbCountry.SelectedItem, true, false);
            }
        }

        void tvkComboBox2_IndexChanged(object sender, int newIndex)
        {
            cbCountry.SelectedIndex = 0;
            SearchByCategory(cbCategory.SelectedIndex, (string)cbCategory.SelectedItem);
        }

        private void SearchByCategory(int index, string term)
        {
            if (index == 1)
            {
                SearchQuery("most watched", false, false, true, false);
            }
            else if (index == 2)
            {
                SearchQuery("last watched", false, false, false, true);
            }
            else if (index != 0)
            {

                SearchQuery(term, false, true);
            }
        }

        private void InitCheckVersion()
        {
            string res = fmUpdate.CheckVersion("1");
            UpdateActivity upac = new UpdateActivity(this, res);
        }

        public void AskUpdate()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DeCallback(AskUpdate));
                return;
            }
            if (MessageBox.Show("This version no longer supported. Please download and update new version.", "Update", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                Proc.StartInfo.FileName = AppConst.UPDATER_PATH;
                Proc.Start();
                this.Close();
                //this.Invoke(new DeCallback(AgreeUpdate));
            }
        }

        private void callbackUpdate()
        {
            fmupdate = new fmUpdate(this);
            fmupdate.Show();
        }

        private void LoadCountry()
        {
            RequestTVKServer req = new RequestTVKServer();
            string res = req.GetCountry();
            cbCountry.Invoke(new DeCallbackString(LoadCountryCallback), res);
        }

        private void LoadCountryCallback(string res)
        {
            string[] bls = res.Split('#');
            cbCountry.AddItem("More Countries");
            foreach (string bl in bls)
            {
                if (bl != "")
                    cbCountry.AddItem(bl);
            }
            cbCountry.SelectedIndex = 0;

            int margin = 15;
            int countryLeftMargin = labelFavorites.Left + labelFavorites.Width + margin;
            if (!cbCountry.ContainItem("India"))
            {
                labelIndia.Visible = false;
            }
            else
            {
                labelIndia.Left = countryLeftMargin;
                countryLeftMargin = countryLeftMargin + labelIndia.Width + margin;
            }
            if (!cbCountry.ContainItem("USA"))
            {
                labelUsa.Visible = false;
            }
            else
            {
                labelUsa.Left = countryLeftMargin;
                countryLeftMargin = countryLeftMargin + labelUsa.Width + margin;
            }
            if (!cbCountry.ContainItem("United Kingdom"))
            {
                labelUk.Visible = false;
            }
            else
            {
                labelUk.Left = countryLeftMargin;
                countryLeftMargin = countryLeftMargin + labelUk.Width + margin;
            }
            if (!cbCountry.ContainItem("Canada"))
            {
                labelCanada.Visible = false;
            }
            else
            {
                labelCanada.Left = countryLeftMargin;
                countryLeftMargin = countryLeftMargin + labelCanada.Width + margin;
            }
            if (!cbCountry.ContainItem("China"))
            {
                labelChina.Visible = false;
            }
            else
            {
                labelChina.Left = countryLeftMargin;
                countryLeftMargin = countryLeftMargin + labelChina.Width + margin;
            }
            if (!cbCountry.ContainItem("Germany"))
            {
                labelGermany.Visible = false;
            }
            else
            {
                labelGermany.Left = countryLeftMargin;
                countryLeftMargin = countryLeftMargin + labelGermany.Width + margin;
            }
            if (!cbCountry.ContainItem("Russia"))
            {
                labelRussia.Visible = false;
            }
            else
            {
                labelRussia.Left = countryLeftMargin;
                countryLeftMargin = countryLeftMargin + labelRussia.Width + margin;
            }

            cbCountry.Left = countryLeftMargin;

            pictureBoxYoutube.Left = cbCountry.Left + cbCountry.Width + 5;
            pictureBoxYoutube.Top = cbCountry.Top - 5;
        }

        public void AfterSubmitEmail()
        {
            MessageBox.Show("You have registered TVKing successfully. You can watch TV Streams for FREE now.", "System Notice");
        }

        public bool GetUserEmail()
        {
            string user_email = Utility.ReadAppRegistry("TVKing2", "email");
            if (user_email == "")
            {
                FormAskEmail fmEmail = new FormAskEmail();
                fmEmail.deAfterSubmitEmail = new DeAfterSubmitEmail(AfterSubmitEmail);
                fmEmail.TopMost = true;
                fmEmail.StartPosition = FormStartPosition.CenterScreen;
                fmEmail.Show();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LoadCategory()
        {
            RequestTVKServer req = new RequestTVKServer();
            string res = req.GetCategory();
            cbCategory.Invoke(new DeCallbackString(LoadCategoryCallback), res);
        }

        private void LoadCategoryCallback(string res)
        {
            string[] bls = res.Split('#');
            cbCategory.AddItem("Categories");
            cbCategory.AddItem("Most Watched");
            cbCategory.AddItem("Being Watched");
            foreach (string bl in bls)
            {
                if (bl != "")
                    cbCategory.AddItem(bl);
            }
            cbCategory.SelectedIndex = 0;
        }

        private void ServerResponseChannels(string res)
        {
            List<Channel> allList = Channel.ParseList(res);
            ServerResponseChannels(allList);
        }

        private void ServerResponseChannels(List<Channel> allList)
        {
            lvStream.Clear();
            lvWeb.Clear();

            streamList = new List<Channel>();
            webList = new List<Channel>();


            if (labelFavorites.ForeColor == Color.DodgerBlue)
                lvStream.SetBackgroundColor(Color.LightBlue);
            else
                lvStream.SetBackgroundColor(Color.Black);

            foreach (Channel i in allList)
            {
                if (i.is_stream == "1")
                    streamList.Add(i);
                else
                    webList.Add(i);

                AddChannel(i);
            }
            FinishedLoadingChannels();
        }

        private void FinishedLoadingChannels()
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallback(FinishedLoadingChannels));
                return;
            }

            bool isOK = false;
            while (true)
            {
                pbStartSearch.Visible = true;
                pbStopSearch.Visible = false;
                txtSearch.Enabled = true;
                isOK = true;
                lvStream.ShowWaiting(false);
                lvWeb.ShowWaiting(false);
                if (isOK == true)
                    break;
                else
                    Thread.Sleep(1000);
            }
        }

        private void AddChannel(Channel ch)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallbackChannel(AddChannel), ch);
                return;
            }

            string local_image = ch.image_url;
            string icon_key = ch.country.ToUpper();
            bool isShowLogo = true;
            if (Utility.ReadAppRegistry("TVKing2", "show_logo") == "0")
                isShowLogo = false;
            if (server_config_showlogo == false)
            {
                isShowLogo = false;
            }

            if (local_image != "" && isShowLogo == true)
            {
                string image_ext = local_image.Split('.')[local_image.Split('.').Length - 1];
                local_image = Utility.md5String(local_image);
                local_image = local_image + "." + image_ext;
                bool isDownloadable = true;

                if (File.Exists("logos\\" + local_image) == false && local_image != "")
                {
                    isDownloadable = Utility.DownloadFile(ch.image_url, "logos\\" + local_image);
                    if (isDownloadable == false && File.Exists("logos\\" + local_image) == true)
                    {
                        FileInfo f = new FileInfo("logos\\" + local_image);
                        long filesize = f.Length;
                        if (filesize == 0)
                        {
                            File.Delete("logos\\" + local_image);
                        }
                    }
                }
                if (isDownloadable == true)
                    icon_key = local_image;
                if (!flagList.Images.ContainsKey(icon_key) && icon_key != "")
                {
                    try
                    {
                        Image flag_image = Image.FromFile("logos\\" + icon_key);
                        flagList.Images.Add(icon_key, flag_image);
                        lvStream.SetImageList(flagList);
                        lvWeb.SetImageList(flagList);
                    }
                    catch (Exception ex)
                    {
                        File.Delete("logos\\" + icon_key);
                    }
                }
            }

            string chName = ch.name;
            if (chName.Length > 20)
                chName = chName.Substring(0, 20);

            if (ch.is_stream == "1")
                lvStream.AddItem(ch.id.ToString(), chName, icon_key, ch.rating);
            else
                lvWeb.AddItem(ch.id.ToString(), chName, icon_key, ch.rating);

            lbCountStream.Text = streamList.Count.ToString();
            lbCountWeb.Text = webList.Count.ToString();
        }

        private void SearchQuery(string key, bool country, bool category, bool most_watched = false, bool last_watched = false)
        {
            cur_key = key;
            cur_country = country;
            cur_category = category;
            cur_most_watched = most_watched;
            cur_last_watched = last_watched;

            searchingRequest.isStoped = true;
            searchingRequest = new RequestTVKServer();

            if (country)
                defaultCountry = key;

            lbListTitle.Text = key;

            lvStream.Clear();
            lvWeb.Clear();

            lvStream.ShowWaiting(true);
            lvWeb.ShowWaiting(true);
            pbStartSearch.Visible = false;
            pbStopSearch.Visible = true;
            txtSearch.Enabled = false;

            searchingRequest.search_key = key;
            searchingRequest.isCountrySearch = country;
            searchingRequest.isCategorySearch = category;
            searchingRequest.most_watched = most_watched;
            searchingRequest.last_watched = last_watched;
            searchingRequest.callbackRequest = new DeRequestTVKServer(ServerResponseChannels);
            Thread thSearch;
            thSearch = new Thread(new ThreadStart(searchingRequest.SearchAsys));
            thSearch.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SearchQuery(txtSearch.Text, false, false);
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CloseApp();
        }

        public void CloseApp()
        {
            this.Close();
        }

        public void ShowMessage(string mes, string title)
        {
            MessageBox.Show(mes, title);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (alwaysOnTopToolStripMenuItem.Checked == true)
            {
                this.TopMost = true;
            }
            else
            {
                this.TopMost = false;
            }
            toolStripMenuItem3.Checked = alwaysOnTopToolStripMenuItem.Checked;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem3.Checked == true)
            {
                this.TopMost = true;
            }
            else
            {
                this.TopMost = false;
            }
            alwaysOnTopToolStripMenuItem.Checked = toolStripMenuItem3.Checked;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (currentPlayingChannel == null)
                return;
            if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_WMP)
            {
                if (player_wmp.status.IndexOf("Playing") >= 0)
                    player_wmp.fullScreen = true;
            }
            else
            {
                MessageBox.Show("Fullscreen is not support for this video.");
            }
        }

        private void PlaySelectedStream(Channel ch)
        {
            timerSeeking.Enabled = false;
            HideYoutubeControl();
            playingYoutube = false;
            if (isStop == true)
            {
                tvkTrialScreen1.Visible = true;
                pbErrorBG.Visible = true;
                MessageBox.Show("Trial time is end. Please register to continue.", "Trial Version", MessageBoxButtons.OK);
                return;
            }
            currentPlayingChannel = ch;

            galleryControlYoutube.Visible = false;

            barText.Text = "Loading channel " + ch.name + " ...";
            barText.Visible = true;
            barProgress.Visible = true;
            pnStatus.Visible = true;
            currentPlayingChannel = ch;
            isShownAds = false;

            pbFullScreen.Visible = true;

            pbErrorBG.Visible = false;
            lbErrorPlayer.Visible = false;

            Console.WriteLine("web: " + ch.web);
            Console.WriteLine("stream: " + ch.stream);

            StopPlaying();
            player_wmp.Visible = false;
            player_flash.Visible = false;

            if (favList.IsInFav(ch) == true)
            {
                pbRevFav.Visible = true;
                pbAddFav.Visible = false;
            }
            else
            {
                pbRevFav.Visible = false;
                pbAddFav.Visible = true;
            }

            if (ch.channel_type == AppConst.STREAM_TYPE_FLASH)
            {
                player_flash.Visible = true;
                player_flash.Movie = ch.stream;//.Replace("[divider]", "#").Replace("[divider2]", ";");
                player_flash.Play();
            }
            else if (ch.channel_type == AppConst.STREAM_TYPE_WMP)
            {
                player_wmp.Visible = true;
                player_wmp.URL = ch.stream;
            }
            //else
            //{
            //    flash.Visible = false;
            //    player.Visible = true;
            //    wplayer.Visible = false;
            //    wplayer.URL = "";
            //    flash.Movie = "http://";
            //    player.playlistClear();
            //    string url = ch.stream;
            //    player.addTarget(url, null, AXVLC.VLCPlaylistMode.VLCPlayListReplace, 0);
            //    player.play();
            //}
        }

        private void lvStream_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            string selectedId = lvStream.SelectedId();
            foreach (Channel ch in streamList)
            {
                if (ch.id == selectedId)
                {
                    PlaySelectedStream(ch);
                    break;
                }
            }
        }

        private void player_MediaPlayerEncounteredError(object sender, EventArgs e)
        {
            ChannelError();
        }

        string last_sending_success = ""; //prevent multiple submit success signal
        private void LoadingChannelSuccess(bool is_youtube = false)
        {
            isVideoPlaying = true;
            if (is_youtube == false)
            {
                if (last_sending_success != currentPlayingChannel.id)
                {
                    last_sending_success = currentPlayingChannel.id;
                    RequestTVKServer sv = new RequestTVKServer();
                    sv.ChannelChecked(currentPlayingChannel.id);
                    txtTitle.Text = currentPlayingChannel.name;
                }
            }
            //if (currentPlayingChannel.channel_type != "4" && currentPlayingChannel.channel_type != "1")
            //    isLoadVideo = true;
            barText.Visible = false;
            barProgress.Visible = false;
            pnStatus.Visible = false;
            pbErrorBG.Visible = false;

            txtTitle.Visible = true;
            //txtDescription.Visible = true;
            //txtDescription.Text = currentPlayingChannel.description;
            pbMinimize.Visible = true;

            if (isShownAds == false)
            {
                switch (AppConst.appType)
                {
                    case RegisterType.Ads:
                        RegisterCheck reg = new RegisterCheck();
                        if (reg.IsRegister() == false)
                        {
                            ShowAdsOnChannel();
                        }
                        break;
                    case RegisterType.Free:
                        break;
                    case RegisterType.Hours:
                        break;
                    case RegisterType.HoursDaily:
                        break;
                    case RegisterType.Trial:
                        break;
                }
            }
        }

        private void PauseCurrentChannel()
        {
            if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_WMP)
                player_wmp.Ctlcontrols.stop();
            else
                if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_FLASH)
                    player_flash.Stop();
            //else
            //    player.pause();
        }

        private void PlayCurrentChannel()
        {

            if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_WMP)
            {
                player_wmp.URL = currentPlayingChannel.stream;
                player_wmp.Visible = true;
                player_wmp.Ctlcontrols.play();
            }
            else
                if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_FLASH)
                {
                    player_flash.Movie = currentPlayingChannel.stream;//.Replace("[divider]", "#").Replace("[divider2]", ";");
                    player_flash.Visible = true;
                    player_flash.Play();
                }
            //else
            //{
            //    player.Visible = true;
            //    player.play();
            //}
        }

        private void ShowAdsOnChannel()
        {
            isShownAds = true;
            adsMain2.Visible = true;
            adsMain2.SetTimer(AppConst.adsCountdown, this);
            PauseCurrentChannel();
        }

        public void HideAdsOnChannel()
        {
            adsMain2.Visible = false;
            PlayCurrentChannel();
        }

        //private void player_MediaPlayerPlaying(object sender, EventArgs e)
        //{
        //    LoadingChannelSuccess();
        //}

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tvking.tv");
        }

        private void lvWeb_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RegisterCheck reg = new RegisterCheck();
            if (reg.IsRegister() == false)
            {
                switch (AppConst.appType)
                {
                    case RegisterType.Ads:
                        lbRegisterText.Text = "Please register to watch TV Without Ads";
                        lbLeftDays.Text = "";
                        label10.Text = "";
                        adv = new fmAdvertisement();
                        adv.Show();
                        tmAdvMoving.Enabled = true;
                        return;
                    case RegisterType.Free:
                        break;
                    case RegisterType.Hours:
                        reg.SetTimeUsed();
                        int timeLeft = reg.GetTimeLeft();
                        if (timeLeft < 0)
                        {
                            fmReg.Show();
                        }
                        return;
                    case RegisterType.HoursDaily:
                        if (reg.IsRegister() == false && reg.IsExpired() == false)
                        {
                            isStop = true;
                            fmReg.Show();
                            tvkTrialScreen1.Visible = true;
                            pbErrorBG.Visible = true;
                            return;
                        }
                        break;
                    case RegisterType.Trial:
                        if (reg.IsExpired() == true)
                        {
                            isStop = true;
                            fmReg.Show();
                            tvkTrialScreen1.Visible = true;
                            pbErrorBG.Visible = true;
                        }
                        lbLeftDays.Text = reg.GetDayLeft() + " Days";
                        return;
                }
            }
            string selectedId = lvWeb.SelectedId();
            foreach (Channel ch in webList)
            {
                if (ch.id.ToString() == selectedId)
                {
                    System.Diagnostics.Process.Start(ch.web);
                    break;
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            isVideoPlaying = false;
            //if (isLoadVideo == true)
            //    player.stop();
            if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_FLASH)
                player_flash.Movie = "http://";
            player_wmp.URL = "";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            PlayCurrentChannel();
        }

        private void StopPlaying()
        {
            isVideoPlaying = false;
            if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_FLASH)
                player_flash.Movie = "http://";
            player_wmp.URL = "";
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            StopPlaying();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (playingYoutube == true)
            {
                if (_youtube_videos.Count != 0)
                {
                    favList.AddFavoriteYoutube(_youtube_videos[current_youtubeid]);
                }
            }
            else
            {
                favList.AddFavorite(currentPlayingChannel);
                pbAddFav.Visible = false;
                pbRevFav.Visible = true;
                if (labelFavorites.ForeColor == Color.DodgerBlue)
                    label1_Click_1(labelFavorites, null);
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            favList.DeleteFavorite(currentPlayingChannel);
            pbAddFav.Visible = true;
            pbRevFav.Visible = false;
            if (labelFavorites.ForeColor == Color.DodgerBlue)
                label1_Click_1(labelFavorites, null);
        }

        private void tmAdvMoving_Tick(object sender, EventArgs e)
        {
            try
            {
                if (adv == null)
                    return;
                adv.Left = this.Left + this.Width;
                adv.Top = this.Top;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.Message);
                Utility.WriteLog(ex.StackTrace);
            }
        }

        
        private void flash_OnReadyStateChange(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_OnReadyStateChangeEvent e)
        {
            if (e.newState.ToString() == last_wp_status)
                return;
            last_wp_status = e.newState.ToString();
            //Console.WriteLine(e.newState);
            if (e.newState == 4)
            {
                LoadingChannelSuccess();
            }
        }

        private void lbRegisterText_Click(object sender, EventArgs e)
        {
            fmReg.Show();
            System.Diagnostics.Process.Start("http://www.tvking.tv/register");
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmReg.Show();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmUpdate upd = new fmUpdate(this);
            upd.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmSetting settings = new fmSetting();
            settings.Show();
        }
        bool has_submitted_channel = false;
        string last_wp_status = "";
        private void wplayer_StatusChange(object sender, EventArgs e)
        {
            if (player_wmp.status == last_wp_status)
                return;
            last_wp_status = player_wmp.status;
            //Console.WriteLine("WMP Status: " + player_wmp.status);
            if (player_wmp.status.Trim() == "")
            {
                has_submitted_channel = false;
            }
            if (player_wmp.status.IndexOf("Playing") >= 0 && has_submitted_channel == false)
            {
                has_submitted_channel = true;
                if (playingYoutube == true)
                {
                    if (timerSeeking.Enabled == false)
                        timerSeeking.Enabled = true;
                    LoadingChannelSuccess(true);
                }
                else
                    LoadingChannelSuccess();
            }
            else if (player_wmp.status.IndexOf("Stopped") >= 0)
            {
                //if (playingYoutube == true && BufferingYoutube == false)
                ////if (playingYoutube == true)
                //{
                //    timerSeeking.Enabled = false;
                //    current_youtubeid++;
                //    BufferingYoutube = true;
                //    play_youtube_channel();
                //}
                //else
                //    if (playingYoutube == true && BufferingYoutube == true)
                //    {
                //        play_youtube_channel();
                //    }
            }
            else if (player_wmp.status.IndexOf("Buffering") >= 0)
            {
                //BufferingYoutube = false;

                barText.Visible = true;
                barProgress.Visible = true;
                pnStatus.Visible = true;
            }
            else if (player_wmp.status.IndexOf("Playing") >= 0)
            {
                barText.Visible = false;
                barProgress.Visible = false;
                pnStatus.Visible = false;
            }
        }


        private void wplayer_MediaError(object sender, AxWMPLib._WMPOCXEvents_MediaErrorEvent e)
        {
            if (playingYoutube == true)
                return;
            ChannelError();
        }

        private void ChannelError()
        {
            barText.Visible = false;
            barProgress.Visible = false;
            pnStatus.Visible = false;

            txtTitle.Visible = true;
            txtTitle.Text = currentPlayingChannel.name;

            pbErrorBG.Visible = true;
            lbErrorPlayer.Visible = true;

            RequestTVKServer sv = new RequestTVKServer();
            sv.ChannelError(currentPlayingChannel.id);
        }

        private void tmAutoPlay_Tick(object sender, EventArgs e)
        {
            try
            {
                if (lvStream.CountItems() > 0)
                {
                    tmAutoPlay.Enabled = false;
                    Channel def = new Channel();

                    def.name = Utility.ReadAppRegistry("TVKing2", "def_name");
                    def.stream = Utility.ReadAppRegistry("TVKing2", "def_stream");
                    def.channel_type = Utility.ReadAppRegistry("TVKing2", "def_type");
                    lvStream.DefaultItem = Utility.ReadAppRegistry("TVKing2", "def_id");

                    currentPlayingChannel = def;

                    player_flash.Visible = false;
                    //player.Visible = false;
                    player_wmp.Visible = false;

                    if (def.stream == "" || def.stream == null)
                    {
                        lvStream.SelectedItem(0);
                        string selectedId = lvStream.SelectedId();
                        foreach (Channel ch in streamList)
                        {
                            if (ch.id == selectedId)
                            {
                                PlaySelectedStream(ch);
                                break;
                            }
                        }
                    }
                    else
                    {
                        txtTitle.Text = def.name;
                        PlayCurrentChannel();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.Message);
                Utility.WriteLog(ex.StackTrace);
            }
        }

        private void tmAds_Tick(object sender, EventArgs e)
        {
            try
            {
                HideAdsOnChannel();

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.Message);
                Utility.WriteLog(ex.StackTrace);
            }
        }

        private void MainApp_Resize(object sender, EventArgs e)
        {
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20)); // adjust these parameters to get the lookyou want.

            //adsMain2.Left = (wplayer.Width - adsMain2.Width) / 2 + wplayer.Left;
            //pbFullScreen.Refresh();
            //pbMinimize.Refresh();
            //pbAddFav.Refresh();
            //pbRevFav.Refresh();
            //txtTitle.Refresh();
            this.Refresh();
            LocateControls();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            SwitchToMiniMode();
        }

        private void lbMini_Click(object sender, EventArgs e)
        {
            SwitchToNormalMode();
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fmSetting settings = new fmSetting();
            settings.Show();
        }

        private void registerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fmReg.Show();
        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string res = fmUpdate.CheckVersion("0");
            UpdateActivity upac = new UpdateActivity(this, res);
            //fmupdate = new fmUpdate(this);
            //fmupdate.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            lbStream.ForeColor = Color.DodgerBlue;
            lbWeb.ForeColor = Color.SlateGray;
            lvWeb.Visible = false;
            lvStream.Visible = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            lbStream.ForeColor = Color.SlateGray;
            lbWeb.ForeColor = Color.DodgerBlue;
            lvWeb.Visible = true;
            lvStream.Visible = false;
        }

        private void pbStopSearch_Click(object sender, EventArgs e)
        {
            searchingRequest.isStoped = true;
            pbStopSearch.Visible = false;
            pbStartSearch.Visible = true;
        }

        private void MainApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            lvStream.isStopping = true;
            lvWeb.isStopping = true;
            searchingRequest.isStoped = true;
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void iconClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconMinimize_MouseHover(object sender, EventArgs e)
        {
            iconMinimize.Image = global::TVKing2.Properties.Resources.icon_minimize;
        }

        private void iconMinimize_MouseLeave(object sender, EventArgs e)
        {
            iconMinimize.Image = global::TVKing2.Properties.Resources.icon_minimize_n;
        }

        private void iconMaximize_MouseHover(object sender, EventArgs e)
        {
            iconMaximize.Image = global::TVKing2.Properties.Resources.icon_maximize;
        }

        private void iconMaximize_MouseLeave(object sender, EventArgs e)
        {
            iconMaximize.Image = global::TVKing2.Properties.Resources.icon_maximize_n;
        }

        private void iconClose_MouseHover(object sender, EventArgs e)
        {
            iconClose.Image = global::TVKing2.Properties.Resources.icon_close;
        }

        private void iconClose_MouseLeave(object sender, EventArgs e)
        {
            iconClose.Image = global::TVKing2.Properties.Resources.icon_close_n;
        }

        private void MainApp_MouseDown(object sender, MouseEventArgs e)
        {
            isMoving = true;
            currentX = e.X;
            currentY = e.Y;
        }

        private void MainApp_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void MainApp_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                this.Top = this.Top + (e.Y - currentY);
                this.Left = this.Left + (e.X - currentX);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            searchingRequest.isStoped = true;
            lvStream.Clear();
            lvWeb.Clear();
            lbCountStream.Text = "0";
            lbCountWeb.Text = "0";
            SelectLabel(sender);
            favList.LoadFavorite();
            lbListTitle.Text = "Favorites";
            ServerResponseChannels(favList.favList);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isMoving = true;
            currentX = e.X;
            currentY = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                this.Height = this.Height + (e.Y - currentY);
                this.Width = this.Width + (e.X - currentX);
            }
        }

        private void SelectLabel(object sender)
        {
            labelFavorites.ForeColor = Color.DarkGray;
            labelUsa.ForeColor = Color.DarkGray;
            labelIndia.ForeColor = Color.DarkGray;
            labelUk.ForeColor = Color.DarkGray;
            labelCanada.ForeColor = Color.DarkGray;
            labelChina.ForeColor = Color.DarkGray;
            labelGermany.ForeColor = Color.DarkGray;
            labelRussia.ForeColor = Color.DarkGray;

            //label1.Font = new Font(label1.Font, FontStyle.Bold);
            labelUsa.Font = new Font(labelUsa.Font, FontStyle.Bold);
            labelIndia.Font = new Font(labelIndia.Font, FontStyle.Bold);
            labelUk.Font = new Font(labelUk.Font, FontStyle.Bold);
            labelCanada.Font = new Font(labelCanada.Font, FontStyle.Bold);
            labelChina.Font = new Font(labelChina.Font, FontStyle.Bold);
            labelGermany.Font = new Font(labelGermany.Font, FontStyle.Bold);
            labelRussia.Font = new Font(labelRussia.Font, FontStyle.Bold);

            Label curL = (Label)sender;
            curL.ForeColor = Color.DodgerBlue;
            if (curL.Text != "Favorites")
                curL.Font = new Font(curL.Font, FontStyle.Bold);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SearchQuery("India", true, false);
            SelectLabel(sender);
        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            SearchQuery("USA", true, false);
            SelectLabel(sender);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SearchQuery("United Kingdom", true, false);
            SelectLabel(sender);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            SearchQuery("Canada", true, false);
            SelectLabel(sender);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            SearchQuery("China", true, false);
            SelectLabel(sender);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            SearchQuery("Germany", true, false);
            SelectLabel(sender);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            SearchQuery("Russia", true, false);
            SelectLabel(sender);
        }

        private void tvkLabel2_Click(object sender, EventArgs e)
        {
            fmReg.Show();
        }

        private void tvkPictureBox2_Click(object sender, EventArgs e)
        {
            fmReg.Show();
        }

        private void tvkLabel1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(tvkLabel1.Location.X + this.Location.X, tvkLabel1.Location.Y + this.Location.Y);
        }

        private void tvkPictureBox1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(tvkPictureBox1.Location.X + this.Location.X, tvkPictureBox1.Location.Y + this.Location.Y);
        }

        private void tvkLabel1_MouseHover(object sender, EventArgs e)
        {
            HoverLabel(sender);
        }

        private void HoverLabel(object sender)
        {
            TVKing2.TVKLabel obj = (TVKing2.TVKLabel)sender;
            obj.ForeColor = Color.DodgerBlue;
        }

        private void LeaveLabel(object sender)
        {
            TVKing2.TVKLabel obj = (TVKing2.TVKLabel)sender;
            obj.ForeColor = Color.DarkGray;
        }

        private void tvkLabel1_MouseLeave(object sender, EventArgs e)
        {
            LeaveLabel(sender);
        }

        private void tvkLabel2_MouseHover(object sender, EventArgs e)
        {
            HoverLabel(sender);
        }

        private void tvkLabel2_MouseLeave(object sender, EventArgs e)
        {
            LeaveLabel(sender);
        }

        private void txtSearch_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtSearch.Text == "Search")
                txtSearch.Text = "";
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                SearchQuery(txtSearch.Text, false, false);
        }

        private void tvkPictureBox3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void mnAddFavorites_Click(object sender, EventArgs e)
        {
            if (lvStream.SelectedId() == "")
            {
                MessageBox.Show("Please choose a channel to add to Favorites", "TVKing Error Adding favorite");
                return;
            }

            favList.AddFavorite(streamList[lvStream.SelectedIndex]);
            if (currentPlayingChannel.id == streamList[lvStream.SelectedIndex].id)
            {
                pbAddFav.Visible = false;
                pbRevFav.Visible = true;
            }
            if (labelFavorites.ForeColor == Color.DodgerBlue)
                label1_Click_1(labelFavorites, null);
        }

        private void contextChannels_Opening(object sender, CancelEventArgs e)
        {
            if (lvStream.SelectedIndex <= -1 ||
            streamList.Count < lvStream.SelectedIndex)
            {
                e.Cancel = true;
                return;
            }
            if (favList.IsInFav(streamList[lvStream.SelectedIndex]))
            {
                mnRemoveFromFav.Visible = true;
                mnAddFavorites.Visible = false;
            }

            else
            {
                mnRemoveFromFav.Visible = false;
                mnAddFavorites.Visible = true;
            }
        }

        private void mnRemoveFromFav_Click(object sender, EventArgs e)
        {
            if (lvStream.SelectedId() == "")
            {
                MessageBox.Show("Please choose a channel to remove from Favorites", "TVKing Error Removing favorite");
                return;
            }

            favList.DeleteFavorite(streamList[lvStream.SelectedIndex]);
            if (currentPlayingChannel.id == streamList[lvStream.SelectedIndex].id)
            {
                pbAddFav.Visible = true;
                pbRevFav.Visible = false;
            }

            if (labelFavorites.ForeColor == Color.DodgerBlue)
                label1_Click_1(labelFavorites, null);
        }

        private void mnSetToDefault_Click(object sender, EventArgs e)
        {
            if (lvStream.SelectedId() == "")
            {
                MessageBox.Show("Please choose a channel to set default", "TVKing Error setting default");
                return;
            }
            Channel def = streamList[lvStream.SelectedIndex];
            Utility.WriteAppRegistry("TVKing2", "def_name", def.name);
            Utility.WriteAppRegistry("TVKing2", "def_stream", def.stream);
            Utility.WriteAppRegistry("TVKing2", "def_type", def.channel_type);
            Utility.WriteAppRegistry("TVKing2", "def_id", def.id);
            lvStream.DefaultItem = def.id;
        }

        private void RemoveDefaultChannel()
        {
            Utility.WriteAppRegistry("TVKing2", "def_name", "");
            Utility.WriteAppRegistry("TVKing2", "def_stream", "");
            Utility.WriteAppRegistry("TVKing2", "def_type", "");
            Utility.WriteAppRegistry("TVKing2", "def_id", "");
        }

        private void removeDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveDefaultChannel();
            lvStream.DefaultItem = "";
        }

        private void tmBandwidth_Tick(object sender, EventArgs e)
        {
            try
            {
                if (currentPlayingChannel == null)
                    return;
                if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_WMP)
                {
                    pbBandwidth.Visible = true;
                    int bw = player_wmp.network.bandWidth;
                    if (bw > 250000)
                        pbBandwidth.Image = global::TVKing2.Properties.Resources.bandwith5;
                    else
                        if (bw > 200000)
                            pbBandwidth.Image = global::TVKing2.Properties.Resources.bandwith4;
                        else
                            if (bw > 150000)
                                pbBandwidth.Image = global::TVKing2.Properties.Resources.bandwith3;
                            else
                                if (bw > 100000)
                                    pbBandwidth.Image = global::TVKing2.Properties.Resources.bandwith2;
                                else
                                    if (bw <= 100000)
                                        pbBandwidth.Image = global::TVKing2.Properties.Resources.bandwith1;
                    pbBandwidth.SizeMode = PictureBoxSizeMode.AutoSize;
                    return;
                }
                pbBandwidth.Visible = false;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.Message);
                Utility.WriteLog(ex.StackTrace);
            }
        }

        private void DownloadCheckedList()
        {
            RequestTVKServer sv = new RequestTVKServer();
            DeRequestTVKServer callback = new DeRequestTVKServer(DownloadCheckedListCallback);
            sv.DownloadSync("?action=checked_channels", callback);
        }

        private void DownloadCheckedListCallback(string res)
        {
            string[] checkedList = res.Split('#');
            lvStream.checkedList = checkedList;
        }

        private void tmPlaying_Tick(object sender, EventArgs e)
        {
            try
            {
                RegisterCheck reg = new RegisterCheck();
                reg.callback = this;

                switch (AppConst.appType)
                {
                    case RegisterType.Ads:
                        break;
                    case RegisterType.Free:
                        break;
                    case RegisterType.Hours:
                        if (reg.IsRegister() == false)
                        {
                            reg.SetTimeUsed();
                            int timeLeft = reg.GetTimeLeft();
                            barTrialHours.Maximum = AppConst.TrialHours * 60;
                            if (timeLeft >= 0)
                                barTrialHours.Value = timeLeft;
                            if (isVideoPlaying == true)
                            {
                                if (timeLeft < 0)
                                {
                                    isVideoPlaying = false;
                                    //if (isLoadVideo == true)
                                    //    player.stop();
                                    if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_FLASH)
                                        player_flash.Movie = "http://";
                                    player_wmp.URL = "";
                                    fmReg.Show();
                                }
                            }
                        }
                        break;
                    case RegisterType.HoursDaily:
                        if (reg.IsRegister() == false)
                        {
                            reg.SetTimeUsedHourDaily();
                            if (reg.IsExpired() == true)
                            {
                                if (isVideoPlaying == true)
                                {
                                    isVideoPlaying = false;
                                    if (currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_FLASH)
                                        player_flash.Movie = "http://";
                                    player_wmp.URL = "";
                                    fmReg.Show();
                                }
                            }
                        }

                        break;
                    case RegisterType.Trial:
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.Message);
                Utility.WriteLog(ex.StackTrace);
            }
        }

        private void setAsDefaultCountryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utility.WriteAppRegistry("TVKing2", "default_country", defaultCountry);
            MessageBox.Show(defaultCountry + " is set as default country", "TVKing Notice", MessageBoxButtons.OK);
        }

        private void labelReportViolation_Click(object sender, EventArgs e)
        {
            if (isVideoPlaying == true)
            {
                ViolateReport vp = new ViolateReport(Utility.ReadAppRegistry("TVKing2", "email"), currentPlayingChannel.name);
                vp.Show();
            }
            else
            {
                MessageBox.Show("Please choose a channel to report", "Violation Report");
            }
        }

        private void pictureBox2_Click_2(object sender, EventArgs e)
        {
            if (cur_key != "")
            {
                SearchQuery(cur_key, cur_country, cur_category, cur_most_watched, cur_last_watched);
            }
        }

        private void timerRightBanner_Tick(object sender, EventArgs e)
        {
            try
            {
                tvkads.Top = this.Top;
                tvkads.Left = this.Left + this.Width - 1;
                tvkads.SetHeight(this.Height);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.Message);
                Utility.WriteLog(ex.StackTrace);
            }
        }

        private void MainApp_Move(object sender, EventArgs e)
        {
            if (tvkads != null)
            {
                tvkads.TopMost = true;
                tvkads.TopMost = false;
            }
        }

        private void timerSystemUser_Tick(object sender, EventArgs e)
        {
            try
            {
                if (currentPlayingChannel != null)
                {
                    systemuser.PlayingChannelId = currentPlayingChannel.id;
                }
                else
                {
                    systemuser.PlayingChannelId = "0";
                }
                Thread threadSystemUser = new Thread(new ThreadStart(systemuser.RequestToServer));
                threadSystemUser.Start();
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.Message);
                Utility.WriteLog(ex.StackTrace);
            }
        }

        private void label1_Click_2(object sender, EventArgs e)
        {
            fmTellFriends tyf = new fmTellFriends();
            tyf.Show();
        }

        private void pictureBox3_Click_2(object sender, EventArgs e)
        {
            if (isVideoPlaying == true)
            {
                ViolateReport vp = new ViolateReport(Utility.ReadAppRegistry("TVKing2", "email"), currentPlayingChannel.name);
                vp.Show();
            }
            else
            {
                MessageBox.Show("Please choose a channel to report", "Violation Report");
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (isVideoPlaying == true)
            {
                if (MessageBox.Show(this, "Do you want to claim this is broken stream?", "Broken Stream Report", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ChannelError();
                    MessageBox.Show("Thank you for your report", "Broken Stream Report");
                }
            }
            else
            {
                MessageBox.Show("Please choose a channel to report", "Broken Stream Report");
            }
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            fmTellFriends tyf = new fmTellFriends();
            tyf.Show();
        }

        private void submitChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSubmitUrl fmSub = new FormSubmitUrl();
            fmSub.Countries = cbCountry.GetItems();
            fmSub.Show();
        }

        private void playYoutubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormYoutube fmyoutube = new FormYoutube();
            fmyoutube.callback = this;
            fmyoutube.Show();
        }

        public void callback_choose_youtube_object(int i)
        {
            current_youtubeid = i;
            play_youtube_channel();
            galleryControlYoutube.Visible = false;
        }

        private void play_youtube_channel()
        {
            ShowYoutubeControl();
            if (current_youtubeid >= max_youtubeid)
            {
                return;
            }
            if (_youtube_videos.Count < current_youtubeid)
                return;
            string title = "";
            if (Utility.HtmlDecode(_youtube_videos[current_youtubeid].title).Length > 35)
            {
                title = Utility.HtmlDecode(_youtube_videos[current_youtubeid].title).Substring(0, 34);
            }
            else
                title = Utility.HtmlDecode(_youtube_videos[current_youtubeid].title);
            barText.Text = "Loading channel " + title + " ...";
            barText.Visible = true;
            barProgress.Visible = true;
            pnStatus.Visible = true;
            isShownAds = false;

            pbFullScreen.Visible = true;

            pbErrorBG.Visible = false;
            lbErrorPlayer.Visible = false;

            WebclientX client = new WebclientX();
            string youtubeid = _youtube_videos[current_youtubeid].youtube_url;
            youtubeid = Utility.SimpleRegexSingle("v=([0-9a-zA-Z_-]*)", youtubeid, 1);
            txtTitle.Text = title;
            string youtubeinfo = client.GetMethod("http://www.youtube.com/get_video_info?video_id=" + youtubeid);
            if (youtubeinfo.IndexOf("errorcode=150") > 0)
            {
                youtubeinfo = client.GetMethod("http://www.youtube.com/watch?v=" + youtubeid);
                youtubeinfo = Utility.SimpleRegexSingle("flashvars=\"([^\"]*)\"", youtubeinfo, 1);
            }
            string content = Utility.SimpleRegexSingle("fmt_stream_map=([^&]+)", youtubeinfo, 1);
            content = Utility.URLDecode(content);
            List<string> video_links = Utility.SimpleRegex(@"url=.*?(?=type=)[^,]*", content, 0);
            string youtube_mp4_file = "";
            foreach (string vl in video_links)
            {
                if (vl.IndexOf("video%2Fmp4") > 0)
                {
                    youtube_mp4_file = Utility.URLDecode(Utility.SimpleRegexSingle("url=([^&]*)", vl, 1));
                }
            }
            if (youtube_mp4_file != "")
            {
                isVideoPlaying = false;
                if (currentPlayingChannel != null && currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_FLASH)
                    player_flash.Movie = "http://";

                player_wmp.Visible = true;
                player_flash.Visible = false;
                player_flash.Movie = "http://";
                player_wmp.URL = youtube_mp4_file;
                playingYoutube = true;
                //timerFixWplayerBug.Enabled = true;
            }
            else
            {
                current_youtubeid++;
                play_youtube_channel();
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            timerSeeking.Enabled = false;
            FormYoutube fmyoutube = new FormYoutube();
            fmyoutube.callback = this;
            fmyoutube.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void ShowYoutubeControl()
        {
            buttonNextYoutube.Visible = true;
            buttonBackYoutube.Visible = true;
            tvkSeeking.Visible = true;
        }

        private void HideYoutubeControl()
        {
            buttonNextYoutube.Visible = false;
            buttonBackYoutube.Visible = false;
            tvkSeeking.Visible = false;
        }


        public void FormFinishedSearch(List<YoutubeObject> youtube_videos)
        {
            HideYoutubeControl();
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallbackYoutubeList(FormFinishedSearch), youtube_videos);
                return;
            }
            isVideoPlaying = false;
            if (currentPlayingChannel != null && currentPlayingChannel.channel_type == AppConst.STREAM_TYPE_FLASH)
                player_flash.Movie = "http://";
            player_wmp.URL = "";

            max_youtubeid = youtube_videos.Count;
            _youtube_videos = youtube_videos;
            galleryControlYoutube.Enabled = false;
            //GalleryItemGroup gal_group = new GalleryItemGroup();
            galleryControlYoutube.Gallery.Groups[0].Items.Clear();
            for (int i = 0; i < youtube_videos.Count; i++)
            {
                Image gal_pic = Utility.DownloadImage(youtube_videos[i].image);
                string title = Utility.HtmlDecode(youtube_videos[i].title);
                if (title.Length > 19)
                {
                    title = title.Substring(0, 19) + "...";
                }
                string desc = Utility.HtmlDecode(youtube_videos[i].description);
                if (desc.Length > 22)
                {
                    desc = desc.Substring(0, 22) + "...";
                }
                GalleryItem item = new GalleryItem(gal_pic, title, desc);
                item.Tag = i;
                galleryControlYoutube.Gallery.Groups[0].Items.Add(item);
            }
            GalleryItem item_lm = new GalleryItem(null, "Click to load more", "");
            item_lm.Tag = "load_more";
            galleryControlYoutube.Gallery.Groups[0].Items.Add(item_lm);
            //galleryControlYoutube.Gallery.Groups.Add(gal_group);

            galleryControlYoutube.Visible = true;

            //if (Utility.ReadAppRegistry("TVKing2", "not_show_codec") != "1")
            //{
            //    FormInstallCodec codec_note = new FormInstallCodec();
            //    codec_note.Show();
            //}
            galleryControlYoutube.Enabled = true;
            //youtube_list_form.youtube_videos = youtube_videos;
            //youtube_list_form.ReLoadLoadChannel();
            //youtube_list_form.deClickYoutubeChannel = new DeClickYoutubeChannel(callback_choose_youtube_object);
            //youtube_list_form.Visible = true;

        }

        private void tvkTrialScreen1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (current_youtubeid <= 1)
            {
                current_youtubeid = 0;
            }
            else
            {
                current_youtubeid = current_youtubeid - 2;
            }
            play_youtube_channel();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (current_youtubeid >= max_youtubeid - 1)
            {
                current_youtubeid = max_youtubeid - 1;
            }
            else
            {
                current_youtubeid++;
            }
            play_youtube_channel();
        }

        private void galleryControlGallery1_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            if (e.Item.Tag.ToString() == "load_more")
            {
                YoutubeSearch ys = new YoutubeSearch();
                ys.callback = this;
                ys.ContinueSearching();
                //System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(ys.ContinueSearching));
                //th.Start();
            }
            else
                callback_choose_youtube_object((int)e.Item.Tag);
        }

        public void IYS_BeginSearching()
        {
        }

        public void IYS_ProgressSearching(int percent)
        {
        }
        delegate void DeFinishedSearching(List<YoutubeObject> youtube_videos);
        public void IYS_FinishedSearching(List<YoutubeObject> youtube_videos)
        {
            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new DeFinishedSearching(IYS_FinishedSearching), youtube_videos);
            //    return;
            //}
            //GalleryItemGroup gal_group = galleryControlYoutube.Gallery.Groups[0];
            max_youtubeid += youtube_videos.Count;
            _youtube_videos.AddRange(youtube_videos);
            galleryControlYoutube.Gallery.Groups[0].Items.RemoveAt(galleryControlYoutube.Gallery.Groups[0].Items.Count - 1);

            for (int i = 0; i < youtube_videos.Count; i++)
            {
                Image gal_pic = Utility.DownloadImage("http://" + youtube_videos[i].image);
                string title = Utility.HtmlDecode(youtube_videos[i].title);
                if (title.Length > 20)
                {
                    title = title.Substring(0, 20) + "...";
                }
                string desc = Utility.HtmlDecode(youtube_videos[i].description);
                if (desc.Length > 25)
                {
                    desc = desc.Substring(0, 25) + "...";
                }
                GalleryItem item = new GalleryItem(gal_pic, title, desc);
                item.Tag = i;
                galleryControlYoutube.Gallery.Groups[0].Items.Add(item);
            }
            GalleryItem item_lm = new GalleryItem(null, "Click to load more", "");
            item_lm.Tag = "load_more";
            galleryControlYoutube.Gallery.Groups[0].Items.Add(item_lm);
            //galleryControlYoutube.Gallery.Groups.Clear();
            //galleryControlYoutube.Gallery.Groups.Add(gal_group);
        }

        public void IYS_Stop()
        {
        }

        private void timerFixWplayerBug_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("WMP Status: " + wplayer.status);
            //if (playingYoutube && wplayer.status.IndexOf("Playing") == -1 && wplayer.status.IndexOf("Connecting...") == -1)
            //    play_youtube_channel();
            //else
            //    timerFixWplayerBug.Enabled = false;
        }

        private void timerSeeking_Tick(object sender, EventArgs e)
        {
            try
            {
                double duration = player_wmp.currentMedia.duration;
                IWMPControls control = (WMPLib.IWMPControls3)player_wmp.Ctlcontrols;
                double position = control.currentPosition;
                if (playingYoutube == true && player_wmp.status == "Stopped" && position == 0)
                {
                    current_youtubeid++;
                    play_youtube_channel();
                }
                tvkSeeking.SetValue((int)(position * 100 / duration));

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.Message);
                Utility.WriteLog(ex.StackTrace);
            }
        }

        public void FinishedSetTimeHourDaily(int secondLeft)
        {
            RegisterCheck reg = new RegisterCheck();
            if (reg.IsRegister() == false)
            {
                int second = secondLeft % 60;
                int minute = (secondLeft-second) / 60;
                labelVersion.Text = "Version: " + AppConst.Version;
                labelVersion.Text += " - " + minute + ":" + second + " Minutes FREE watching";
            }
        }
    }
}
