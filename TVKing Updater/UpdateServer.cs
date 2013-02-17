using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.ComponentModel;

namespace TVKing_Updater
{
    class UpdateServer
    {
        WebClient client = new WebClient();
        IFormDownload callback;
        private string setup_path = "";
        private string server_path = "http://tvking.tv/Setup.exe";

        public UpdateServer(IFormDownload main)
        {
            callback = main;
            System.Guid desiredGuid = System.Guid.NewGuid();
            setup_path = Path.GetTempPath() + desiredGuid.ToString() + ".exe";
        }

        public void DownloadSetUpFile()
        {
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            try
            {
                System.IO.File.Delete(setup_path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            client.DownloadFileAsync(new Uri(server_path), setup_path);
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double p = e.BytesReceived * 100 / e.TotalBytesToReceive;
            callback.download_progress((int)p);
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            callback.finished_download(setup_path);
        }

    }
}
