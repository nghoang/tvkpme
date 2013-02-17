namespace TVKing2
{
    partial class fmRegister
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
            this.lbLicense = new System.Windows.Forms.Label();
            this.lbEmail = new System.Windows.Forms.Label();
            this.tvkLabel1 = new TVKing2.TVKLabel();
            this.picUnlock = new System.Windows.Forms.PictureBox();
            this.txtEmail = new TVKing2.TVKingTextBox();
            this.txtKey = new TVKing2.TVKingTextBox();
            this.picRegLogo = new System.Windows.Forms.PictureBox();
            this.lbToUnlock = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picUnlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRegLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lbLicense
            // 
            this.lbLicense.AutoSize = true;
            this.lbLicense.BackColor = System.Drawing.Color.Transparent;
            this.lbLicense.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbLicense.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbLicense.Location = new System.Drawing.Point(26, 382);
            this.lbLicense.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLicense.Name = "lbLicense";
            this.lbLicense.Size = new System.Drawing.Size(89, 17);
            this.lbLicense.TabIndex = 4;
            this.lbLicense.Text = "License Key:";
            // 
            // lbEmail
            // 
            this.lbEmail.AutoSize = true;
            this.lbEmail.BackColor = System.Drawing.Color.Transparent;
            this.lbEmail.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbEmail.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbEmail.Location = new System.Drawing.Point(13, 355);
            this.lbEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbEmail.Name = "lbEmail";
            this.lbEmail.Size = new System.Drawing.Size(102, 17);
            this.lbEmail.TabIndex = 3;
            this.lbEmail.Text = "Email Address:";
            // 
            // tvkLabel1
            // 
            this.tvkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvkLabel1.ForeColor = System.Drawing.Color.White;
            this.tvkLabel1.Location = new System.Drawing.Point(11, 12);
            this.tvkLabel1.Name = "tvkLabel1";
            this.tvkLabel1.Size = new System.Drawing.Size(260, 25);
            this.tvkLabel1.TabIndex = 10;
            this.tvkLabel1.TabStop = false;
            this.tvkLabel1.Text = "TVKing - Get registration key to unlock";
            this.tvkLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // picUnlock
            // 
            this.picUnlock.Image = global::TVKing2.Properties.Resources.unlock_button;
            this.picUnlock.Location = new System.Drawing.Point(299, 378);
            this.picUnlock.Name = "picUnlock";
            this.picUnlock.Size = new System.Drawing.Size(92, 26);
            this.picUnlock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picUnlock.TabIndex = 11;
            this.picUnlock.TabStop = false;
            this.picUnlock.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.Black;
            this.txtEmail.Location = new System.Drawing.Point(115, 346);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(277, 26);
            this.txtEmail.TabIndex = 12;
            // 
            // txtKey
            // 
            this.txtKey.BackColor = System.Drawing.Color.Black;
            this.txtKey.Location = new System.Drawing.Point(115, 378);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(178, 26);
            this.txtKey.TabIndex = 13;
            // 
            // picRegLogo
            // 
            this.picRegLogo.Image = global::TVKing2.Properties.Resources.register_ad;
            this.picRegLogo.Location = new System.Drawing.Point(16, 47);
            this.picRegLogo.Name = "picRegLogo";
            this.picRegLogo.Size = new System.Drawing.Size(376, 261);
            this.picRegLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picRegLogo.TabIndex = 14;
            this.picRegLogo.TabStop = false;
            this.picRegLogo.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // lbToUnlock
            // 
            this.lbToUnlock.AutoSize = true;
            this.lbToUnlock.BackColor = System.Drawing.Color.Transparent;
            this.lbToUnlock.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbToUnlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbToUnlock.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.lbToUnlock.Location = new System.Drawing.Point(26, 318);
            this.lbToUnlock.Name = "lbToUnlock";
            this.lbToUnlock.Size = new System.Drawing.Size(362, 18);
            this.lbToUnlock.TabIndex = 15;
            this.lbToUnlock.Text = "To unlock, please enter registration info below:";
            // 
            // fmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 435);
            this.Controls.Add(this.lbToUnlock);
            this.Controls.Add(this.picRegLogo);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.picUnlock);
            this.Controls.Add(this.tvkLabel1);
            this.Controls.Add(this.lbLicense);
            this.Controls.Add(this.lbEmail);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fmRegister";
            this.ShowIcon = false;
            this.Text = "Registration Dialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fmRegister_FormClosing);
            this.Load += new System.EventHandler(this.fmRegister_Load);
            this.Resize += new System.EventHandler(this.fmRegister_Resize);
            this.Controls.SetChildIndex(this.lbEmail, 0);
            this.Controls.SetChildIndex(this.lbLicense, 0);
            this.Controls.SetChildIndex(this.tvkLabel1, 0);
            this.Controls.SetChildIndex(this.picUnlock, 0);
            this.Controls.SetChildIndex(this.txtEmail, 0);
            this.Controls.SetChildIndex(this.txtKey, 0);
            this.Controls.SetChildIndex(this.picRegLogo, 0);
            this.Controls.SetChildIndex(this.lbToUnlock, 0);
            ((System.ComponentModel.ISupportInitialize)(this.picUnlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRegLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLicense;
        private System.Windows.Forms.Label lbEmail;
        private TVKLabel tvkLabel1;
        private System.Windows.Forms.PictureBox picUnlock;
        private TVKingTextBox txtEmail;
        private TVKingTextBox txtKey;
        private System.Windows.Forms.PictureBox picRegLogo;
        private System.Windows.Forms.Label lbToUnlock;
    }
}