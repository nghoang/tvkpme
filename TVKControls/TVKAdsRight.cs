using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using CrawlerLib.Net;

namespace TVKing2
{
    public partial class TVKAdsRight : Form
    {
        string link1 = "";
        string link2 = "";
        string link3 = "";
        string link4 = "";
        private string _ServerUrl = "";
        WebClient client = new WebClient();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
             int nTopRect, // y-coordinate of upper-left corner
             int nRightRect, // x-coordinate of lower-right corner
             int nBottomRect, // y-coordinate of lower-right corner
             int nWidthEllipse, // height of ellipse
             int nHeightEllipse // width of ellipse
         );

        public TVKAdsRight(string ServerUrl)
        {
            InitializeComponent();

            this.ShowInTaskbar = false;
            _ServerUrl = ServerUrl;
            //client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
        }

        static public string URLEncode(String v)
        {
            return System.Web.HttpUtility.UrlEncode(v);
        }

        static public string URLDecode(String v)
        {
            return System.Web.HttpUtility.UrlDecode(v);
        }

        //void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        //{

        //}

        private void TVKAdsRight_Load(object sender, EventArgs e)
        {
            //String content = client.DownloadString(_ServerUrl + "?action=get_ads_list");
            //client.DownloadStringAsync(new Uri(_ServerUrl+"?action=get_ads_list"));
            string content = client.DownloadString(_ServerUrl + "?action=get_ads_list");
            List<String> ads = Utility.SimpleRegex("<ad>(.*?)(?=</ad>)</ad>", content, 0);
            int i = 0;
            foreach (String ad in ads)
            {
                String img = Utility.SimpleRegexSingle("<name>(.*?)(?=</name>)</name>", ad, 1);
                String link = Utility.SimpleRegexSingle("<link>(.*?)(?=</link>)</link>", ad, 1);
                img = URLDecode(img);
                link = URLDecode(link);
                if (i == 0)
                {
                    try
                    {
                        pictureBox1.LoadAsync(img);
                        link1 = link;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Loading ad picture 1: " + ex.Message);
                    }
                }
                else if (i == 1)
                {
                    try
                    {
                        pictureBox2.LoadAsync(img);
                        link2 = link;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Loading ad picture 2: " + ex.Message);
                    }
                }
                else if (i == 2)
                {
                    try
                    {
                        pictureBox3.LoadAsync(img);
                        link3 = link;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Loading ad picture 3: " + ex.Message);
                    }
                }
                else if (i == 3)
                {
                    try
                    {
                        pictureBox4.LoadAsync(img);
                        link4 = link;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Loading ad picture 4: " + ex.Message);
                    }
                }

                i++;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (link1 != "")
            {
                System.Diagnostics.Process.Start(link1);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (link2 != "")
            {
                System.Diagnostics.Process.Start(link2);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (link3 != "")
            {
                System.Diagnostics.Process.Start(link3);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (link4 != "")
            {
                System.Diagnostics.Process.Start(link4);
            }
        }

        //public static List<string> SimpleRegex(string pattern, string content, int group, RegexOptions options)
        //{
        //    Regex exp = new Regex(pattern, options);
        //    MatchCollection MatchList = exp.Matches(content);
        //    List<string> res = new List<string>();
        //    foreach (Match m in MatchList)
        //    {
        //        res.Add(m.Groups[group].Value);
        //    }
        //    return res;
        //}

        //public static string SimpleRegexSingle(string pattern, string content, int group, RegexOptions options)
        //{
        //    List<string> res = SimpleRegex(pattern, content, group, options);
        //    if ((res.Count > 0))
        //    {
        //        return res[0];
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}

        //public static List<string> SimpleRegex(string pattern, string content, int group)
        //{
        //    return SimpleRegex(pattern, content, group, RegexOptions.Singleline);
        //}

        //public static string SimpleRegexSingle(string pattern, string content, int group)
        //{
        //    return SimpleRegexSingle(pattern, content, group, RegexOptions.Singleline);
        //}

        public void SetHeight(int h)
        {
            this.Height = h;
        }

        private void TVKAdsRight_Resize(object sender, EventArgs e)
        {
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
    }
}
