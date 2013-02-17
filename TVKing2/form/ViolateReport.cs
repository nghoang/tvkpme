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
    public partial class ViolateReport : TVKForm
    {
        public ViolateReport(string email, string channel)
        {
            InitializeComponent();
            textBoxEmail.Text  = email;
            textBoxChannel.Text = channel;
        }

        private void ViolateReport_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBoxEmail.Text == "")
            {
                MessageBox.Show("Please enter your email", "System");
                return;
            }
            else
                if (textBoxChannel.Text == "")
                {
                    MessageBox.Show("Please enter channel name", "System");
                    return;
                }
            RequestTVKServer sv = new RequestTVKServer();
            sv.ReportChannelViolation(textBoxEmail.Text, textBoxChannel.Text, textBoxMessage.Text);
            MessageBox.Show("Thank you for your report", "System");
            this.Hide();
        }
    }
}
