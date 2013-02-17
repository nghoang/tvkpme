using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Net;
using System.Collections.Specialized;
using CrawlerLib.Net;

namespace TVKing2
{
    class RegisterCheck
    {
        //public RegisterType type = AppConst.appType;
        public string keyLocation = "SOFTWARE\\RegisteredApplications";
        public string keyName = "rcj";
        public string register_str = "101";
        public IRegisterCheck callback;


        public void UnRegister()
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
            if (key != null)
            {
                key.DeleteSubKey(keyName);
                key.DeleteSubKey(keyName + "d");
            }
        }

        public bool IsRegister()
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
            if (key == null)
                return false;
            else
                if ((string)key.GetValue(keyName) == register_str)
                    return true;
            return false;
        }

        public int GetDayLeft()
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
            string initDate = (string)key.GetValue(keyName + "d");
            string lastDate = (string)key.GetValue(keyName + "l");
            if (initDate == null || initDate == "")
            {
                key.SetValue(keyName + "d", Utility.EncodeTo64(DateTime.Now.ToLongDateString()));
                return AppConst.trial_day;
            }
            initDate = Utility.DecodeTo64(initDate);
            lastDate = Utility.DecodeTo64(lastDate);
            DateTime curD = DateTime.Now;
            DateTime preD = DateTime.Parse(initDate);
            DateTime lastD = DateTime.Parse(lastDate);
            if (curD.Subtract(lastD).TotalDays < 0)
                return 0;
            key.SetValue(keyName + "l", Utility.EncodeTo64(DateTime.Now.ToLongDateString()));

            int res = AppConst.trial_day - (int)curD.Subtract(preD).TotalDays;
            if (res < 0)
                res = 0;
            return res;
        }

        public int GetTimeLeft()
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
            string leftTimeStr = (string)key.GetValue(keyName + "hrs");
            if (leftTimeStr == null || leftTimeStr == "")
            {
                int totalTime = AppConst.TrialHours * 60 * 60;
                key.SetValue(keyName + "hrs", Utility.EncodeTo64(totalTime.ToString()));
                return totalTime;
            }

            int leftTime = int.Parse(Utility.DecodeTo64(leftTimeStr));

            return leftTime;
        }

        private void resetSetTime()
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
            string leftTimeStr = (string)key.GetValue(keyName + "hrs");
            int totalTime = AppConst.TrialHours * 60 * 60;
            key.SetValue(keyName + "hrs", Utility.EncodeTo64(totalTime.ToString()));
        }

        public void SetTimeUsed()
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
            string leftTimeStr = (string)key.GetValue(keyName + "hrs");
            if (leftTimeStr == null || leftTimeStr == "")
            {
                int totalTime = AppConst.TrialHours * 60 * 60;
                key.SetValue(keyName + "hrs", Utility.EncodeTo64(totalTime.ToString()));
                return;
            }

            int leftTime = int.Parse(Utility.DecodeTo64(leftTimeStr)) - 10;
            key.SetValue(keyName + "hrs", Utility.EncodeTo64(leftTime.ToString()));
        }

        private void GetRegisterDateInfo()
        {

        }

        private bool _CheckingExpiredHoursDaily()
        {
            DateTime checking_date;
            DateTime current_date;
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
            string temp = "";
            int watched_time = 0;

            //get checking date
            //this is the date when we checking limited hours to watch
            temp = (string)key.GetValue(keyName + "cd");
            if (temp == "")
                checking_date = DateTime.Now;
            else
            {
                DateTime.TryParse(temp, out checking_date);
            }

            //get current date
            current_date = DateTime.Now;

            //check if user reverses clock
            if (current_date < checking_date)
                return true;

            // if not in same date, meaning that he just watches
            if (checking_date.Year.ToString() + checking_date.Month.ToString() + checking_date.Day.ToString() != current_date.Year.ToString() + current_date.Month.ToString() + current_date.Day.ToString())
            {
                //we should update registry to count watched time again
                key.SetValue(keyName + "wt", "0");
                key.SetValue(keyName + "cd", current_date.ToLocalTime());

                //write current date to registry (this is used to check for exceed time or not in other function)
                key.SetValue(keyName + "cd", DateTime.Now.ToLocalTime());
                callback.FinishedSetTimeHourDaily(0);
                return false;
            }
            else
            {
                //get number of seconds watching movies (today)
                temp = (string)key.GetValue(keyName + "wt");
                if (temp != null)
                    watched_time = int.Parse(temp);


                callback.FinishedSetTimeHourDaily(AppConst.MinsPerDay * 60 - watched_time);
                //otherwise checking if exceeding limited
                if (watched_time > AppConst.MinsPerDay * 60)
                    return true;
                else
                    return false;
            }
        }

        private bool _CheckingExpiredTrial()
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
            if ((string)key.GetValue(keyName) == register_str)
                return false;
            string lastDate = (string)key.GetValue(keyName + "l");
            string initDate = (string)key.GetValue(keyName + "d");
            if (initDate == null || initDate == "")
            {
                key.SetValue(keyName + "d", Utility.EncodeTo64(DateTime.Now.ToLongDateString()));
                key.SetValue(keyName + "l", Utility.EncodeTo64(DateTime.Now.ToLongDateString()));
                return false;
            }
            lastDate = Utility.DecodeTo64(lastDate);
            initDate = Utility.DecodeTo64(initDate);
            DateTime curD = DateTime.Now;
            DateTime preD = DateTime.Parse(initDate);
            DateTime lastD = DateTime.Parse(lastDate);
            if (curD.Subtract(lastD).TotalDays < 0)
                return true;
            key.SetValue(keyName + "l", Utility.EncodeTo64(DateTime.Now.ToLongDateString()));
            if ((int)curD.Subtract(preD).TotalDays > AppConst.trial_day)
                return true;

            return false;
        }

        public bool IsExpired()
        {
            switch (AppConst.appType)
            {
                case RegisterType.Ads:
                    break;
                case RegisterType.Free:
                    break;
                case RegisterType.Hours:
                    break;
                case RegisterType.HoursDaily:
                    return this._CheckingExpiredHoursDaily();
                case RegisterType.Trial:
                    return this._CheckingExpiredTrial();
            }
            return false;
        }

        public bool Register3(string email)
        {
            WebclientX client = new WebclientX();
            NameValueCollection pars = new NameValueCollection();
            pars.Add("action", "free_register");
            pars.Add("id", Utility.EncodeTo64(email));
            string res = client.PostMethod("http://tvking.tv/tvking_services.php", pars);
            if (res.Trim() == "1")
            {
                resetSetTime();
                RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
                key.SetValue(keyName, register_str);
                return true;
            }
            return false;
        }

        public bool Register2(string email, string code)
        {
            WebClient client = new WebClient();
            NameValueCollection data = new NameValueCollection();
            data.Add("action", "MakePayment");
            data.Add("email", email);
            data.Add("license", code);

            byte[] res = client.UploadValues(AppConst.ServerURL, "POST", data);
            System.Text.Encoding enc = System.Text.Encoding.ASCII;
            string regResult = enc.GetString(res);
            if (regResult == "1")
            {
                resetSetTime();
                RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
                key.SetValue(keyName, register_str);
                return true;
            }
            return false;
        }

        public bool Register(string email, string code)
        {
            string hash = email;

            hash = hash.Replace("1", "m");
            hash = hash.Replace("2", "j");
            hash = hash.Replace("3", "u");
            hash = hash.Replace("4", "k");
            hash = hash.Replace("5", "i");
            hash = hash.Replace("6", "l");
            hash = hash.Replace("7", "o");
            hash = hash.Replace("8", "p");
            hash = hash.Replace("9", "n");
            hash = hash.Replace("0", "h");
            hash = hash.Replace("a", "y");
            hash = hash.Replace("s", "s");
            hash = hash.Replace("d", "g");
            hash = hash.Replace("z", "v");
            hash = hash.Replace("x", "f");
            hash = hash.Replace("c", "e");
            hash = hash.Replace("q", "d");
            hash = hash.Replace("w", "c");
            hash = hash.Replace("e", "x");
            hash = hash.Replace("r", "s");
            hash = hash.Replace("f", "w");
            hash = hash.Replace("v", "!");
            hash = hash.Replace("t", "$");
            hash = hash.Replace("g", "^");
            hash = hash.Replace("b", "&");
            hash = hash.Replace("y", "*");
            hash = hash.Replace("h", "(");
            hash = hash.Replace("n", "$");
            hash = hash.Replace("u", "x");
            hash = hash.Replace("j", "z");
            hash = hash.Replace("m", "Q");
            hash = hash.Replace("i", "~");
            hash = hash.Replace("k", "t");
            hash = hash.Replace("o", "h");
            hash = hash.Replace("l", "g");
            hash = hash.Replace("p", "i");

            hash = Utility.md5String(hash);

            hash = hash.Replace("0", "f");
            hash = hash.Replace("1", "c");
            hash = hash.Replace("2", "r");
            hash = hash.Replace("3", "w");
            hash = hash.Replace("4", "q");
            hash = hash.Replace("5", "y");
            hash = hash.Replace("6", "u");
            hash = hash.Replace("7", "n");
            hash = hash.Replace("8", "v");
            hash = hash.Replace("9", "m");

            if (hash.ToUpper() == code.ToUpper())
            {
                RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
                key.SetValue(keyName, register_str);
                return true;
            }
            return false;
        }

        internal void SetTimeUsedHourDaily()
        {
            RegistryKey key = Registry.LocalMachine.CreateSubKey(keyLocation);
            string temp;
            int watched_time = 0;

            //get watched time
            temp = (string)key.GetValue(keyName + "wt");
            if (temp != null)
                watched_time = int.Parse(temp);

            //increase 10 seconds (because timer of each check is 10 seconds)
            watched_time += 10;

            //then write to registry
            key.SetValue(keyName + "wt", watched_time.ToString());

        }
    }

    public enum RegisterType
    {
        Trial,
        Ads,
        Hours,
        Free,
        HoursDaily
    }
}
