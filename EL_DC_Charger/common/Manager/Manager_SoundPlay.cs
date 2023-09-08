using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace EL_DC_Charger.common.Manager
{
    public class Manager_SoundPlay
    {
        //public enum SoundType
        //{
        //    Charging_Complete = 1,
        //    Charging_Start = 2,
        //    Connect_Connector = 3,
        //    Connecting_Connector = 4,
        //    Failed_Charger = 5,
        //    ReadCard = 6,
        //    ReturnFirst = 7,
        //    Select_Button = 8,
        //    Seperate_Connector = 9,
        //}
        public const string noti_connected_connector = "noti_connected_connector";
        public const string noti_disconnect_connector = "noti_disconnect_connector";
        public const string sound_connect_connector = "sound_connect_connector";
        public const string noti_service_connect_connector = "noti_service_connect_connector";
        public const string noti_start_charging = "noti_start_charging";
        public const string noti_stop_charging = "noti_stop_charging";
        public const string noti_use_complete = "noti_use_complete";
        static Manager_SoundPlay mMediaPlay = null;

        public static Manager_SoundPlay getInstance()
        {
            if (mMediaPlay == null)
                mMediaPlay = new Manager_SoundPlay();

            return mMediaPlay;
        }
        private WindowsMediaPlayer mWMediaPlayer = null;
        //private MediaPlayer mMediaPlayer = null;
        public Manager_SoundPlay()
        {
            mWMediaPlayer = new WindowsMediaPlayer();
            //mWMediaPlayer.settings.volume = MyApplication.getInstance().getSettingData_System().getSettingData_Int(EINDEX_SETTINGDATA_SYSTEM.SOUND_VOLUME);

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            parentDirectory = Directory.GetParent(currentDirectory).FullName;
            parentDirectory = Directory.GetParent(parentDirectory).FullName;
            //mMediaPlayer = new MediaPlayer();
            //mMediaPlayer.Volume = MyApplication.getInstance().getSettingData_System().getSettingData_Int(EINDEX_SETTINGDATA_SYSTEM.SOUND_VOLUME) / 100.0f;
        }
        string parentDirectory = "";

        public void stop() => mWMediaPlayer.controls.stop();
        public void setVolume(int volume) => mWMediaPlayer.settings.volume = volume;


        public void play(string fileName, bool roop)
        {
            mWMediaPlayer.controls.stop();

            string path = parentDirectory + @"\" + fileName + ".mp3";
            mWMediaPlayer.URL = path;

            mWMediaPlayer.settings.setMode("loop", roop);
            mWMediaPlayer.settings.autoStart = false;
            mWMediaPlayer.controls.play();
        }

        public void play(string fileName)
        {
            mWMediaPlayer.controls.stop();

            string path = parentDirectory + @"\" + fileName + ".mp3";

            mWMediaPlayer.URL = path;

            mWMediaPlayer.settings.setMode("loop", false);
            mWMediaPlayer.settings.autoStart = false;
            mWMediaPlayer.settings.volume = 100;
            //mWMediaPlayer.settings.volume = MyApplication.getInstance().getSettingData_System().getSettingData_Int(EINDEX_SETTINGDATA_SYSTEM.SOUND_VOLUME);
            mWMediaPlayer.controls.play();
        }
    }
}
