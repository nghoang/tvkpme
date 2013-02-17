namespace TVKing2
{
    partial class FormYoutube
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxKeyword = new System.Windows.Forms.TextBox();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.tvkLabel1 = new TVKing2.TVKLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxSort = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxUploaded = new System.Windows.Forms.ComboBox();
            this.barSearching = new System.Windows.Forms.ProgressBar();
            this.btSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btStop = new DevExpress.XtraEditors.SimpleButton();
            this.listBoxHistory = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxKeyword
            // 
            this.textBoxKeyword.Location = new System.Drawing.Point(85, 206);
            this.textBoxKeyword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxKeyword.Name = "textBoxKeyword";
            this.textBoxKeyword.Size = new System.Drawing.Size(277, 22);
            this.textBoxKeyword.TabIndex = 0;
            this.textBoxKeyword.TextChanged += new System.EventHandler(this.textBoxKeyword_TextChanged);
            this.textBoxKeyword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxKeyword_KeyUp);
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Items.AddRange(new object[] {
            "All",
            "Cars & Vehicles",
            "Comedy",
            "Education ",
            "Entertainment",
            "Film & Animation",
            "Gaming",
            "Howto & Style",
            "News & Politics",
            "Non-profits & Activism",
            "People & Blogs",
            "Pets & Animals",
            "Science & Technology",
            "Sport",
            "Travel & Events"});
            this.comboBoxCategory.Location = new System.Drawing.Point(85, 114);
            this.comboBoxCategory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(277, 24);
            this.comboBoxCategory.TabIndex = 2;
            // 
            // tvkLabel1
            // 
            this.tvkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvkLabel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tvkLabel1.Location = new System.Drawing.Point(12, 10);
            this.tvkLabel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvkLabel1.Name = "tvkLabel1";
            this.tvkLabel1.Size = new System.Drawing.Size(161, 38);
            this.tvkLabel1.TabIndex = 6;
            this.tvkLabel1.TabStop = false;
            this.tvkLabel1.Text = "Youtube Search";
            this.tvkLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label1.Location = new System.Drawing.Point(15, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Category";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label2.Location = new System.Drawing.Point(19, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Keyword";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label3.Location = new System.Drawing.Point(15, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(259, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Search for video streams from Youtube.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(15, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(298, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Application will stream each video one by one.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label5.Location = new System.Drawing.Point(23, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Sort By";
            // 
            // comboBoxSort
            // 
            this.comboBoxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSort.FormattingEnabled = true;
            this.comboBoxSort.Items.AddRange(new object[] {
            "Relevance",
            "Upload date",
            "View count",
            "Rating "});
            this.comboBoxSort.Location = new System.Drawing.Point(85, 145);
            this.comboBoxSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxSort.Name = "comboBoxSort";
            this.comboBoxSort.Size = new System.Drawing.Size(277, 24);
            this.comboBoxSort.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label6.Location = new System.Drawing.Point(12, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Uploaded";
            // 
            // comboBoxUploaded
            // 
            this.comboBoxUploaded.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUploaded.FormattingEnabled = true;
            this.comboBoxUploaded.Items.AddRange(new object[] {
            "All",
            "Cars & Vehicles",
            "Comedy",
            "Education ",
            "Entertainment",
            "Film & Animation",
            "Gaming",
            "Howto & Style",
            "News & Politics",
            "Non-profits & Activism",
            "People & Blogs",
            "Pets & Animals",
            "Science & Technology",
            "Sport",
            "Travel & Events"});
            this.comboBoxUploaded.Location = new System.Drawing.Point(85, 175);
            this.comboBoxUploaded.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxUploaded.Name = "comboBoxUploaded";
            this.comboBoxUploaded.Size = new System.Drawing.Size(277, 24);
            this.comboBoxUploaded.TabIndex = 13;
            // 
            // barSearching
            // 
            this.barSearching.Location = new System.Drawing.Point(12, 245);
            this.barSearching.Margin = new System.Windows.Forms.Padding(4);
            this.barSearching.Name = "barSearching";
            this.barSearching.Size = new System.Drawing.Size(279, 17);
            this.barSearching.Step = 1;
            this.barSearching.TabIndex = 15;
            this.barSearching.Visible = false;
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(264, 243);
            this.btSearch.LookAndFeel.SkinName = "TVKingSkin";
            this.btSearch.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(98, 22);
            this.btSearch.TabIndex = 80;
            this.btSearch.Text = "Search";
            this.btSearch.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(298, 243);
            this.btStop.LookAndFeel.SkinName = "TVKingSkin";
            this.btStop.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(64, 22);
            this.btStop.TabIndex = 81;
            this.btStop.Text = "Stop";
            this.btStop.Visible = false;
            this.btStop.Click += new System.EventHandler(this.simpleButton1_Click_1);
            // 
            // listBoxHistory
            // 
            this.listBoxHistory.ContextMenuStrip = this.contextMenuStrip1;
            this.listBoxHistory.FormattingEnabled = true;
            this.listBoxHistory.ItemHeight = 16;
            this.listBoxHistory.Location = new System.Drawing.Point(368, 69);
            this.listBoxHistory.Name = "listBoxHistory";
            this.listBoxHistory.Size = new System.Drawing.Size(215, 196);
            this.listBoxHistory.TabIndex = 82;
            this.listBoxHistory.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listBoxHistory_MouseClick);
            this.listBoxHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxHistory_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 50);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Location = new System.Drawing.Point(365, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 17);
            this.label7.TabIndex = 83;
            this.label7.Text = "Previous search terms";
            // 
            // FormYoutube
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 277);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listBoxHistory);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.barSearching);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxUploaded);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxSort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvkLabel1);
            this.Controls.Add(this.comboBoxCategory);
            this.Controls.Add(this.textBoxKeyword);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormYoutube";
            this.Text = "FormYoutube";
            this.Load += new System.EventHandler(this.FormYoutube_Load);
            this.Controls.SetChildIndex(this.textBoxKeyword, 0);
            this.Controls.SetChildIndex(this.comboBoxCategory, 0);
            this.Controls.SetChildIndex(this.tvkLabel1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.comboBoxSort, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.comboBoxUploaded, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.barSearching, 0);
            this.Controls.SetChildIndex(this.btSearch, 0);
            this.Controls.SetChildIndex(this.btStop, 0);
            this.Controls.SetChildIndex(this.listBoxHistory, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxKeyword;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private TVKLabel tvkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxSort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxUploaded;
        private System.Windows.Forms.ProgressBar barSearching;
        private DevExpress.XtraEditors.SimpleButton btSearch;
        private DevExpress.XtraEditors.SimpleButton btStop;
        private System.Windows.Forms.ListBox listBoxHistory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}