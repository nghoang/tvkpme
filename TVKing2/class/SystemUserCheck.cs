using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrawlerLib.Net;
using System.Collections.Specialized;
using System.Management;

namespace TVKing2
{
    delegate void DeFinishedGetSystemUser(string content);
    class SystemUserCheck
    {
        public DeFinishedGetSystemUser deFinishedGetSystemUser;
        public string PlayingChannelId = "0";

        public void RequestToServer()
        {
            WebclientX client = new WebclientX();
            NameValueCollection ps = new NameValueCollection();
            ps.Add("action", "system_user");
            ps.Add("watching_id", PlayingChannelId);
            ps.Add("machine_key", SystemUserCheck.GetMachineKey());
            string content = client.PostMethod(AppConst.ServerURL, ps);
            deFinishedGetSystemUser.Invoke(content);
        }

        public static string GetMachineKey()
        {
            string serial = "";
            ManagementObjectSearcher searcher = new
            ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");
            foreach (ManagementObject wmi_HD in searcher.Get())
            {
                // get the hardware serial no.
                if (wmi_HD["SerialNumber"] != null)
                {
                    serial = (String)wmi_HD["SerialNumber"];
                    serial = serial.Trim();
                    if (serial != "")
                        break;
                }
            }
            if (serial == "")
            {
                serial = CrawlerLib.Net.Utility.ReadAppRegistry("TVKing2", "tvkhddkey");
                if (serial == "")
                {
                    serial = System.Guid.NewGuid().ToString();
                    CrawlerLib.Net.Utility.WriteAppRegistry("TVKing2", "tvkhddkey", serial);
                }
            }
            return serial;
        }

    }
}
