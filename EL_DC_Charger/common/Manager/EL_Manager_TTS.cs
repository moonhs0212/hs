using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;

namespace EL_DC_Charger.Manager
{
    public class EL_Manager_TTS
    {
        private static SpeechSynthesizer speechSynthesizer = null;
        //SpeeachSynthesizer : 설치된 음성 합성 엔진의 기능에 액세스함
        public static void Speak(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            if (speechSynthesizer == null)
            {
                speechSynthesizer = new SpeechSynthesizer();
                speechSynthesizer.SetOutputToDefaultAudioDevice();
                speechSynthesizer.SelectVoice("Microsoft Heami Desktop");
            }

            speechSynthesizer.SpeakAsync(text);
        }
        public static void ChSpeak(int channelindex, string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            try
            {
                string str = string.Empty;
                switch (channelindex)
                {
                    case 1:
                        str = "첫번째채널";
                        break;
                    case 2:
                        str = "두번째채널";
                        break;
                    case 3:
                        str = "세번째채널";
                        break;
                    case 4:
                        str = "네번째채널";
                        break;
                    default:
                        return;
                }

                if (speechSynthesizer == null)
                {
                    speechSynthesizer = new SpeechSynthesizer();
                    speechSynthesizer.SetOutputToDefaultAudioDevice();
                    speechSynthesizer.SelectVoice("Microsoft Heami Desktop");
                }

                speechSynthesizer.SpeakAsync(string.Format(text, str));
            }
            catch (Exception ex)
            {
            }
        }

    }
}
