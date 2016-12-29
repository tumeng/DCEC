using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using NAudio;
using NAudio.Wave;

namespace Rmes.Public
{
    public class RSpeechLib
    {
        private SpeechSynthesizer speechsync;
        private List<string> installedvoices;
        private string currentvoice = "";
        private int volume = 75;
        private int rate = -1;
        private Prompt prompt;

        public RSpeechLib()
        {
            speechsync = new SpeechSynthesizer();
            speechsync.Rate = rate;
            speechsync.Volume = volume;
            installedvoices = this.getVoices();
            if (installedvoices.Count > 0)
            {
                currentvoice = installedvoices[0];
                speechsync.SelectVoice(currentvoice);
            }
        }
          

        public void Speak(string word, bool cancelOther = false)
        {
            if (currentvoice.Equals(string.Empty))
                throw new Exception("您的计算机上没有安装播报声音！");
            if (cancelOther) speechsync.SpeakAsyncCancelAll();
            prompt = speechsync.SpeakAsync(word);
        }

        private List<string> getVoices()
        {
            List<string> rets = new List<string>();
            foreach (InstalledVoice v in speechsync.GetInstalledVoices().ToList<InstalledVoice>())
            {
                rets.Add(v.VoiceInfo.Name);
            }
            return rets;
        }
       
        public int Volume
        {
            get { return speechsync.Volume; }
            set { speechsync.Volume = value; }
        }

        public int Rate
        {
            get { return speechsync.Rate; }
            set { speechsync.Rate = value; }
        }

        public string Voice
        {
            get { return speechsync.Voice.Name; }
            set { speechsync.SelectVoice(value); }
        }

        public List<string> InstalledVoices
        {
            get { return installedvoices; }
        }

        private NAudio.Wave.BlockAlignReductionStream stream = null;
        private NAudio.Wave.DirectSoundOut output = null;
        //private NAudio.Wave.WaveOut output = null;
        private string fileName;

        public void MP3PlayFile(string fileName)
        {
            this.fileName = fileName;
            if (fileName.EndsWith(".mp3"))
            {
                NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(fileName));
                stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            }
            else if (fileName.EndsWith(".wav"))
            {
                NAudio.Wave.WaveStream pcm = new NAudio.Wave.WaveChannel32(new NAudio.Wave.WaveFileReader(fileName));
                stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
            }
            else throw new InvalidOperationException("Not a correct audio file type.");
            output = new NAudio.Wave.DirectSoundOut();
            //output = new WaveOut();
            output.Init(stream);
            output.Play();
            //output.Volume = 1.0f;
            
        }
        public void MP3Volume(float vol)
        {
            //output.Volume = vol;
            //output.Volume = vol;
        }
        public void MP3PausePlay()
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Pause();
                else if (output.PlaybackState == NAudio.Wave.PlaybackState.Paused) output.Play();
            }
        }
        public bool MP3isPlaying
        { 
            get{
                if (output == null) 
                    return false;
                else
                    return (output.PlaybackState == NAudio.Wave.PlaybackState.Playing);
            }
        }
        public void MP3Pause()
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Pause();
            }
        }
        public void MP3Play()
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Paused) output.Play();
            }
        }
        public void MP3CloseFile()
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Stop();
                output.Dispose();
                output = null;
            }
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }
    }
}
