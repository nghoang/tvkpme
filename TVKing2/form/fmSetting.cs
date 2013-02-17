using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CrawlerLib.Net;

namespace TVKing2
{
    public partial class fmSetting : TVKForm
    {
        public fmSetting()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStartup.Checked == true)
                Utility.WriteRegistry("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "TVKing2", Directory.GetCurrentDirectory() + "\\TVKing2.exe", "user");
            else
                Utility.WriteRegistry("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "TVKing2", "", "user");
        }

        private void cbShowlogo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowlogo.Checked == true)
                Utility.WriteAppRegistry("TVKing2", "show_logo", "1");
            else

                Utility.WriteAppRegistry("TVKing2", "show_logo", "0");
        }

        private void cbAutoplay_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoplay.Checked == true)
                Utility.WriteAppRegistry("TVKing2", "auto_play", "1");
            else

                Utility.WriteAppRegistry("TVKing2", "auto_play", "0");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void fmSetting_Load(object sender, EventArgs e)
        {
            this.ShowResize(false);
            tvkLabel1.Text = "Settings";
            if (Utility.ReadRegistry("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", "TVKing2", "user") != "")
                cbStartup.Checked = true;
            else
                cbStartup.Checked = false;

            if (Utility.ReadAppRegistry("TVKing2", "show_logo") == "0")
                cbShowlogo.Checked = false;
            else
                cbShowlogo.Checked = true;

            if (Utility.ReadAppRegistry("TVKing2", "auto_play") == "0")
                cbAutoplay.Checked = false;
            else
                cbAutoplay.Checked = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Favorite list will be deleted. Are you sure?", "Clear cache", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (File.Exists(Application.StartupPath + "\\fav.txt"))
                    File.Delete(Application.StartupPath + "\\fav.txt");
            }
        }
    }
}
