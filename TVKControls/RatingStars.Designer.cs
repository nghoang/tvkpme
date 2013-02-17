namespace TVKing2
{
    partial class RatingStars
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
            this.star1 = new System.Windows.Forms.PictureBox();
            this.star2 = new System.Windows.Forms.PictureBox();
            this.star3 = new System.Windows.Forms.PictureBox();
            this.star4 = new System.Windows.Forms.PictureBox();
            this.star5 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.star1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.star2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.star3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.star4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.star5)).BeginInit();
            this.SuspendLayout();
            // 
            // star1
            // 
            this.star1.BackColor = System.Drawing.Color.Transparent;
            this.star1.Location = new System.Drawing.Point(22, 18);
            this.star1.Name = "star1";
            this.star1.Size = new System.Drawing.Size(21, 19);
            this.star1.TabIndex = 0;
            this.star1.TabStop = false;
            this.star1.Click += new System.EventHandler(this.star1_Click);
            this.star1.MouseLeave += new System.EventHandler(this.star1_MouseLeave);
            this.star1.MouseHover += new System.EventHandler(this.star1_MouseHover);
            // 
            // star2
            // 
            this.star2.BackColor = System.Drawing.Color.Transparent;
            this.star2.Location = new System.Drawing.Point(67, 18);
            this.star2.Name = "star2";
            this.star2.Size = new System.Drawing.Size(21, 19);
            this.star2.TabIndex = 1;
            this.star2.TabStop = false;
            // 
            // star3
            // 
            this.star3.BackColor = System.Drawing.Color.Transparent;
            this.star3.Location = new System.Drawing.Point(94, 18);
            this.star3.Name = "star3";
            this.star3.Size = new System.Drawing.Size(21, 19);
            this.star3.TabIndex = 2;
            this.star3.TabStop = false;
            // 
            // star4
            // 
            this.star4.BackColor = System.Drawing.Color.Transparent;
            this.star4.Location = new System.Drawing.Point(112, 92);
            this.star4.Name = "star4";
            this.star4.Size = new System.Drawing.Size(21, 19);
            this.star4.TabIndex = 3;
            this.star4.TabStop = false;
            // 
            // star5
            // 
            this.star5.BackColor = System.Drawing.Color.Transparent;
            this.star5.Location = new System.Drawing.Point(133, 128);
            this.star5.Name = "star5";
            this.star5.Size = new System.Drawing.Size(21, 19);
            this.star5.TabIndex = 4;
            this.star5.TabStop = false;
            // 
            // RatingStars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TVKing2.TVKControlResource.background;
            this.Controls.Add(this.star5);
            this.Controls.Add(this.star4);
            this.Controls.Add(this.star3);
            this.Controls.Add(this.star2);
            this.Controls.Add(this.star1);
            this.Name = "RatingStars";
            this.Size = new System.Drawing.Size(212, 170);
            this.Load += new System.EventHandler(this.RatingStars_Load);
            this.Resize += new System.EventHandler(this.RatingStars_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.star1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.star2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.star3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.star4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.star5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox star1;
        private System.Windows.Forms.PictureBox star2;
        private System.Windows.Forms.PictureBox star3;
        private System.Windows.Forms.PictureBox star4;
        private System.Windows.Forms.PictureBox star5;
    }
}
