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
    public delegate void TVKVolumeScrollEvent(object sender, int value);
    public delegate void TVKEndVolumeScrollEvent();
    public partial class TVKVolume : UserControl
    {
        public bool is_show_icons = true;
        public event TVKVolumeScrollEvent VolumeScroll;
        public event TVKEndVolumeScrollEvent EndVolumeScroll;
        protected virtual void OnVolumeScroll(int e)
        {
            if (VolumeScroll != null)
                VolumeScroll(this, e);
        }
        protected virtual void OnEndVolumeScroll()
        {
            if (EndVolumeScroll != null)
                EndVolumeScroll();
        }

        public int value = 67;
        public PictureBox indicator = new PictureBox();
        bool isMoving = false;
        int currentX, currentY;
        public TVKVolume()
        {
            InitializeComponent();
            this.Height = global::TVKing2.TVKControlResource.volume_right.Height;
            this.BackgroundImage = global::TVKing2.TVKControlResource.volume_bg;
            this.Controls.Add(indicator);
            indicator.Top = 0;
            indicator.Left = (this.Width - 40) * value / 100;
            indicator.Image = global::TVKing2.TVKControlResource.volume_indicator;
            indicator.Width = global::TVKing2.TVKControlResource.volume_indicator.Width;
            indicator.Height = this.Height;

            indicator.MouseUp += new MouseEventHandler(indicator_MouseUp);
            indicator.MouseMove += new MouseEventHandler(indicator_MouseMove);
            indicator.MouseDown += new MouseEventHandler(indicator_MouseDown);
            indicator.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        void indicator_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
            SetVolumePosition(e);
            OnEndVolumeScroll();
        }

        void SetVolumePosition(MouseEventArgs e)
        {
            int min = 15;
            int max = this.Width - 27;
            indicator.Top = 0;
            int x = indicator.Left + (e.X - currentX);
            if (x < min)
                x = min;
            if (x > max)
                x = max;
            indicator.Left = x;
            value = (indicator.Left - min) * 100 / (max - min);
        }

        void indicator_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                SetVolumePosition(e);
                OnVolumeScroll(value);
            }
        }

        public void SetValue(int v)
        {
            int min = 15;
            int max = this.Width - 27;
            indicator.Left = v * (max - min) / 100 + min;
            value = v;
        }

        void indicator_MouseDown(object sender, MouseEventArgs e)
        {
            isMoving = true;
            currentX = e.X;
            currentY = e.Y;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (is_show_icons == true)
            {
                g.DrawImage(global::TVKing2.TVKControlResource.volume_left, 0, 0);
                g.DrawImage(global::TVKing2.TVKControlResource.volume_right, this.Width - global::TVKing2.TVKControlResource.volume_right.Width, 0);
            }
            else 
            {
                g.DrawImage(global::TVKing2.TVKControlResource.volume_left2, 0, 0, global::TVKing2.TVKControlResource.volume_left2.Width, global::TVKing2.TVKControlResource.volume_left2.Height);
                g.DrawImage(global::TVKing2.TVKControlResource.volume_right2, this.Width - global::TVKing2.TVKControlResource.volume_right.Width, 0, global::TVKing2.TVKControlResource.volume_right2.Width, global::TVKing2.TVKControlResource.volume_right2.Height);
            }
        }
    }
}
