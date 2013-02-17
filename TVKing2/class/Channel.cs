using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrawlerLib.Net;

namespace TVKing2
{
    class Channel
    {
        public string id = "";
        public string name = "";
        public string web = "";
        public string stream = "";
        public string country = "";
        public string channel_type = "";
        public string image_url = "";
        public string description = "";
        public string is_stream = "";
        public double rating = 0;

        public static Channel ParseChannel(GeneralBlock c)
        {
            try
            {
                Channel ch = new Channel();
                ch.id = Utility.URLDecode(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "id").value);
                ch.name = Utility.URLDecode(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "name").value);
                ch.description = Utility.URLDecode(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "channel_description").value);
                ch.web = Utility.URLDecode(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "link").value);
                ch.country = Utility.URLDecode(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "country").value);
                ch.is_stream = Utility.URLDecode(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "stream_type").value);
                ch.stream = Utility.URLDecode(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "channel_link").value);
                ch.channel_type = Utility.URLDecode(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "video_type").value);
                ch.image_url = Utility.URLDecode(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "logo_path").value);
                if (ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "rating").value != "")
                    ch.rating = double.Parse(ParserLib.GetBlockSingle(c.innerHtml, "channel", "", "", "", "", "", "rating").value);
                return ch;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public static List<Channel> ParseList(string res)
        {
            List<Channel> list = new List<Channel>();
            //string[] lines = res.Split('\n');
            //foreach (string line in lines)
            //{
            //    Channel ch = Channel.ParseChannel(line);
            //    if (ch != null)
            //        list.Add(ch);
            //}
            List<GeneralBlock> channels = ParserLib.GetBlock(res, "channel", "", "", "", "", "", "");
            foreach (GeneralBlock channel in channels)
            {
                Channel ch = Channel.ParseChannel(channel);
                if (ch != null)
                    list.Add(ch);
            }
            return list;
        }
    }
}
