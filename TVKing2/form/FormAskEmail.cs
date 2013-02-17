using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TVKing2
{
    public delegate void DeAfterSubmitEmail();
    public partial class FormAskEmail : TVKForm
    {
        public DeAfterSubmitEmail deAfterSubmitEmail;
        public FormAskEmail()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            string email_regex = @"^[a-zA-Z0-9\._-]+@[a-zA-Z0-9\._-]+\.[a-zA-Z0-9]+$";
            if (CrawlerLib.Net.Utility.SimpleRegexSingle(email_regex, textBoxEmail.Text, 0) != "")
            {
                RequestTVKServer sv = new RequestTVKServer();
                sv.SubmitEmail(textBoxEmail.Text);
                CrawlerLib.Net.Utility.WriteAppRegistry("TVKing2", "email", textBoxEmail.Text);
                deAfterSubmitEmail.Invoke();
                this.Close();
            }
            else
            {
                MessageBox.Show("Your email is not correct. Please enter again.", "Error Input");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CrawlerLib.Net.Utility.WriteAppRegistry("TVKing2", "email", "noemail");
            this.Close();
        }
    }
}
