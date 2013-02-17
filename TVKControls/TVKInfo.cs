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
    public partial class TVKInfo : UserControl
    {
        public TVKInfo()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.UserPaint, true);
        }

        private void TVKInfo_Resize(object sender, EventArgs e)
        {
            pbLeft.Top = 0;
            pbLeft.Left = 0;

            pbRight.Top = 0;
            pbRight.Left = this.Width - pbRight.Width;

            //pbMiddle.Left = 0;
            //pbMiddle.Top = 0;
            //pbMiddle.Width = this.Width;
            //pbMiddle.Height = this.Height;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Graphics g = e.Graphics;

            g.FillRectangle(new TextureBrush(global::TVKing2.TVKControlResource.info_middle),new Rectangle(0,0,this.Width,this.Height));
        }
    }
}
