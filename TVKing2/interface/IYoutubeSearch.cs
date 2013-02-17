using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVKing2
{
    public interface IYoutubeSearch
    {
        void IYS_BeginSearching();
        void IYS_ProgressSearching(int percent);
        void IYS_FinishedSearching(List<YoutubeObject> youtube_videos);
        void IYS_Stop();
    }
}
