using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TVKing_Updater
{
    delegate void DeCallbackString(string res);
    delegate void DeCallbackInt(int res);
    public partial class Form1 : Form, IFormDownload
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pbDownloading.Value = 0;
            pbDownloading.Maximum = 100;
            UpdateServer up = new UpdateServer(this);
            Thread th = new Thread(new ThreadStart(up.DownloadSetUpFile));
            th.Start();
        }

        #region IFormDownload Members

        public void download_progress(int p)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DeCallbackInt(download_progress), p);
                return;
            }
            pbDownloading.Value = (int)p;
        }

        public void finished_download(string setup_path)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new DeCallbackString(finished_download), setup_path);
                return;
            }
            if (MessageBox.Show("Download was completed. Do you want to run the update?", "Update", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                Proc.StartInfo.FileName = setup_path;
                Proc.Start();
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        #endregion
    }
}
