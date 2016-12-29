using System;
using System.Collections.Generic;
using System.Text;
using SpeechLib;

namespace Rmes.Public
{
   public  class Speech
    {
        public  void txtToSpeech(string value)
        {
            GC.Collect();
            SpeechLib.SpVoice voice = new SpVoiceClass();
            voice.Speak(value, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak );
            
        }
        public void txtToFile(string value, string fileName)
        {
            GC.Collect();
            SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
            SpeechLib.SpVoice voice = new SpVoiceClass();
            SpeechStreamFileMode SpFileMode = SpeechStreamFileMode.SSFMCreateForWrite;
            SpFileStream SpFileStream = new SpFileStream();
            SpFileStream.Open(fileName, SpFileMode, false);
            voice.AudioOutputStream = SpFileStream;
            voice.Speak(value, SpFlags);
            voice.WaitUntilDone(System.Threading.Timeout.Infinite);
            SpFileStream.Close();

        }
    }
}
