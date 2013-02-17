using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrawlerLib.Net;

namespace TVKing2
{
    //public delegate void DeFinishedSearchYoutube(List<YoutubeObject> youtube_videos);

    public class YoutubeSearch
    {
        public IYoutubeSearch callback;
        public static string last_query = "";
        public static int last_page = -1;
        int max_page = 1;
        public bool stoping = false;
        //public DeFinishedSearchYoutube callbackYoutubeForm;

        public void ContinueSearching()
        {
            List<YoutubeObject> youtube_videos = new List<YoutubeObject>();
            WebclientX client = new WebclientX();
            callback.IYS_BeginSearching();
            YoutubeSearch.last_page++;
            string search_content = client.GetMethod(last_query + YoutubeSearch.last_page);
            youtube_videos.AddRange(GetListYoutubeVD(search_content));
            //for (int i = last_page + 1; i < last_page + max_page; i++)
            //{
            //    if (stoping)
            //    {
            //        callback.IYS_Stop();
            //        return;
            //    }
            //    callback.IYS_ProgressSearching((i + 1) * 100 / (last_page + max_page));
            //    string search_content = client.GetMethod(last_query + i);
            //    youtube_videos.AddRange(GetListYoutubeVD(search_content));
            //}
            //YoutubeSearch.last_page = last_page +  max_page;
            callback.IYS_FinishedSearching(youtube_videos);
            //return youtube_videos;
        }

        string _cate_id;
        string _sort_id;
        string _uploaded_id;
        string _key;

        public void SearchYoutube(string cate_id, string sort_id, string uploaded_id, string key)
        {
            WebclientX client = new WebclientX();
            List<YoutubeObject> youtube_videos = new List<YoutubeObject>();
            string category = "";
            string sort = "";
            string uploaded = "";
            if (cate_id != "")
            {
                category = "search_category=" + cate_id + "&";
            }
            if (sort_id != "")
            {
                sort = "search_sort=" + sort_id + "&";
            }
            if (uploaded_id != "")
            {
                uploaded = "uploaded=" + uploaded_id + "&";
            }

            string search_link = "http://www.youtube.com/results?" + sort + uploaded + category + "search_query=" + Utility.URLEncode(key) + "&page=";
            YoutubeSearch.last_query = search_link;

            callback.IYS_BeginSearching();
            for (int i = 1; i <= max_page; i++)
            {
                YoutubeSearch.last_page = i;
                callback.IYS_ProgressSearching(i * 100 / max_page);
                if (stoping)
                {
                    callback.IYS_Stop();
                    return;
                }
                //string search_content = Utility.ReadFile(@"C:\Users\Hoang\Desktop\results.htm");//client.GetMethod(search_link + i);
                string search_content = client.GetMethod(search_link + i);
                youtube_videos.AddRange(GetListYoutubeVD(search_content));
            }
            callback.IYS_FinishedSearching(youtube_videos);
            //callbackYoutubeForm.Invoke(youtube_videos);
        }

        public static List<YoutubeObject> GetListYoutubeVD(string content)
        {
            List<YoutubeObject> youtube_videos = new List<YoutubeObject>();
            string regexBlock = @"class=""yt-uix-tile yt-lockup-list yt-tile-default yt-grid-box "">.*?(?=\sviews</span>)";
            List<string> result_blocks = Utility.SimpleRegex(regexBlock, content, 0);
            foreach (string block in result_blocks)
            {
                YoutubeObject yo = new YoutubeObject();
                yo.image = Utility.SimpleRegexSingle(@"src=""([^""]*)""\s*alt=""Thumbnail""", block, 1);
                if (yo.image.StartsWith("//"))
                    yo.image = "http:" + yo.image;
                yo.description = Utility.SimpleRegexSingle(@"-description"" dir=""ltr"">(.*?)(?=</p>)", block, 1);
                yo.youtube_url = Utility.SimpleRegexSingle(@"dir=""ltr""title=""([^""]*)""href=""([^""&]*)"">", block, 2);
                yo.title = Utility.SimpleRegexSingle(@"dir=""ltr""title=""([^""]*)""href=""([^""&]*)"">", block, 1);
                if (yo.youtube_url == "")
                    continue;
                youtube_videos.Add(yo);
            }
            return youtube_videos;
        }

        internal void SetSearchVariables(string cate_id, string sort_id, string uploaded_id, string key)
        {
             _cate_id = cate_id;
             _sort_id = sort_id;
             _uploaded_id = uploaded_id;
             _key = key;
        }

        public void SearchYoutubeASY()
        {
            SearchYoutube(_cate_id, _sort_id, _uploaded_id, _key);
        }
    }
}
