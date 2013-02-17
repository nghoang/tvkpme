namespace TVKing2
{
    partial class TVKForm
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
            this.iconClose = new System.Windows.Forms.PictureBox();
            this.iconMaximize = new System.Windows.Forms.PictureBox();
            this.iconMinimize = new System.Windows.Forms.PictureBox();
            this.barDrag = new System.Windows.Forms.PictureBox();
            this.iconResize = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.iconClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMaximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMinimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barDrag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconResize)).BeginInit();
            this.SuspendLayout();
            // 
            // iconClose
            // 
            this.iconClose.BackColor = System.Drawing.Color.Transparent;
            this.iconClose.Image = global::TVKing2.TVKControlResource.icon_close_n;
            this.iconClose.Location = new System.Drawing.Point(441, 32);
            this.iconClose.Name = "iconClose";
            this.iconClose.Size = new System.Drawing.Size(14, 15);
            this.iconClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconClose.TabIndex = 0;
            this.iconClose.TabStop = false;
            this.iconClose.Click += new System.EventHandler(this.iconClose_Click);
            this.iconClose.MouseLeave += new System.EventHandler(this.iconClose_MouseLeave);
            this.iconClose.MouseHover += new System.EventHandler(this.iconClose_MouseHover);
            // 
            // iconMaximize
            // 
            this.iconMaximize.BackColor = System.Drawing.Color.Transparent;
            this.iconMaximize.Image = global::TVKing2.TVKControlResource.icon_maximize_n;
            this.iconMaximize.Location = new System.Drawing.Point(467, 91);
            this.iconMaximize.Name = "iconMaximize";
            this.iconMaximize.Size = new System.Drawing.Size(14, 15);
            this.iconMaximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconMaximize.TabIndex = 1;
            this.iconMaximize.TabStop = false;
            this.iconMaximize.Click += new System.EventHandler(this.iconMaximize_Click);
            this.iconMaximize.MouseLeave += new System.EventHandler(this.iconMaximize_MouseLeave);
            this.iconMaximize.MouseHover += new System.EventHandler(this.iconMaximize_MouseHover);
            // 
            // iconMinimize
            // 
            this.iconMinimize.BackColor = System.Drawing.Color.Transparent;
            this.iconMinimize.Image = global::TVKing2.TVKControlResource.icon_minimize_n;
            this.iconMinimize.Location = new System.Drawing.Point(401, 68);
            this.iconMinimize.Name = "iconMinimize";
            this.iconMinimize.Size = new System.Drawing.Size(14, 15);
            this.iconMinimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconMinimize.TabIndex = 2;
            this.iconMinimize.TabStop = false;
            this.iconMinimize.Click += new System.EventHandler(this.iconMinimize_Click);
            this.iconMinimize.MouseLeave += new System.EventHandler(this.iconMinimize_MouseLeave);
            this.iconMinimize.MouseHover += new System.EventHandler(this.iconMinimize_MouseHover);
            // 
            // barDrag
            // 
            this.barDrag.BackgroundImage = global::TVKing2.TVKControlResource.form_top;
            this.barDrag.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.barDrag.Location = new System.Drawing.Point(169, 65);
            this.barDrag.Name = "barDrag";
            this.barDrag.Size = new System.Drawing.Size(153, 41);
            this.barDrag.TabIndex = 3;
            this.barDrag.TabStop = false;
            this.barDrag.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.barDrag_MouseDoubleClick);
            this.barDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.barDrag_MouseDown);
            this.barDrag.MouseMove += new System.Windows.Forms.MouseEventHandler(this.barDrag_MouseMove);
            this.barDrag.MouseUp += new System.Windows.Forms.MouseEventHandler(this.barDrag_MouseUp);
            // 
            // iconResize
            // 
            this.iconResize.BackColor = System.Drawing.Color.Transparent;
            this.iconResize.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.iconResize.Location = new System.Drawing.Point(401, 310);
            this.iconResize.Name = "iconResize";
            this.iconResize.Size = new System.Drawing.Size(79, 68);
            this.iconResize.TabIndex = 4;
            this.iconResize.TabStop = false;
            this.iconResize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.iconResize_MouseDown);
            this.iconResize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.iconResize_MouseMove);
            this.iconResize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.iconResize_MouseUp);
            // 
            // TVKForm
            // 
            this.BackgroundImage = global::TVKing2.TVKControlResource.background;
            this.ClientSize = new System.Drawing.Size(535, 418);
            this.Controls.Add(this.iconResize);
            this.Controls.Add(this.iconMinimize);
            this.Controls.Add(this.iconMaximize);
            this.Controls.Add(this.iconClose);
            this.Controls.Add(this.barDrag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TVKForm";
            this.Load += new System.EventHandler(this.TVKForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iconClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMaximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconMinimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barDrag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconResize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox iconClose;
        private System.Windows.Forms.PictureBox iconMaximize;
        private System.Windows.Forms.PictureBox iconMinimize;
        private System.Windows.Forms.PictureBox barDrag;
        private System.Windows.Forms.PictureBox iconResize;
    }
}
