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
    public partial class TVKTrialScreen : UserControl
    {
        public TVKTrialScreen()
        {
            InitializeComponent();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            PictureBox obj = (PictureBox)sender;
            obj.Image = global::TVKing2.TVKControlResource.button_register_on;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            PictureBox obj = (PictureBox)sender;
            obj.Image = global::TVKing2.TVKControlResource.button_register_of;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tvking.tv/register/");
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            Label obj = (Label)sender;
            obj.ForeColor = Color.DodgerBlue;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            Label obj = (Label)sender;
            obj.ForeColor = Color.DodgerBlue;
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            Label obj = (Label)sender;
            obj.ForeColor = Color.DodgerBlue;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            Label obj = (Label)sender;
            obj.ForeColor = Color.DarkGray;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            Label obj = (Label)sender;
            obj.ForeColor = Color.DarkGray;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            Label obj = (Label)sender;
            obj.ForeColor = Color.DarkGray;
        }
    }
}
