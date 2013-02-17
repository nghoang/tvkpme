using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.XPath;
using System.Xml;
using System.Web;
using System.Threading;

namespace TVKing2
{
    public partial class RatingForm : TVKForm
    {
        string id;
        string name;
        public RatingForm(string _id, string _name)
        {
            InitializeComponent();
            id = _id;
            name = _name;

            txtTitle.Text = "Rating " + name;
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtName.Text == "Your name")
                txtName.Text = "";
        }

        private void txtComment_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtComment.Text == "Your comment")
                txtComment.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void RatingForm_Load(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(ReadComments));
            th.Start();
        }

        private void ReadComments()
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallback(ReadComments));
                return;
            }
            XPathDocument doc = new XPathDocument("http://www.tvking.tv/tvking_services.php?action=comment&channel_id=" + id);
            XPathNavigator nav = doc.CreateNavigator();
            XPathExpression expr;
            expr = nav.Compile("/comments/comment");
            XPathNodeIterator iterator = nav.Select(expr);

            while (iterator.MoveNext())
            {
                XPathNavigator nav2 = iterator.Current.Clone();
                string name = nav2.GetAttribute("name", "");
                string content = nav2.GetAttribute("content", "");

                txtComments.Text += HttpUtility.UrlDecode(name) + "\n";
                txtComments.Text += HttpUtility.UrlDecode(content) + "\n\n";
                txtComments.Text += "==========================\n\n";
            }
        }

        private void buttonBackYoutube_Click(object sender, EventArgs e)
        {
            if (ratingStars1.currentSelect == 0)
            {
                MessageBox.Show("Please rate this stream");
                return;
            }
            if (txtName.Text == "" || txtName.Text == "Your name")
            {
                MessageBox.Show("Please enter your name");
                return;
            }
            if (txtComment.Text == "" || txtComment.Text == "Your name")
            {
                MessageBox.Show("Please enter your comment");
                return;
            }

            RequestTVKServer req = new RequestTVKServer();
            req.PostRating(id, ratingStars1.currentSelect.ToString(), txtName.Text, txtComment.Text);
            this.Visible = false;
            MessageBox.Show("You have rated the item Successfully");
            this.Close();
        }
    }
}
