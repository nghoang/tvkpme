namespace TVKing2
{
    partial class TVKInfo
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
            this.pbRight = new System.Windows.Forms.PictureBox();
            this.pbLeft = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).BeginInit();
            this.SuspendLayout();
            // 
            // pbRight
            // 
            this.pbRight.BackColor = System.Drawing.Color.Transparent;
            this.pbRight.Image = global::TVKing2.TVKControlResource.info_last;
            this.pbRight.Location = new System.Drawing.Point(66, 21);
            this.pbRight.Name = "pbRight";
            this.pbRight.Size = new System.Drawing.Size(9, 120);
            this.pbRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbRight.TabIndex = 1;
            this.pbRight.TabStop = false;
            // 
            // pbLeft
            // 
            this.pbLeft.BackColor = System.Drawing.Color.Transparent;
            this.pbLeft.Image = global::TVKing2.TVKControlResource.info_first;
            this.pbLeft.Location = new System.Drawing.Point(116, 21);
            this.pbLeft.Name = "pbLeft";
            this.pbLeft.Size = new System.Drawing.Size(9, 120);
            this.pbLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLeft.TabIndex = 2;
            this.pbLeft.TabStop = false;
            // 
            // TVKInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TVKing2.TVKControlResource.background;
            this.Controls.Add(this.pbLeft);
            this.Controls.Add(this.pbRight);
            this.Name = "TVKInfo";
            this.Resize += new System.EventHandler(this.TVKInfo_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeft)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbRight;
        private System.Windows.Forms.PictureBox pbLeft;
    }
}
