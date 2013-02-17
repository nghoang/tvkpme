namespace TVKing2
{
    partial class fmUpdate
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
            this.barDownload = new System.Windows.Forms.ProgressBar();
            this.tvkLabel1 = new TVKing2.TVKLabel();
            this.btDownload = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // barDownload
            // 
            this.barDownload.Location = new System.Drawing.Point(200, 58);
            this.barDownload.Margin = new System.Windows.Forms.Padding(4);
            this.barDownload.Name = "barDownload";
            this.barDownload.Size = new System.Drawing.Size(384, 31);
            this.barDownload.Step = 1;
            this.barDownload.TabIndex = 1;
            // 
            // tvkLabel1
            // 
            this.tvkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvkLabel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.tvkLabel1.Location = new System.Drawing.Point(13, 12);
            this.tvkLabel1.Name = "tvkLabel1";
            this.tvkLabel1.Size = new System.Drawing.Size(97, 38);
            this.tvkLabel1.TabIndex = 5;
            this.tvkLabel1.TabStop = false;
            this.tvkLabel1.Text = "Update";
            this.tvkLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // btDownload
            // 
            this.btDownload.Location = new System.Drawing.Point(13, 58);
            this.btDownload.LookAndFeel.SkinName = "TVKingSkin";
            this.btDownload.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btDownload.Name = "btDownload";
            this.btDownload.Size = new System.Drawing.Size(180, 31);
            this.btDownload.TabIndex = 6;
            this.btDownload.Text = "Download and Update";
            this.btDownload.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // fmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 104);
            this.Controls.Add(this.btDownload);
            this.Controls.Add(this.tvkLabel1);
            this.Controls.Add(this.barDownload);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "fmUpdate";
            this.ShowIcon = false;
            this.Text = "Update";
            this.Load += new System.EventHandler(this.fmUpdate_Load);
            this.Controls.SetChildIndex(this.barDownload, 0);
            this.Controls.SetChildIndex(this.tvkLabel1, 0);
            this.Controls.SetChildIndex(this.btDownload, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar barDownload;
        private TVKLabel tvkLabel1;
        private DevExpress.XtraEditors.SimpleButton btDownload;
    }
}