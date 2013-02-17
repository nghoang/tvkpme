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
    public delegate void TVKComboBoxIndexChangedEvent(object sender, int newIndex);
    public partial class TVKComboBox : UserControl
    {
        public event TVKComboBoxIndexChangedEvent IndexChanged;
        protected virtual void OnIndexChanged(int e)
        {
            IndexChanged(this, e);
        }

        List<string> itemTemp = new List<string>();
        ListBox itemList = new ListBox();
        TextBox text = new TextBox();
        PictureBox thumb = new PictureBox();
        private string filterWord = "";

        public bool ContainItem(string item)
        {
            return itemList.Items.Contains(item);
        }

        public void FilterListAddWord(string w)
        {
            filterWord += w;
            filterWordProcess();
        }

        private void filterWordProcess()
        {
            itemList.Items.Clear();
            foreach (string i in itemTemp)
            {
                if (i.ToUpper().IndexOf(filterWord.ToUpper()) >= 0)
                {
                    itemList.Items.Add(i);
                }
            }
        }

        public TVKComboBox()
        {
            InitializeComponent();

            this.Controls.Add(itemList);
            this.Controls.Add(thumb);
            this.Controls.Add(text);
            itemList.Visible = false;

            thumb.Image = global::TVKing2.TVKControlResource.scroll_down;
            thumb.Top = 1;
            thumb.Width = global::TVKing2.TVKControlResource.scroll_down.Width;
            thumb.SizeMode = PictureBoxSizeMode.StretchImage;
            thumb.Cursor = Cursors.Hand;

            thumb.MouseClick += new MouseEventHandler(thumb_MouseClick);

            text.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            text.Text = "test";
            text.BackColor = Color.Black;
            text.ForeColor = Color.DarkGray;
            text.ReadOnly = true;
            text.MouseClick += new MouseEventHandler(text_MouseClick);

            itemList.BackColor = Color.Black;
            itemList.ForeColor = Color.DarkGray;
            itemList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            itemList.Top = text.Height;
            itemList.Left = 0;

            thumb.Height = text.Height - 2;

            itemList.MouseClick += new MouseEventHandler(itemList_MouseClick);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            FilterListAddWord(keyData.ToString());
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void Clear()
        {
            itemList.Items.Clear();
            itemTemp = new List<string>();
            text.Text = "";
        }

        void text_MouseClick(object sender, MouseEventArgs e)
        {
            ShowList(!itemList.Visible);
        }

        public void SetListHeigh(int h)
        {
            itemList.Height = h;
        }

        void itemList_MouseClick(object sender, MouseEventArgs e)
        {
            ShowList(!itemList.Visible);
            text.Text = (string)itemList.SelectedItem;
            OnIndexChanged(itemList.SelectedIndex);
        }

        public void ShowList(bool show)
        {
            itemList.Visible = show;
            if (show)
            {
                filterWord = "";
                itemList.Items.Clear();
                FilterListAddWord(""); ;
                this.Height += itemList.Height;
                text.Focus();
            }
            else
                this.Height -= itemList.Height;
        }

        void thumb_MouseClick(object sender, MouseEventArgs e)
        {
            ShowList(!itemList.Visible);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            text.Top = 0;
            text.Left = 0;
            text.Width = this.Width;
            itemList.Width = this.Width;
            thumb.Left = this.Width - thumb.Width;
        }

        public void AddItem(string i)
        {
            itemList.Items.Add(i);
            itemTemp.Add(i);
        }

        public int SelectedIndex
        {
            get
            { return itemList.SelectedIndex; }
            set
            {
                itemList.SelectedIndex = value;
                if (value == -1)
                    text.Text = "";
                else
                    text.Text = (string)itemList.SelectedItems[value];
            }
        }

        public string SelectedItem
        {
            get
            { return (string)itemList.SelectedItem; }
        }

        public List<string> GetItems()
        {
            return itemTemp;
        }
    }
}
