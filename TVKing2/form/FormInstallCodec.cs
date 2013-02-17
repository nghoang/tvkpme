using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrawlerLib.Net;

namespace TVKing2.form
{
    public partial class FormInstallCodec : TVKForm
    {
        public FormInstallCodec()
        {
            InitializeComponent();
            this.ShowResize(false);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.free-codecs.com/download/k_lite_codec_pack.htm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Utility.WriteAppRegistry("TVKing2", "not_show_codec", "1");
            }
            this.Close();
        }
    }
}
