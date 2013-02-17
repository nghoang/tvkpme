namespace TVKing2
{
    partial class FormSubmitUrl
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
            this.txtTitle = new TVKing2.TVKLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxChannelUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listboxCountry = new System.Windows.Forms.ComboBox();
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
            this.txtTitle.Size = new System.Drawing.Size(235, 38);
            this.txtTitle.TabIndex = 12;
            this.txtTitle.TabStop = false;
            this.txtTitle.Text = "Submit Channel";
            this.txtTitle.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 14;
            this.label2.Text = "Channel Url:";
            // 
            // textBoxChannelUrl
            // 
            this.textBoxChannelUrl.Location = new System.Drawing.Point(109, 63);
            this.textBoxChannelUrl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxChannelUrl.Name = "textBoxChannelUrl";
            this.textBoxChannelUrl.Size = new System.Drawing.Size(333, 22);
            this.textBoxChannelUrl.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(40, 100);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "Country:";
            // 
            // listboxCountry
            // 
            this.listboxCountry.FormattingEnabled = true;
            this.listboxCountry.Location = new System.Drawing.Point(111, 94);
            this.listboxCountry.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listboxCountry.Name = "listboxCountry";
            this.listboxCountry.Size = new System.Drawing.Size(332, 24);
            this.listboxCountry.TabIndex = 78;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(177, 137);
            this.button1.LookAndFeel.SkinName = "TVKingSkin";
            this.button1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 22);
            this.button1.TabIndex = 79;
            this.button1.Text = "Submit";
            this.button1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // FormSubmitUrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 175);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listboxCountry);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxChannelUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTitle);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormSubmitUrl";
            this.Text = "FormSubmitUrl";
            this.Load += new System.EventHandler(this.FormSubmitUrl_Load);
            this.Controls.SetChildIndex(this.txtTitle, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.textBoxChannelUrl, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.listboxCountry, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TVKLabel txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxChannelUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox listboxCountry;
        private DevExpress.XtraEditors.SimpleButton button1;
    }
}