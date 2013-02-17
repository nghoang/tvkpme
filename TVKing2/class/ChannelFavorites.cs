using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrawlerLib.Net;

namespace TVKing2
{
    class ChannelFavorites
    {
        private string favoriteFile = Application.StartupPath + "\\fav2.txt";
        private string favoriteYoutubeFile = Application.StartupPath + "\\fav3.txt";
        public List<Channel> favList = new List<Channel>();
        public List<YoutubeObject> favYoutubeList = new List<YoutubeObject>();

        private void InitFav()
        {
            if (System.IO.File.Exists(favoriteFile) == false)
            {
                Utility.WriteFile(favoriteFile, "<fs></fs>", false);
            }
            if (System.IO.File.Exists(favoriteYoutubeFile) == false)
            {
                Utility.WriteFile(favoriteYoutubeFile, "<fs></fs>", false);
            }
        }

        public void LoadFavorite()
        {
            InitFav();
            string res = Utility.ReadFile(favoriteFile);
            List<string> ids = Utility.ReadAttribueXpath(res, "/fs/f", "id");
            List<string> names = Utility.ReadAttribueXpath(res, "/fs/f", "name");
            List<string> webs = Utility.ReadAttribueXpath(res, "/fs/f", "web");
            List<string> streams = Utility.ReadAttribueXpath(res, "/fs/f", "stream");
            List<string> countries = Utility.ReadAttribueXpath(res, "/fs/f", "country");
            List<string> channel_types = Utility.ReadAttribueXpath(res, "/fs/f", "channel_type");
            List<string> image_urls = Utility.ReadAttribueXpath(res, "/fs/f", "image_url");
            List<string> descriptions = Utility.ReadAttribueXpath(res, "/fs/f", "description");
            List<string> is_streams = Utility.ReadAttribueXpath(res, "/fs/f", "is_stream");
            favList.Clear();
            for (int i = 0; i < ids.Count; i++)
            {
                Channel ch = new Channel();
                ch.id = Utility.URLDecode(ids[i]);
                ch.name = Utility.URLDecode(names[i]);
                ch.web = Utility.URLDecode(webs[i]);
                ch.stream = Utility.URLDecode(streams[i]);
                ch.country = Utility.URLDecode(countries[i]);
                ch.channel_type = Utility.URLDecode(channel_types[i]);
                ch.image_url = Utility.URLDecode(image_urls[i]);
                ch.description = Utility.URLDecode(descriptions[i]);
                ch.is_stream = Utility.URLDecode(is_streams[i]);
                favList.Add(ch);
            }
        }

        public Boolean IsInFav(Channel ch)
        {
            foreach (Channel c in favList)
            {
                if (c.id == ch.id)
                {
                    return true;
                }
            }
            return false;
        }


        public Boolean IsInFav(string id)
        {
            foreach (Channel c in favList)
            {
                if (c.id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public void AddFavorite(Channel ch)
        {
            InitFav();
            LoadFavorite();
            foreach (Channel c in favList)
            {
                if (c.id == ch.id)
                    return;
            }
            favList.Add(ch);
            SaveFav();
        }

        public void DeleteFavorite(int index)
        {
            InitFav();
            LoadFavorite();
            favList.RemoveAt(index);
            SaveFav();
        }

        public void DeleteFavorite(Channel ch)
        {
            InitFav();
            LoadFavorite();
            for (int i = 0; i < favList.Count; i++)
            {
                if (ch.id == favList[i].id)
                {
                    favList.RemoveAt(i);
                    SaveFav();
                    return;
                }
            }
        }

        private void SaveFav()
        {
            string line = "<fs>";
            foreach (Channel c in favList)
            {
                line += "<f id=\"" + Utility.URLEncode(c.id) + "\"";
                line += " name=\"" + Utility.URLEncode(c.name) + "\"";
                line += " web=\"" + Utility.URLEncode(c.web) + "\"";
                line += " stream=\"" + Utility.URLEncode(c.stream) + "\"";
                line += " country=\"" + Utility.URLEncode(c.country) + "\"";
                line += " channel_type=\"" + Utility.URLEncode(c.channel_type) + "\"";
                line += " image_url=\"" + Utility.URLEncode(c.image_url) + "\"";
                line += " description=\"" + Utility.URLEncode(c.description) + "\"";
                line += " is_stream=\"" + Utility.URLEncode(c.is_stream) + "\" />";
            }
            line += "</fs>";
            Utility.WriteFile(favoriteFile, line, false);
        }

        private void SaveYoutubeFav()
        {
            string line = "<fs>";
            foreach (YoutubeObject c in favYoutubeList)
            {
                line += "<f youtube_url=\"" + Utility.URLEncode(c.youtube_url) + "\"";
                line += " image=\"" + Utility.URLEncode(c.image) + "\"";
                line += " author=\"" + Utility.URLEncode(c.author) + "\"";
                line += " author_url=\"" + Utility.URLEncode(c.author_url) + "\"";
                line += " view_count=\"" + Utility.URLEncode(c.view_count) + "\"";
                line += " has_uploaded_date=\"" + Utility.URLEncode(c.has_uploaded_date) + "\"";
                line += " description=\"" + Utility.URLEncode(c.description) + "\"";
                line += " title=\"" + Utility.URLEncode(c.title) + "\"";
                line += " time=\"" + Utility.URLEncode(c.time) + "\" />";
            }
            line += "</fs>";
            Utility.WriteFile(favoriteYoutubeFile, line, false);
        }

        internal void AddFavoriteYoutube(YoutubeObject youtubeObject)
        {
            favYoutubeList.Add(youtubeObject);
            SaveYoutubeFav();
            LoadFavoriteYoutube();
        }

        public void LoadFavoriteYoutube()
        {
            InitFav();
            string res = Utility.ReadFile(favoriteYoutubeFile);
            List<string> youtube_url = Utility.ReadAttribueXpath(res, "/fs/f", "youtube_url");
            List<string> image = Utility.ReadAttribueXpath(res, "/fs/f", "image");
            List<string> author = Utility.ReadAttribueXpath(res, "/fs/f", "author");
            List<string> author_url = Utility.ReadAttribueXpath(res, "/fs/f", "author_url");
            List<string> view_count = Utility.ReadAttribueXpath(res, "/fs/f", "view_count");
            List<string> has_uploaded_date = Utility.ReadAttribueXpath(res, "/fs/f", "has_uploaded_date");
            List<string> description = Utility.ReadAttribueXpath(res, "/fs/f", "description");
            List<string> title = Utility.ReadAttribueXpath(res, "/fs/f", "title");
            List<string> time = Utility.ReadAttribueXpath(res, "/fs/f", "time");
            favYoutubeList.Clear();
            for (int i = 0; i < youtube_url.Count; i++)
            {
                YoutubeObject ch = new YoutubeObject();
                ch.youtube_url = Utility.URLDecode(youtube_url[i]);
                ch.image = Utility.URLDecode(image[i]);
                ch.author = Utility.URLDecode(author[i]);
                ch.author_url = Utility.URLDecode(author_url[i]);
                ch.view_count = Utility.URLDecode(view_count[i]);
                ch.has_uploaded_date = Utility.URLDecode(has_uploaded_date[i]);
                ch.description = Utility.URLDecode(description[i]);
                ch.title = Utility.URLDecode(title[i]);
                ch.time = Utility.URLDecode(time[i]);
                favYoutubeList.Add(ch);
            }
        }
    }
}
