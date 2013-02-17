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
    public partial class TVKingTextBox : UserControl
    {
        int margin_left = 5;
        int margin_top = 3;
        int margin_bottom = 3;
        int margin_right = 5;
        public TVKingTextBox()
        {
            InitializeComponent();
            LoadPosition();

            this.SetStyle(ControlStyles.DoubleBuffer |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.UserPaint, true);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LoadPosition();
            txt.Text = "";
        }

        private void LoadPosition()
        {
            txt.Top = margin_top;
            txt.Left = margin_left;
            txt.Width = this.Width - margin_left - margin_right;
            this.Height = margin_top + txt.Height + margin_bottom;
        }

        public string Text
        {
            get
            {
                return txt.Text;
            }
            set
            {
                txt.Text = value;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            for (int i = 0; i < this.Width; i++)
            {
                g.DrawImage(TVKControlResource.txt_border_bottom, new Point(i, this.Height - TVKControlResource.txt_border_bottom.Height));
            }

            for (int i = 0; i < this.Height; i++)
            {
                g.DrawImage(TVKControlResource.txt_border_right, new Point(this.Width - TVKControlResource.txt_border_right.Width, i));
            }
        }

        private void TVKingTextBox_Load(object sender, EventArgs e)
        {
            LoadPosition();
        }
    }
}
