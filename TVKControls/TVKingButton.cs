using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace TVKing2
{
    public partial class TVKingButton : Button
    {
        public TVKingButton()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Height = 22;
        }

        public TVKingButton(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.Clear(Parent.BackColor);
            Draw(g);
        }

        private void Draw(Graphics g)
        {
            DrawButton(g);
            DrawText(g);
        }

        private void DrawText(Graphics g)
        {
            Font f = new Font("Arial", 8);
            int textLength = System.Windows.Forms.TextRenderer.MeasureText(this.Text, f).Width;
            int left = (this.Width - textLength)/2;
            g.DrawString(this.Text, f, new SolidBrush(Color.White), left, 4);
        }

        bool is_mouse_over = false;

        protected override void OnMouseLeave(EventArgs e)
        {
            is_mouse_over = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            is_mouse_over = true;
            base.OnMouseEnter(e);
        }

        protected virtual void DrawButton(Graphics g)
        {
            if (is_mouse_over == true)
            {
                g.FillRectangle(new TextureBrush(TVKControlResource.button_mid_click), 0, 0, this.Width, this.Height);
                g.DrawImage(TVKControlResource.button_left_click, 0, 0);
                int left = this.Width - TVKControlResource.button_right.Width;
                if (left < 0)
                    left = 0;
                g.DrawImage(TVKControlResource.button_right_click, left, 0);
            }
            else
            {
                g.FillRectangle(new TextureBrush(TVKControlResource.button_middle), 0, 0, this.Width, this.Height);
                g.DrawImage(TVKControlResource.button_left, 0, 0);
                int left = this.Width - TVKControlResource.button_right.Width;
                if (left < 0)
                    left = 0;
                g.DrawImage(TVKControlResource.button_right, left, 0);
            }
        }
    }
}
