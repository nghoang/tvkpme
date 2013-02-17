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
    public delegate void DeTVKDefaultClick(object sender);
    public delegate void DeRatingClick(object sender, string id, string name);
    delegate void DeCallback();
    delegate void DeCallbackBool(bool b);
    
    public partial class TVKList : UserControl
    {

        public bool isStopping = false;
        public event DeTVKDefaultClick DefaultClick;
        public event DeRatingClick RatingClick;
        protected virtual void OnDefaultClick()
        {
            DefaultClick(this);
        }

        private ImageList imageList;
        public List<string> items = new List<string>();
        public List<string> itemkeys = new List<string>();
        public List<string> itemflags = new List<string>();
        public List<double> RatingValue = new List<double>();
        int marginTop = 7;
        int FirstY = 6;
        int marginLeftRating = 48;
        int marginLeftFlag = 23;
        int currentSelected = -1;
        int visibleItems = 0;
        TVKingScrollbar scrollBar = new TVKingScrollbar();
        TVKLabel txtWait = new TVKLabel();
        //TVKLabel txtDefault = new TVKLabel();
        string def_id = "";
        Color bgColor = Color.Black;
        public string[] checkedList = { };
        int RatePointing = 0;

        public string DefaultItem
        {
            set
            { def_id = value; }
        }

        public int SelectedIndex
        {
            get
            {
                return currentSelected;
            }
        }

        public void ShowWaiting(bool s)
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallbackBool(ShowWaiting),s);
                return;
            }
            txtWait.Visible = s;
        }

        public void SetImageList(ImageList i)
        {
            imageList = i;
        }

        public int CountItems()
        {
            return items.Count;
        }

        public void SelectedItem(int i)
        {
            currentSelected = i + scrollBar.Value;
            this.Invalidate();
        }

        public void Clear()
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallback(Clear));
                return;
            }

            itemflags = new List<string>();
            items = new List<string>();
            itemkeys = new List<string>();
            RatingValue = new List<double>();
            currentSelected = -1;
            this.Invalidate();
            scrollBar.Visible = false;
            scrollBar.Value = 0;
        }

        public TVKList()
        {
            InitializeComponent();

            txtWait.Text = "Loading...";
            txtWait.Font = new Font("Microsoft Sans Serif", (float)16);
            txtWait.Visible = true;
            txtWait.ForeColor = Color.Black;
            txtWait.Left = 40;
            txtWait.Width = 100;
            txtWait.Height = 40;

            //txtDefault.Text = "";
            //txtDefault.Visible = false;
            //txtDefault.Cursor = Cursors.Hand;
            //txtDefault.Left = 130;
            //txtDefault.Width = 43;
            //txtDefault.Height = 14;

            this.Resize += new EventHandler(TVKList_Resize);
            this.Controls.Add(scrollBar);
            scrollBar.Width = 17;
            scrollBar.Maximum = 1;
            scrollBar.Scroll += new EventHandler(scrollBar_Scroll);

            this.SetStyle(ControlStyles.DoubleBuffer |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.UserPaint, true);

            this.Controls.Add(txtWait);
            //this.Controls.Add(txtDefault);

            //txtDefault.MouseClick += new MouseEventHandler(txtDefault_MouseClick);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            int top = FirstY;
            int y = e.Y;
            this.Refresh();
            Graphics g = this.CreateGraphics();

            this.Cursor = Cursors.Arrow;

            for (int i = 0; i < items.Count; i++)
            {
                if (e.X >= 3 && e.X <= 3 + TVKControlResource.rate_button_hover.Width && e.Y >= top && e.Y <= top + TVKControlResource.rate_button_hover.Height)
                {
                    RatePointing = top;
                    g.DrawImage(TVKControlResource.rate_button_hover, new Point(3, top));
                    this.Cursor = Cursors.Hand;
                    break;
                }
                else
                {
                    RatePointing = 0;
                }
                top += marginTop + imageList.ImageSize.Height;
            }
        }

        //void txtDefault_MouseClick(object sender, MouseEventArgs e)
        //{
        //    OnDefaultClick();
        //}

        void scrollBar_Scroll(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        void TVKList_Resize(object sender, EventArgs e)
        {
            scrollBar.Top = 0;
            scrollBar.Left = this.Width - 17;
            scrollBar.Height = this.Height;

            txtWait.Top = this.Height / 2;
        }

        public void AddItem(string chid, string name, string flag, double rating)
        {
            itemflags.Add(flag);
            items.Add(name);
            itemkeys.Add(chid);
            RatingValue.Add(rating);
            DrawItemLastItem();

            if (items.Count > 0)
                ShowWaiting(false);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int top = FirstY;
            int y = e.Y;
            this.Refresh();
            Graphics g = this.CreateGraphics();

            for (int i = 0; i < items.Count; i++)
            {
                if (e.X >= 3 && e.X <= 3 + TVKControlResource.rate_button_hover.Width && e.Y >= top && e.Y <= top + TVKControlResource.rate_button_hover.Height)
                {
                    RatingClick(this, itemkeys[i], items[i]);
                    break;
                }
                if (top + imageList.ImageSize.Height >= y)
                {
                    currentSelected = i;
                    break;
                }
                top += marginTop + imageList.ImageSize.Height;
            }
            SelectedItem(currentSelected);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawItems(g);
        }

        public void SetBackgroundColor(Color value)
        {
            bgColor = value;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;
            if (bgColor != Color.Black)
                g.FillRectangle(new SolidBrush(bgColor), new Rectangle(0, 0, this.Width, this.Height));
            else
                g.FillRectangle(new TextureBrush(global::TVKing2.TVKControlResource.control_bg), new Rectangle(0, 0, this.Width, this.Height));
        }

        public string SelectedName()
        {
            if (currentSelected >= 0)
                return items[currentSelected];
            return "";
        }

        private void DrawItems(Graphics g)
        {
            lock (this)
            {
                try
                {
                    if (imageList != null)
                        visibleItems = (this.Height - marginTop) / (marginTop + imageList.ImageSize.Height) + 1;
                    int top = FirstY;
                    bool drawnSelected = false;
                    //Console.WriteLine("begin scroll bar: " + scrollBar.Value + " maximum scrollbar: " + scrollBar.Maximum);
                    scrollBar.Maximum = 1;
                    for (int i = scrollBar.Value; i < visibleItems + scrollBar.Value; i++)
                    {
                        if (i >= items.Count)
                            return;

                        Color textColor = Color.Black;
                        if (i == currentSelected)
                        {
                            if (drawnSelected == false)
                            {
                                drawnSelected = true;
                                g.FillRectangle(new SolidBrush(Color.FromArgb(51, 153, 255)), new Rectangle(marginLeftFlag - 2, top - 2, this.Width - 5 - marginLeftFlag, imageList.ImageSize.Height + 4));
                            }
                            textColor = Color.White;
                        }

                        if (itemkeys[i] == def_id)
                            textColor = Color.Blue;
                        Font drawFont = new Font("Microsoft Sans Serif", (float)8.25);
                        g.DrawString(items[i], drawFont, new SolidBrush(textColor), new Point(imageList.ImageSize.Width + marginLeftFlag + 2, top));
                        DrawRating(RatingValue[i], g, top);
                        if (checkedList.Contains(itemkeys[i]))
                        {
                            SizeF strSize = g.MeasureString(items[i], drawFont);
                            g.DrawImage(global::TVKing2.TVKControlResource.accept, marginLeftFlag + imageList.ImageSize.Width + 5 + strSize.Width + 5, top);
                        }

                        if (imageList.Images.ContainsKey(itemflags[i]))
                            g.DrawImage(imageList.Images[itemflags[i]], new Point(marginLeftFlag, top));
                        else
                            g.DrawImage(imageList.Images["DEFAULT"], new Point(marginLeftFlag, top));

                        if (top != RatePointing)
                            g.DrawImage(TVKControlResource.rate_button_click, new Point(3, top));
                        else
                            g.DrawImage(TVKControlResource.rate_button_hover, new Point(3, top));
                        top += marginTop + imageList.ImageSize.Height;
                        scrollBar.Maximum = items.Count - visibleItems + 1;
                        if (items.Count > visibleItems)
                            scrollBar.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    CrawlerLib.Net.Utility.WriteLog(ex.Message);
                    CrawlerLib.Net.Utility.WriteLog(ex.StackTrace);
                }
            }
        }

        private void DrawItemLastItem()
        {
            if (this.InvokeRequired == true)
            {
                this.Invoke(new DeCallback(DrawItemLastItem));
                return;
            }
            lock (this)
            {
                if (items.Count == 0)
                    return;
                visibleItems = (this.Height - marginTop) / (marginTop + imageList.ImageSize.Height) + 1;
                int top = FirstY;
                scrollBar.Visible = false;
                if (isStopping == true)
                    return;
                Graphics g = this.CreateGraphics();

                top += (marginTop + imageList.ImageSize.Height) * (items.Count - 1);

                if (items.Count > visibleItems)
                    scrollBar.Visible = true;

                if (top >= this.Height)
                    return;

                Color textColor = Color.Black;
                if (itemkeys[items.Count - 1] == def_id)
                    textColor = Color.Blue;
                g.DrawString(items[items.Count - 1], new Font("Microsoft Sans Serif", (float)8.25), new SolidBrush(textColor), new Point(imageList.ImageSize.Width + marginLeftFlag + 2, top));
                DrawRating(RatingValue[items.Count - 1], g, top);
                if (items.Count == 0)
                    return;
                if (imageList.Images.ContainsKey(itemflags[items.Count - 1]))
                    g.DrawImage(imageList.Images[itemflags[items.Count - 1]], new Point(marginLeftFlag, top));
                else
                    g.DrawImage(imageList.Images["DEFAULT"], new Point(marginLeftFlag, top));

                if (top != RatePointing)
                    g.DrawImage(TVKControlResource.rate_button_click, new Point(3, top));
                else
                    g.DrawImage(TVKControlResource.rate_button_hover, new Point(3, top));
                //visibleItems++;
                scrollBar.Maximum = items.Count - visibleItems + 1;
            }
        }

        private void DrawRating(double rateValue, Graphics g, int top)
        {
            if (rateValue == -1)
                return;
            int r = (int)rateValue;
            int i = 0;
            for (i = 0; i < r; i++)
                g.DrawImage(TVKControlResource.rating_selected, marginLeftRating + 7 * i, top + 13);
            int j = 0;
            for (j = i; j < 5; j++)
                g.DrawImage(TVKControlResource.rating_unselected, marginLeftRating + 7 * j, top + 13);
        }


        public string SelectedId()
        {
            if (currentSelected >= 0)
            {
                if (itemkeys.Count > currentSelected)
                    return itemkeys[currentSelected];
            }
            return "";
        }
    }
}
