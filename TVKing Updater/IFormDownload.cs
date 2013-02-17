using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVKing_Updater
{
    interface IFormDownload
    {
        void download_progress(int p);
        void finished_download(string path);
    }
}
