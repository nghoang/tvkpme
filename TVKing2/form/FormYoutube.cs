using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrawlerLib.Net;

namespace TVKing2
{
    public partial class FormYoutube : TVKForm, IYoutubeSearch
    {
        string[] category_names = new string[] { "All", "Music", "Entertainment", "Film & Animation", "Comedy" };
        string[] category_ids = new string[] { "", "10", "24", "1", "23" };

        string[] sort_names = new string[] { "Relevance", "Upload date", "View count", "Rating" };
        string[] sort_ids = new string[] { "", "video_date_uploaded", "video_view_count", "video_avg_rating" };

        string[] uploaded_names = new string[] { "Anytime", "Today", "This week", "This month" };
        string[] uploaded_ids = new string[] { "", "d", "w", "m" };

        YoutubeSearch ys = new YoutubeSearch();
        public IFormYoutube callback;
        public FormYoutube()
        {
            InitializeComponent();
            LoadSearchToList();
            this.StartPosition = FormStartPosition.CenterScreen;
            comboBoxCategory.Items.Clear();
            foreach (string c in category_names)
            {
                comboBoxCategory.Items.Add(c);
            }
            comboBoxCategory.SelectedIndex = 0;


            comboBoxSort.Items.Clear();
            foreach (string c in sort_names)
            {
                comboBoxSort.Items.Add(c);
            }
            comboBoxSort.SelectedIndex = 0;


            comboBoxUploaded.Items.Clear();
            foreach (string c in uploaded_names)
            {
                comboBoxUploaded.Items.Add(c);
            }
            comboBoxUploaded.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void FormYoutube_Load(object sender, EventArgs e)
        {
            this.ShowResize(false);
            this.TopMost = true;
        }

        private void textBoxKeyword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        public void IYS_BeginSearching()
        {
        }

        public void IYS_ProgressSearching(int percent)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallbackInt(IYS_ProgressSearching), percent);
                return;
            }
            Console.WriteLine(percent);
            barSearching.Value = percent;
        }

        public void IYS_FinishedSearching(List<YoutubeObject> youtube_videos)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallbackYoutubeList(IYS_FinishedSearching), youtube_videos);
                return;
            }
            callback.FormFinishedSearch(youtube_videos);
            this.Close();
        }

        public void IYS_Stop()
        {
            btSearch.Visible = true;
            btStop.Visible = false;
            barSearching.Visible = false;
        }

        private void Search()
        {
            if (textBoxKeyword.Text.Trim() == "")
            {
                MessageBox.Show("Please enter search term");
                return;
            }
            string cate_id = category_ids[comboBoxCategory.SelectedIndex];
            string sort_id = sort_ids[comboBoxSort.SelectedIndex];
            string uploaded_id = uploaded_ids[comboBoxUploaded.SelectedIndex];

            btSearch.Visible = false;
            barSearching.Value = 3;
            barSearching.Visible = true;
            btStop.Visible = true;

            ys = new YoutubeSearch();
            ys.callback = this;
            ys.SetSearchVariables(cate_id, sort_id, uploaded_id, textBoxKeyword.Text);
            //ys.SearchYoutubeASY();
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(ys.SearchYoutubeASY));
            th.Start();

            SaveSearchInList();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Search();
        }
        List<string> search_keys = null;
        private void LoadSearchToList()
        {
            if (Utility.IsFileExist("search_youtube.txt") == false)
            {
                Utility.WriteFile("search_youtube.txt", "", false);
            }
            string content = Utility.ReadFile("search_youtube.txt");
            string[] lines = content.Split('|');
            search_keys = new List<string>();
            listBoxHistory.Items.Clear();
            foreach (string l in lines)
            {
                if (l.Trim() == "")
                    continue;
                listBoxHistory.Items.Add(Utility.URLDecode(l.Split('&')[0].Trim()));
                search_keys.Add(l.Trim());
            }
        }

        private void SaveSearchInList()
        {
            string key = textBoxKeyword.Text.Trim();
            int op1 = comboBoxCategory.SelectedIndex;
            int op2 = comboBoxSort.SelectedIndex;
            int op3 = comboBoxUploaded.SelectedIndex;
            string line = Utility.URLEncode(key) + "&" + op1 + "&" + op2 + "&" + op3;
            bool has = false;
            foreach (string l in search_keys)
            {
                if (l == line)
                {
                    has = true;
                    break;
                }
            }
            if (has == false)
            {
                string content = Utility.ReadFile("search_youtube.txt");
                content = line + '|' + content;
                Utility.WriteFile("search_youtube.txt", content, false);
                LoadSearchToList();
            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            ys.stoping = true;
        }

        private void textBoxKeyword_TextChanged(object sender, EventArgs e)
        {

        }

        private void SelectYoutubeItem()
        {
            if (search_keys == null || listBoxHistory.SelectedIndex < 0)
                return;
            string key = Utility.URLDecode(search_keys[listBoxHistory.SelectedIndex].Split('&')[0]);
            string op1 = search_keys[listBoxHistory.SelectedIndex].Split('&')[1];
            string op2 = search_keys[listBoxHistory.SelectedIndex].Split('&')[2];
            string op3 = search_keys[listBoxHistory.SelectedIndex].Split('&')[3];

            textBoxKeyword.Text = key;
            comboBoxCategory.SelectedIndex = int.Parse(op1);
            comboBoxSort.SelectedIndex = int.Parse(op2);
            comboBoxUploaded.SelectedIndex = int.Parse(op3);
        }

        private void listBoxHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectYoutubeItem();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxHistory.SelectedIndex < 0)
                return;
            if (Utility.IsFileExist("search_youtube.txt") == false)
            {
                Utility.WriteFile("search_youtube.txt", "", false);
            }
            string content = Utility.ReadFile("search_youtube.txt");
            string[] lines = content.Split('|');
            int t = -1;
            string new_content = "";

            foreach (string l in lines)
            {
                if (l.Trim() == "")
                    continue;
                t++;
                if (t == listBoxHistory.SelectedIndex)
                    continue;
                new_content += l + "|";
            }
            Utility.WriteFile("search_youtube.txt", new_content, false);
            LoadSearchToList();
        }

        private void listBoxHistory_MouseClick(object sender, MouseEventArgs e)
        {
            SelectYoutubeItem();
        }
    }
}
