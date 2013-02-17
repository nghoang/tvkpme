using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace TVKing2
{
    public partial class TVKForm : Form
    {
        bool isMoving = false;
        int currentY = 0;
        int currentX = 0;

        public void ShowResize(bool show_icon)
        {
            iconResize.Visible = show_icon;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.DrawLine(new Pen(Color.Black, 4), 0, 0, this.Width, 0);
            g.DrawLine(new Pen(Color.Black, 4), 0, 0, 0, this.Height);
            g.DrawLine(new Pen(Color.Black, 5), 0, this.Height, this.Width, this.Height);
            g.DrawLine(new Pen(Color.Black, 5), this.Width, 0, this.Width, this.Height);
        }

        //[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //private static extern IntPtr CreateRoundRectRgn
        //(
        //    int nLeftRect, // x-coordinate of upper-left corner
        //     int nTopRect, // y-coordinate of upper-left corner
        //     int nRightRect, // x-coordinate of lower-right corner
        //     int nBottomRect, // y-coordinate of lower-right corner
        //     int nWidthEllipse, // height of ellipse
        //     int nHeightEllipse // width of ellipse
        // );

        protected override void OnResize(EventArgs e)
        {
            RenderControls();
            base.OnResize(e);
        }

        private void RenderControls()
        {
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            iconClose.Top = 10;
            iconClose.Left = this.Width - 30;

            iconMaximize.Top = iconClose.Top;
            iconMaximize.Left = iconClose.Left - 30;

            iconMinimize.Top = iconMaximize.Top;
            iconMinimize.Left = iconMaximize.Left - 30;

            barDrag.Top = 0;
            barDrag.Left = 0;
            barDrag.Width = this.Width;
            iconResize.Left = this.Width - iconResize.Width;
            iconResize.Top = this.Height - iconResize.Height;
        }

        public TVKForm()
        {

            InitializeComponent();

            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            iconResize.Width = 15;
            iconResize.Height = 15;

            this.SetStyle(ControlStyles.DoubleBuffer |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.UserPaint, true);
        }

        public TVKForm(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void iconClose_MouseHover(object sender, EventArgs e)
        {
            iconClose.Image = TVKControlResource.icon_close;
        }

        private void iconClose_MouseLeave(object sender, EventArgs e)
        {
            iconClose.Image = TVKControlResource.icon_close_n;
        }

        private void iconMinimize_MouseHover(object sender, EventArgs e)
        {
            iconMinimize.Image = TVKControlResource.icon_minimize;
        }

        private void iconMinimize_MouseLeave(object sender, EventArgs e)
        {
            iconMinimize.Image = TVKControlResource.icon_minimize_n;
        }

        private void iconMaximize_MouseHover(object sender, EventArgs e)
        {
            iconMaximize.Image = TVKControlResource.icon_maximize;
        }

        private void iconMaximize_MouseLeave(object sender, EventArgs e)
        {
            iconMaximize.Image = TVKControlResource.icon_maximize_n;
        }

        private void iconClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void iconMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
        }

        private void iconMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void barDrag_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
                this.WindowState = FormWindowState.Normal;
            else
                this.WindowState = FormWindowState.Maximized;
        }

        private void barDrag_MouseDown(object sender, MouseEventArgs e)
        {
            isMoving = true;
            currentX = e.X;
            currentY = e.Y;
        }

        private void barDrag_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                this.Top = this.Top + (e.Y - currentY);
                this.Left = this.Left + (e.X - currentX);
            }
        }

        private void barDrag_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void iconResize_MouseDown(object sender, MouseEventArgs e)
        {
            isMoving = true;
            currentX = e.X;
            currentY = e.Y;
        }

        private void iconResize_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoving)
            {
                this.Height = this.Height + (e.Y - currentY);
                this.Width = this.Width + (e.X - currentX);
            }
        }

        private void iconResize_MouseUp(object sender, MouseEventArgs e)
        {
            isMoving = false;
        }

        private void TVKForm_Load(object sender, EventArgs e)
        {

        }
    }
}
