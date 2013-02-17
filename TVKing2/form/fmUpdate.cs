using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using CrawlerLib.Net;
using System.Collections.Specialized;
using System.IO;

namespace TVKing2
{
    public partial class fmUpdate : TVKForm
    {
        WebClient client = new WebClient();
        private MainApp mainApp;
        private string setup_path = "";

        public fmUpdate(MainApp mainApp)
        {
            System.Guid desiredGuid = System.Guid.NewGuid(); 
            setup_path = Path.GetTempPath() + desiredGuid.ToString() + ".exe";
            InitializeComponent();
            this.mainApp = mainApp;
        }

        private void fmUpdate_Load(object sender, EventArgs e)
        {
            string res = CheckVersion("0");
            UpdateActivity upac = new UpdateActivity(null, res);
            if (upac.act == 0)
            {
                MessageBox.Show("You are using lastest version.");
                this.Close();
                return;
            }
            else
                if (upac.act == 1)
                {
                    if (MessageBox.Show("New version " + upac.lastestVersion + " is available. Do you want to download it", "Update is available", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        btDownload.Text = "Cancel Download";
                        this.Visible = false;

                        client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                        client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                        if (Utility.IsFileExist(setup_path))
                        {
                            try
                            {
                                System.IO.File.Delete(setup_path);
                            }
                            catch
                            { }
                        }
                        //if (Utility.IsFileExist("Setup_free.exe"))
                        //{
                        //    System.IO.File.Delete("Setup_free.exe");
                        //}
                        //if (AppConst.appType == RegisterType.Ads)
                        //    client.DownloadFileAsync(new Uri("http://tvking.tv/Setup_free.exe"), "Setup_free.exe");
                        //else if (AppConst.appType == RegisterType.Trial ||
                        //    AppConst.appType == RegisterType.Hours ||
                        //    AppConst.appType == RegisterType.Free)
                        client.DownloadFileAsync(new Uri("http://tvking.tv/Setup.exe"), setup_path);
                    }
                    else
                    {
                        this.Close();
                    }
                }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double p = e.BytesReceived * 100 / e.TotalBytesToReceive;
            barDownload.Value = (int)p;
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (MessageBox.Show("Download was completed. Do you want to run the update?", "Update", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                System.Diagnostics.Process Proc = new System.Diagnostics.Process();
                //if (AppConst.appType == RegisterType.Ads)
                //    Proc.StartInfo.FileName = "Setup_free.exe";
                //else if (AppConst.appType == RegisterType.Trial || 
                //    AppConst.appType == RegisterType.Hours || 
                //    AppConst.appType == RegisterType.Free)
                Proc.StartInfo.FileName = setup_path;
                Proc.Start();
                mainApp.Close();
            }
        }

        public static string CheckVersion(string isQuiet)
        {
            WebclientX client = new WebclientX();
            NameValueCollection pars = new NameValueCollection();
            pars.Add("action", "version");
            pars.Add("quiet", isQuiet);
            pars.Add("version", Utility.EncodeTo64(AppConst.Version));
            pars.Add("type", AppConst.appType.ToString());
            pars.Add("register", Utility.md5String(Utility.ReadAppRegistry("TVKing2", "email")));
            string res = client.PostMethod("http://tvking.tv/tvking_services.php", pars);
            return res.Trim();
        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            client.CancelAsync();
        }


    }

    public class UpdateActivity
    {
        public string lastestVersion = "";
        public int act = 7;
        public UpdateActivity(MainApp main, string response)
        {
            try
            {
                if (response.Trim() != "")
                {
                    act = int.Parse(response.Split('#')[0]);
                }
            }
            catch (Exception ex)
            {
            }

            switch (act)
            {
                case 0 //current version
                :

                    if (main != null)
                        main.ShowMessage("You are using lastest version", "System notification");
                    break;
                case 8 //current version quiet
                :

                    break;
                case 1 //older version
                :

                    lastestVersion = response.Split('#')[1];
                    if (main != null)
                        main.AskUpdate();
                    break;
                case 2 //newer version
                :

                    if (main != null)
                        main.ShowMessage("You could be using illegal version. Please download the lastest version.", "System notification");
                    System.Diagnostics.Process.Start("http://www.tvking.tv");
                    if (main != null)
                        main.Close();
                    break;
                case 3 //show message
                :

                    if (main != null)
                        main.ShowMessage(response.Split('#')[1], "System notification");
                    break;
                case 4 //show message & update
                :

                    if (main != null)
                        main.ShowMessage(response.Split('#')[1], "System notification");
                    if (main != null)
                        main.AskUpdate();
                    break;
                case 5 //show message & close app
                :

                    if (main != null)
                        main.ShowMessage(response.Split('#')[1], "System notification");
                    if (main != null)
                        main.Close();
                    break;
                case 7 //error
                :

                    if (main != null)
                        main.ShowMessage("Our server currently cannot be connected", "System notification");
                    break;
                case 6 //show message & close app & open website
                :

                    if (main != null)
                        main.ShowMessage(response.Split('#')[1], "System notification");
                    System.Diagnostics.Process.Start(response.Split('#')[2]);
                    if (main != null)
                        main.Close();
                    break;
            }
        }
    }
}
