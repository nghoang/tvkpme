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
    public partial class ctlAdvertisement : UserControl
    {
        int currentTime = 0;
        MainApp main;
        public ctlAdvertisement()
        {
            InitializeComponent();
            lbTimer.Visible = false;
        }

        internal void SetTimer(int p)
        {
        }

        private void tmAds_Tick(object sender, EventArgs e)
        {
            currentTime--;
            lbTimer.Text = "Your channel starts in " + currentTime.ToString() + " seconds";
            if (currentTime == 0)
            {
                main.HideAdsOnChannel();
                tmAds.Enabled = false;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tvking.tv");
        }

        internal void SetTimer(int p, MainApp mainApp)
        {
            main = mainApp;
            lbTimer.Visible = true;
            currentTime = p;
            tmAds.Enabled = true;
        }
    }
}
