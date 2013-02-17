using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CrawlerLib.Net;

namespace TVKing2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new TestForm());
            //return;
            bool createdNew;
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.TVKingSkin).Assembly); //Register!
            Utility.WriteLog("===========start---------" + AppConst.Version, 2000);
            System.Threading.Mutex appMutex = new System.Threading.Mutex(true, Application.ProductName, out createdNew);
            if (createdNew)
            {
                RegisterCheck reg = new RegisterCheck();
                switch (AppConst.appType)
                {
                    case RegisterType.Ads:
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        ShowMainForm();
                        break;
                    case RegisterType.Free:
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        ShowMainForm();
                        break;
                    case RegisterType.Hours:
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        ShowMainForm();
                        break;
                    case RegisterType.HoursDaily:
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        ShowMainForm();
                        break;
                    case RegisterType.Trial:
                        if (reg.IsRegister() == false && reg.IsExpired() == true)
                        {
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run(new fmRegister(true));
                        }
                        break;
                }
            }
            else
            {
                string msg = String.Format("The Program \"{0}\" is already running", Application.ProductName);
                MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static void ShowMainForm()
        {
            try
            {
                Application.Run(new MainApp());
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.StackTrace);
            }
        }

    }
}
