namespace TVKing2
{
    partial class YoutubeVideoList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YoutubeVideoList));
            this.barSearching = new System.Windows.Forms.ProgressBar();
            this.btLoadMore = new DevExpress.XtraEditors.SimpleButton();
            this.btStop = new DevExpress.XtraEditors.SimpleButton();
            this.bar = new TVKing2.TVKingScrollbar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // barSearching
            // 
            this.barSearching.Location = new System.Drawing.Point(27, 263);
            this.barSearching.Margin = new System.Windows.Forms.Padding(4);
            this.barSearching.Name = "barSearching";
            this.barSearching.Size = new System.Drawing.Size(224, 17);
            this.barSearching.Step = 1;
            this.barSearching.TabIndex = 16;
            this.barSearching.Visible = false;
            // 
            // btLoadMore
            // 
            this.btLoadMore.Location = new System.Drawing.Point(222, 219);
            this.btLoadMore.LookAndFeel.SkinName = "TVKingSkin";
            this.btLoadMore.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btLoadMore.Name = "btLoadMore";
            this.btLoadMore.Size = new System.Drawing.Size(91, 22);
            this.btLoadMore.TabIndex = 106;
            this.btLoadMore.Text = "Load More";
            this.btLoadMore.Click += new System.EventHandler(this.button1_Click);
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(258, 261);
            this.btStop.LookAndFeel.SkinName = "TVKingSkin";
            this.btStop.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(62, 22);
            this.btStop.TabIndex = 107;
            this.btStop.Text = "Stop";
            this.btStop.Visible = false;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // bar
            // 
            this.bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(41)))), ((int)(((byte)(42)))));
            this.bar.DownArrowImage = ((System.Drawing.Image)(resources.GetObject("bar.DownArrowImage")));
            this.bar.LargeChange = 0;
            this.bar.Location = new System.Drawing.Point(371, 0);
            this.bar.Maximum = 100;
            this.bar.Minimum = 0;
            this.bar.MinimumSize = new System.Drawing.Size(16, 87);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(16, 288);
            this.bar.SmallChange = 1;
            this.bar.TabIndex = 0;
            this.bar.ThumbBottomImage = ((System.Drawing.Image)(resources.GetObject("bar.ThumbBottomImage")));
            this.bar.ThumbBottomSpanImage = ((System.Drawing.Image)(resources.GetObject("bar.ThumbBottomSpanImage")));
            this.bar.ThumbMiddleImage = ((System.Drawing.Image)(resources.GetObject("bar.ThumbMiddleImage")));
            this.bar.ThumbTopImage = ((System.Drawing.Image)(resources.GetObject("bar.ThumbTopImage")));
            this.bar.ThumbTopSpanImage = ((System.Drawing.Image)(resources.GetObject("bar.ThumbTopSpanImage")));
            this.bar.UpArrowImage = ((System.Drawing.Image)(resources.GetObject("bar.UpArrowImage")));
            this.bar.Value = 0;
            this.bar.Scroll += new System.EventHandler(this.bar_Scroll);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // YoutubeVideoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.btLoadMore);
            this.Controls.Add(this.barSearching);
            this.Controls.Add(this.bar);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "YoutubeVideoList";
            this.Size = new System.Drawing.Size(387, 288);
            this.ResumeLayout(false);

        }

        #endregion

        private TVKingScrollbar bar;
        private System.Windows.Forms.ProgressBar barSearching;
        private DevExpress.XtraEditors.SimpleButton btLoadMore;
        private DevExpress.XtraEditors.SimpleButton btStop;
        private System.Windows.Forms.Timer timer1;

    }
}
