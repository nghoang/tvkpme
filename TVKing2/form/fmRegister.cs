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
    public partial class fmRegister : TVKForm
    {
        public fmRegister()
        {
            InitializeComponent();
        }

        bool isExpired = false;

        public fmRegister(bool reg)
        {
            InitializeComponent();
            isExpired = reg;
            LoadPosition();
        }



        private void MakeUnlock()
        {
            RegisterCheck reg = new RegisterCheck();
            bool registerresult = false;

            switch (AppConst.appType)
            {
                case RegisterType.Ads:
                    registerresult = reg.Register(txtEmail.Text, txtKey.Text);
                    break;
                case RegisterType.Free:
                    registerresult = reg.Register3(txtEmail.Text);
                    break;
                case RegisterType.Hours:
                    registerresult = reg.Register2(txtEmail.Text, txtKey.Text);
                    break;
                case RegisterType.HoursDaily:
                    registerresult = reg.Register2(txtEmail.Text, txtKey.Text);
                    break;
                case RegisterType.Trial:
                    registerresult = reg.Register(txtEmail.Text, txtKey.Text);
                    break;
            }

            if (registerresult == true)
            {
                Utility.WriteAppRegistry("TVKing2", "email", txtEmail.Text);
                Utility.WriteAppRegistry("TVKing2", "license", txtKey.Text);
                MessageBox.Show("Thank for your registration. Please restart this application.");

                this.Close();
            }
            else
            {
                MessageBox.Show("Register key is not correct.");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tvking.tv");
        }

        private void fmRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExpired == false)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tvking.tv/register/");
        }

        private void LoadPosition()
        {
            this.Width = picRegLogo.Width + picRegLogo.Left + 15;
            lbToUnlock.Top = picRegLogo.Top + picRegLogo.Height + 7;
            lbEmail.Top = lbToUnlock.Top + lbToUnlock.Height + 10;
            lbEmail.Left = picRegLogo.Left;
            txtEmail.Top = lbEmail.Top - 3;
            txtEmail.Left = lbEmail.Left + lbEmail.Width + 3;
            txtEmail.Width = this.Width - txtEmail.Left - 10;
            lbLicense.Top = lbEmail.Top + lbEmail.Height + 15;
            lbLicense.Left = lbEmail.Left;
            txtKey.Top = lbLicense.Top - 3;
            txtKey.Left = txtEmail.Left;
            txtKey.Width = txtEmail.Width - picUnlock.Width - 5;
            picUnlock.Top = lbLicense.Top;
            picUnlock.Left = txtKey.Left + txtKey.Width + 3;
            this.Height = picUnlock.Top + picUnlock.Height + 10;
        }

        private void fmRegister_Load(object sender, EventArgs e)
        {
            LoadPosition();
            RegisterCheck reg = new RegisterCheck();
            if (reg.IsRegister() == true)
            {
                txtEmail.Text = Utility.ReadAppRegistry("TVKing2", "email");
                txtKey.Text = Utility.ReadAppRegistry("TVKing2", "license");
                txtEmail.Enabled = false;
                txtKey.Enabled = false;
                picUnlock.Enabled = false;
            }
            switch (AppConst.appType)
            {
                case RegisterType.Ads:
                    break;
                case RegisterType.Free:
                    txtKey.Text = "FREE";
                    txtKey.Enabled = false;
                    break;
                case RegisterType.Hours:
                    break;
                case RegisterType.HoursDaily:
                    break;
                case RegisterType.Trial:
                    break;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.tvking.tv/register/");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MakeUnlock();
        }

        private void fmRegister_Resize(object sender, EventArgs e)
        {
            LoadPosition();
        }
    }
}
