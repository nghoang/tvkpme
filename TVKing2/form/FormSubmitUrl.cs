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
    public partial class FormSubmitUrl : TVKForm
    {
        public List<string> Countries;
        public FormSubmitUrl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void FormSubmitUrl_Load(object sender, EventArgs e)
        {
            this.ShowResize(false);
            listboxCountry.Items.Clear();
            foreach (string c in Countries)
            {
                if (c == "More Countries")
                    continue;
                listboxCountry.Items.Add(c);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBoxChannelUrl.Text.Trim() == "")
            {
                MessageBox.Show("Please enter URL", "Error");
                return;
            }
            else
                if (listboxCountry.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please choose country", "Error");
                    return;
                }
            RequestTVKServer sv = new RequestTVKServer();
            sv.SubmitUrl(textBoxChannelUrl.Text, listboxCountry.SelectedItem.ToString());
            MessageBox.Show("Thank you for your submission. Our staff will verify your submission.", "Submission is completed");
            this.Close();
        }
    }
}
