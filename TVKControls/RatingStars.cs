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
    public partial class RatingStars : UserControl
    {
        public int currentSelect = 0;

        public RatingStars()
        {
            InitializeComponent();

            star1.Width = 25;
            star1.Height = 24;
            star1.Top = 0;
            star1.Left = 0;

            star2.Width = 25;
            star2.Height = 24;
            star2.Top = 0;
            star2.Left = 1 * 27;

            star3.Width = 25;
            star3.Height = 24;
            star3.Top = 0;
            star3.Left = 2 * 27;

            star4.Width = 25;
            star4.Height = 24;
            star4.Top = 0;
            star4.Left = 3 * 27;

            star5.Width = 25;
            star5.Height = 24;
            star5.Top = 0;
            star5.Left = 4 * 27;

            star2.Click += new EventHandler(star2_Click);
            star2.MouseHover += new EventHandler(star2_MouseHover);
            star2.MouseLeave += new EventHandler(star2_MouseLeave);

            star3.Click += new EventHandler(star3_Click);
            star3.MouseHover += new EventHandler(star3_MouseHover);
            star3.MouseLeave += new EventHandler(star3_MouseLeave);

            star4.Click += new EventHandler(star4_Click);
            star4.MouseHover += new EventHandler(star4_MouseHover);
            star4.MouseLeave += new EventHandler(star4_MouseLeave);

            star5.Click += new EventHandler(star5_Click);
            star5.MouseHover += new EventHandler(star5_MouseHover);
            star5.MouseLeave += new EventHandler(star5_MouseLeave);

            star1.Image = TVKControlResource.star_chooser_off;
            star2.Image = TVKControlResource.star_chooser_off;
            star3.Image = TVKControlResource.star_chooser_off;
            star4.Image = TVKControlResource.star_chooser_off;
            star5.Image = TVKControlResource.star_chooser_off;
        }

        private void SelectRate(int i)
        {
            star1.Image = TVKControlResource.star_chooser_off;
            star2.Image = TVKControlResource.star_chooser_off;
            star3.Image = TVKControlResource.star_chooser_off;
            star4.Image = TVKControlResource.star_chooser_off;
            star5.Image = TVKControlResource.star_chooser_off;
            switch (i)
            {
                case 1:
                    star1.Image = TVKControlResource.star_chooser_on;
                    star2.Image = TVKControlResource.star_chooser_off;
                    star3.Image = TVKControlResource.star_chooser_off;
                    star4.Image = TVKControlResource.star_chooser_off;
                    star5.Image = TVKControlResource.star_chooser_off;
                    break;
                case 2:
                    star1.Image = TVKControlResource.star_chooser_on;
                    star2.Image = TVKControlResource.star_chooser_on;
                    star3.Image = TVKControlResource.star_chooser_off;
                    star4.Image = TVKControlResource.star_chooser_off;
                    star5.Image = TVKControlResource.star_chooser_off;
                    break;
                case 3:
                    star1.Image = TVKControlResource.star_chooser_on;
                    star2.Image = TVKControlResource.star_chooser_on;
                    star3.Image = TVKControlResource.star_chooser_on;
                    star4.Image = TVKControlResource.star_chooser_off;
                    star5.Image = TVKControlResource.star_chooser_off;
                    break;
                case 4:
                    star1.Image = TVKControlResource.star_chooser_on;
                    star2.Image = TVKControlResource.star_chooser_on;
                    star3.Image = TVKControlResource.star_chooser_on;
                    star4.Image = TVKControlResource.star_chooser_on;
                    star5.Image = TVKControlResource.star_chooser_off;
                    break;
                case 5:
                    star1.Image = TVKControlResource.star_chooser_on;
                    star2.Image = TVKControlResource.star_chooser_on;
                    star3.Image = TVKControlResource.star_chooser_on;
                    star4.Image = TVKControlResource.star_chooser_on;
                    star5.Image = TVKControlResource.star_chooser_on;
                    break;
            }
        }

        private void star1_MouseHover(object sender, EventArgs e)
        {
            SelectRate(1);
        }

        private void star1_MouseLeave(object sender, EventArgs e)
        {
            SelectRate(currentSelect);
        }

        private void star1_Click(object sender, EventArgs e)
        {
            currentSelect = 1;
            SelectRate(currentSelect);
        }

        private void star2_MouseHover(object sender, EventArgs e)
        {
            SelectRate(2);
        }

        private void star2_MouseLeave(object sender, EventArgs e)
        {
            SelectRate(currentSelect);
        }

        private void star2_Click(object sender, EventArgs e)
        {
            currentSelect = 2;
            SelectRate(currentSelect);
        }



        private void star3_MouseHover(object sender, EventArgs e)
        {
            SelectRate(3);
        }

        private void star3_MouseLeave(object sender, EventArgs e)
        {
            SelectRate(currentSelect);
        }

        private void star3_Click(object sender, EventArgs e)
        {
            currentSelect = 3;
            SelectRate(currentSelect);
        }



        private void star4_MouseHover(object sender, EventArgs e)
        {
            SelectRate(4);
        }

        private void star4_MouseLeave(object sender, EventArgs e)
        {
            SelectRate(currentSelect);
        }

        private void star4_Click(object sender, EventArgs e)
        {
            currentSelect = 4;
            SelectRate(currentSelect);
        }



        private void star5_MouseHover(object sender, EventArgs e)
        {
            SelectRate(5);
        }

        private void star5_MouseLeave(object sender, EventArgs e)
        {
            SelectRate(currentSelect);
        }

        private void star5_Click(object sender, EventArgs e)
        {
            currentSelect = 5;
            SelectRate(currentSelect);
        }

        private void RatingStars_Resize(object sender, EventArgs e)
        {
            this.Width = 27 * 5;
            this.Height = 24;
        }

        private void RatingStars_Load(object sender, EventArgs e)
        {
            this.Width = 27 * 5;
            this.Height = 24;
        }
    }
}
