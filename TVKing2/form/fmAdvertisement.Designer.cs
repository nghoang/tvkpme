namespace TVKing2
{
    partial class fmAdvertisement
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
            this.ctlAdvertisement1 = new TVKing2.ctlAdvertisement();
            this.SuspendLayout();
            // 
            // ctlAdvertisement1
            // 
            this.ctlAdvertisement1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ctlAdvertisement1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlAdvertisement1.Location = new System.Drawing.Point(0, 0);
            this.ctlAdvertisement1.Name = "ctlAdvertisement1";
            this.ctlAdvertisement1.Size = new System.Drawing.Size(359, 377);
            this.ctlAdvertisement1.TabIndex = 0;
            // 
            // fmAdvertisement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 377);
            this.Controls.Add(this.ctlAdvertisement1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmAdvertisement";
            this.ShowIcon = false;
            this.Text = "Advertisement Dialog";
            this.ResumeLayout(false);

        }

        #endregion

        private ctlAdvertisement ctlAdvertisement1;
    }
}