using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TVKing2
{
    class AppConst
    {
        public static string ServerURL = "http://www.tvking.tv/tvking_services.php";
        public static string Version = "1.8.10";
        public static int adsCountdown = 20;
        public static RegisterType appType = RegisterType.Free;
        public static int TrialHours = 4;
        public static int MinsPerDay = 10;
        public static int trial_day = 1;
        public static string STREAM_TYPE_FLASH = "4";
        public static string STREAM_TYPE_WMP = "1";
        public static string STREAM_TYPE_VLC = "5";
        public static string STREAM_TYPE_REAL = "2";
        public static string STREAM_TYPE_QUICK = "3";
        public static string UPDATER_PATH = "tvking_updater.exe";
    }
}
/*
 *===1.5.1=== 2011/08/26
 *- change: change broken image and copy right image
 *- bug: fix problem of adding favorite
 *- new: add facebook like
 *
 *===1.5.2=== 
 *- new: let end user adds channel
 *- new: Ask user's email
 *
 *===1.5.5=== 
 *- change: request email form before main application opens
 *
 *===1.5.6=== 
 *- change: request email form with main application in popup form
 *- fix: stop closing app when there is network exception
 *
 *===1.5.7=== 
 *- change: centerize forms
 *
 * ===1.5.8===
 * - add: show hide form resize icon
 * - add: stream from youtube
 *
 * ===1.5.10===
 * - add: list of youtube clips
 * - fix: only stream mp4 file from youtube
 *
 * ===1.5.11===
 * - add: load more youtube video button
 * - fix: next video playing in youtube
 *
 * ===1.6.6===
 * - fix green flag
 * 
 * ===1.6.12===
 * - improve performance when resize
 * - fix layout in win 7
 * 
 * ===1.7.1===
 * - fix update issue by using separated updater file
 */