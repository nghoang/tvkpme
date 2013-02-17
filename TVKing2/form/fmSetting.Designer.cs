namespace TVKing2
{
    partial class fmSetting
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
            this.cbStartup = new System.Windows.Forms.CheckBox();
            this.cbShowlogo = new System.Windows.Forms.CheckBox();
            this.cbAutoplay = new System.Windows.Forms.CheckBox();
            this.tvkLabel1 = new TVKing2.TVKLabel();
            this.button3 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // cbStartup
            // 
            this.cbStartup.AutoSize = true;
            this.cbStartup.BackColor = System.Drawing.Color.Transparent;
            this.cbStartup.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbStartup.Location = new System.Drawing.Point(40, 58);
            this.cbStartup.Margin = new System.Windows.Forms.Padding(4);
            this.cbStartup.Name = "cbStartup";
            this.cbStartup.Size = new System.Drawing.Size(168, 21);
            this.cbStartup.TabIndex = 1;
            this.cbStartup.Text = "Start up with Windows";
            this.cbStartup.UseVisualStyleBackColor = false;
            this.cbStartup.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cbShowlogo
            // 
            this.cbShowlogo.AutoSize = true;
            this.cbShowlogo.BackColor = System.Drawing.Color.Transparent;
            this.cbShowlogo.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbShowlogo.Location = new System.Drawing.Point(40, 86);
            this.cbShowlogo.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowlogo.Name = "cbShowlogo";
            this.cbShowlogo.Size = new System.Drawing.Size(195, 21);
            this.cbShowlogo.TabIndex = 7;
            this.cbShowlogo.Text = "Enable show channel logo";
            this.cbShowlogo.UseVisualStyleBackColor = false;
            this.cbShowlogo.CheckedChanged += new System.EventHandler(this.cbShowlogo_CheckedChanged);
            // 
            // cbAutoplay
            // 
            this.cbAutoplay.AutoSize = true;
            this.cbAutoplay.BackColor = System.Drawing.Color.Transparent;
            this.cbAutoplay.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.cbAutoplay.Location = new System.Drawing.Point(40, 113);
            this.cbAutoplay.Margin = new System.Windows.Forms.Padding(4);
            this.cbAutoplay.Name = "cbAutoplay";
            this.cbAutoplay.Size = new System.Drawing.Size(143, 21);
            this.cbAutoplay.TabIndex = 8;
            this.cbAutoplay.Text = "Auto play channel";
            this.cbAutoplay.UseVisualStyleBackColor = false;
            this.cbAutoplay.CheckedChanged += new System.EventHandler(this.cbAutoplay_CheckedChanged);
            // 
            // tvkLabel1
            // 
            this.tvkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvkLabel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tvkLabel1.Location = new System.Drawing.Point(12, 12);
            this.tvkLabel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvkLabel1.Name = "tvkLabel1";
            this.tvkLabel1.Size = new System.Drawing.Size(211, 38);
            this.tvkLabel1.TabIndex = 9;
            this.tvkLabel1.TabStop = false;
            this.tvkLabel1.Text = "Settings";
            this.tvkLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(40, 172);
            this.button3.LookAndFeel.SkinName = "TVKingSkin";
            this.button3.LookAndFeel.UseDefaultLookAndFeel = false;
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(143, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Clear Cache";
            this.button3.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // fmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 240);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tvkLabel1);
            this.Controls.Add(this.cbStartup);
            this.Controls.Add(this.cbShowlogo);
            this.Controls.Add(this.cbAutoplay);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fmSetting";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.fmSetting_Load);
            this.Controls.SetChildIndex(this.cbAutoplay, 0);
            this.Controls.SetChildIndex(this.cbShowlogo, 0);
            this.Controls.SetChildIndex(this.cbStartup, 0);
            this.Controls.SetChildIndex(this.tvkLabel1, 0);
            this.Controls.SetChildIndex(this.button3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbStartup;
        private System.Windows.Forms.CheckBox cbShowlogo;
        private System.Windows.Forms.CheckBox cbAutoplay;
        private TVKLabel tvkLabel1;
        private DevExpress.XtraEditors.SimpleButton button3;
    }
}