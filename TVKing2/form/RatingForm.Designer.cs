namespace TVKing2
{
    partial class RatingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RatingForm));
            this.txtTitle = new TVKing2.TVKLabel();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtComments = new System.Windows.Forms.RichTextBox();
            this.ratingStars1 = new TVKing2.RatingStars();
            this.button1 = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.txtTitle.Location = new System.Drawing.Point(15, 12);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(251, 38);
            this.txtTitle.TabIndex = 10;
            this.txtTitle.TabStop = false;
            this.txtTitle.Text = "Rating";
            this.txtTitle.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // txtComment
            // 
            this.txtComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComment.Location = new System.Drawing.Point(16, 90);
            this.txtComment.Margin = new System.Windows.Forms.Padding(4);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(524, 80);
            this.txtComment.TabIndex = 12;
            this.txtComment.Text = "Your comment";
            this.txtComment.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtComment_MouseClick);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(16, 58);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(524, 22);
            this.txtName.TabIndex = 13;
            this.txtName.Text = "Your name";
            this.txtName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox2_MouseClick);
            // 
            // txtComments
            // 
            this.txtComments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComments.Location = new System.Drawing.Point(15, 214);
            this.txtComments.Margin = new System.Windows.Forms.Padding(4);
            this.txtComments.Name = "txtComments";
            this.txtComments.ReadOnly = true;
            this.txtComments.Size = new System.Drawing.Size(527, 380);
            this.txtComments.TabIndex = 14;
            this.txtComments.Text = "";
            // 
            // ratingStars1
            // 
            this.ratingStars1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ratingStars1.BackgroundImage")));
            this.ratingStars1.Location = new System.Drawing.Point(15, 178);
            this.ratingStars1.Margin = new System.Windows.Forms.Padding(5);
            this.ratingStars1.Name = "ratingStars1";
            this.ratingStars1.Size = new System.Drawing.Size(135, 24);
            this.ratingStars1.TabIndex = 15;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(405, 178);
            this.button1.LookAndFeel.SkinName = "TVKingSkin";
            this.button1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 28);
            this.button1.TabIndex = 104;
            this.button1.Text = "Rate This Stream";
            this.button1.Click += new System.EventHandler(this.buttonBackYoutube_Click);
            // 
            // RatingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 609);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ratingStars1);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.txtTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RatingForm";
            this.Text = "RatingForm";
            this.Load += new System.EventHandler(this.RatingForm_Load);
            this.Controls.SetChildIndex(this.txtTitle, 0);
            this.Controls.SetChildIndex(this.txtComment, 0);
            this.Controls.SetChildIndex(this.txtName, 0);
            this.Controls.SetChildIndex(this.txtComments, 0);
            this.Controls.SetChildIndex(this.ratingStars1, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TVKLabel txtTitle;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.RichTextBox txtComments;
        private RatingStars ratingStars1;
        private DevExpress.XtraEditors.SimpleButton button1;
    }
}