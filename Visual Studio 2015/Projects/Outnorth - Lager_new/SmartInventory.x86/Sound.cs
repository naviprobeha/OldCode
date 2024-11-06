using System;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace Navipro.SmartInventory
{


    public class Sound
    {
        public static int SOUND_TYPE_SUCCESS = 0;
        public static int SOUND_TYPE_FAIL = 1;
        public static int SOUND_TYPE_OK = 2;
        public static int SOUND_TYPE_ERROR = 3;

        public Sound(int type)
        {
            if (type == 0)
            {
                Sound.BeepBeep(1000, 500, 125);
                Sound.BeepBeep(1000, 625, 125);
                Sound.BeepBeep(1000, 750, 125);
                Sound.BeepBeep(1000, 1000, 350);
                    
            }
            if (type == 1)
            {
                //soundFileName = "\\Windows\\error.wav";
                Sound.BeepBeep(1000, 1000, 125);
                Sound.BeepBeep(1000, 500, 500);
            }
            if (type == 2)
            {
                //soundFileName = "\\Windows\\Remind-02.wav";
                Sound.BeepBeep(1000, 500, 125);
                Sound.BeepBeep(1000, 625, 125);
                Sound.BeepBeep(1000, 750, 125);
                Sound.BeepBeep(1000, 1000, 350);
            }
            if (type == 3)
            {
                //soundFileName = "\\Windows\\error.wav";
                Sound.BeepBeep(1000, 1000, 125);
                Sound.BeepBeep(1000, 500, 500);
                //Sound.BeepBeep(1000, 400, 200);
                //Sound.BeepBeep(1000, 250, 1000);
            }

            
        }


        public static void BeepBeep(int Amplitude, int Frequency, int Duration)
        {
            double A = ((Amplitude * (System.Math.Pow(2, 15))) / 1000) - 1;
            double DeltaFT = 2 * Math.PI * Frequency / 44100.0;

            int Samples = 441 * Duration / 10;
            int Bytes = Samples * 4;
            int[] Hdr = { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };
            using (MemoryStream MS = new MemoryStream(44 + Bytes))
            {
                using (BinaryWriter BW = new BinaryWriter(MS))
                {
                    for (int I = 0; I < Hdr.Length; I++)
                    {
                        BW.Write(Hdr[I]);
                    }
                    for (int T = 0; T < Samples; T++)
                    {
                        short Sample = System.Convert.ToInt16(A * Math.Sin(DeltaFT * T));
                        BW.Write(Sample);
                        BW.Write(Sample);
                    }
                    BW.Flush();
                    MS.Seek(0, SeekOrigin.Begin);
                    using (SoundPlayer SP = new SoundPlayer(MS))
                    {
                        SP.PlaySync();
                    }
                }
            }
        }
    }
}
