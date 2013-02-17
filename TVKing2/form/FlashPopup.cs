using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TVKing2
{
    public partial class FlashPopup : Form
    {
        public FlashPopup()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect // x-coordinate of upper-left corner
        , 
        int nTopRect // y-coordinate of upper-left corner
        , 
        int nRightRect // x-coordinate of lower-right corner
        , 
        int nBottomRect // y-coordinate of lower-right corner
        , 
        int nWidthEllipse // height of ellipse
        , 
        int nHeightEllipse // width of ellipse
        );

        private void FlashPopup_Load(object sender, EventArgs e)
        {
            this.Width = 549;
            this.Height = 175;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 16, 16));
            this.Text = "TVKing - Version: " + AppConst.Version;
        }
    }
}
