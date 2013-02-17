using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using CrawlerLib.Net;

namespace TVKing2
{
    public delegate void DeRequestTVKServer(string res);

    class RequestTVKServer
    {

        WebClient client = new WebClient();
        public string search_key = "";
        public bool isCountrySearch;
        public bool isCategorySearch;
        public DeRequestTVKServer callbackRequest;
        public bool isStoped = false;
        public bool last_watched = false;
        public bool most_watched = false;

        public RequestTVKServer()
        {
            client.DownloadDataCompleted += new DownloadDataCompletedEventHandler(client_DownloadDataCompleted);
        }

        void client_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            string myString = "";
            try
            {
                myString = enc.GetString(e.Result);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.StackTrace);
            }
            callbackRequest.Invoke(myString);
        }

        public void PostRating(string id, string rating, string name, string comment)
        {
            WebClient clientRating = new WebClient();
            NameValueCollection data = new NameValueCollection();
            data.Add("id", id);
            data.Add("name", name);
            data.Add("rating", rating);
            data.Add("comment", comment);
            byte[] res = clientRating.UploadValues(new Uri(AppConst.ServerURL + "?action=rating"), data);
            //System.Text.Encoding enc = System.Text.Encoding.ASCII;
            //string myString = enc.GetString(res);
            //myString += "";
        }

        public string GetCategory()
        {
            return Request("?action=category");
        }

        public string GetCountry()
        {
            return Request("?action=country");
        }

        private string Request(string url_params)
        {
            try
            {
                return client.DownloadString(AppConst.ServerURL + url_params);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex.Message);
            }
            return "";
        }

        public string GetComments(string id)
        {
            return Request("?action=comments&channel_id=" + id);
        }

        public void SearchAsys()
        {
            isStoped = false;
            string res = "";
            string url = "?action=search2";
            if (isCountrySearch == true)
                url += "&country=" + Utility.URLEncode(search_key);
            else if (isCategorySearch == true)
                url += "&category=" + Utility.URLEncode(search_key);
            else
                url += "&key=" + Utility.URLEncode(search_key);

            if (most_watched == true)
                url += "&most_watched=1";
            if (last_watched == true)
                url += "&last_watched=1";

            res = Request(url);

            //if (isCountrySearch == true)
            //    res = client.DownloadString("http://tvking.tv/default_channel.php?country=" + search_key) + res;
            if (isStoped == false)
                callbackRequest.Invoke(res);
            isStoped = true;
        }

        private void RequestSysc(string url)
        {
            client.OpenReadAsync(new Uri(AppConst.ServerURL + url));
        }

        public void DownloadSync(string url, DeRequestTVKServer callback)
        {
            callbackRequest = callback;
            client.DownloadDataAsync(new Uri(AppConst.ServerURL + url));
        }

        private void RequestSyscNoResponse(string url)
        {
            client.OpenReadAsync(new Uri(AppConst.ServerURL + url));
        }

        internal void ChannelError(string p)
        {
            RequestSyscNoResponse("?action=error&channel_id=" + p);
        }

        internal void ChannelChecked(string p)
        {
            RequestSyscNoResponse("?action=checked&channel_id=" + p);
        }

        public void ReportChannelViolation(string email, string channel, string message)
        {
            NameValueCollection prms = new NameValueCollection();
            prms.Add("email", email);
            prms.Add("channel", channel);
            prms.Add("message", message);
            prms.Add("action", "copyright_violation");
            WebclientX client = new WebclientX();
            client.PostMethod(AppConst.ServerURL, prms);
        }

        public string GetConfig()
        {
            return Request("?action=tvking_config");
        }

        internal void SubmitEmail(string email)
        {
            RequestSyscNoResponse("?action=submit_email&email=" + Utility.URLEncode(email));
        }

        internal void SubmitUrl(string url, string country)
        {
            NameValueCollection prms = new NameValueCollection();
            string email = Utility.ReadAppRegistry("TVKing2", "email");
            prms.Add("user_email", email);
            prms.Add("submit_url", url);
            prms.Add("country", country);
            prms.Add("action", "user_submit_channel");
            WebclientX client = new WebclientX();
            client.PostMethod(AppConst.ServerURL, prms);
        }
    }
}
