using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Navipro.SmartInventory
{

    /// <summary>
    /// Summary description for Sound.
    /// </summary>
    public class Sound
    {
        public static int SOUND_TYPE_SUCCESS = 0;
        public static int SOUND_TYPE_FAIL = 1;
        public static int SOUND_TYPE_OK = 2;
        public static int SOUND_TYPE_ERROR = 3;

        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int WCE_PlaySound(string szSound, IntPtr hMod, int flags);

        [DllImport("CoreDll.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private extern static int WCE_PlaySoundBytes(byte[] szSound, IntPtr hMod, int flags);

        private string soundFileName;
        private byte[] soundBuffer;

        private enum Flags
        {
            SND_SYNC = 0x0000, /* play synchronously (default) */
            SND_ASYNC = 0x0001, /* play asynchronously */
            SND_NODEFAULT = 0x0002, /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004, /* pszSound points to a memory file */
            SND_LOOP = 0x0008, /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010, /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004 /* name is resource name or atom */
        }

        // Construct the Sound object to play sound data from the specified file.
        public Sound(int type)
        {
            if (type == 0)
            {
                soundFileName = "\\Windows\\Remind-03.wav";
            }
            if (type == 1)
            {
                soundFileName = "\\Windows\\error.wav";
            }
            if (type == 2)
            {
                soundFileName = "\\Windows\\Remind-02.wav";
            }
            if (type == 3)
            {
                soundFileName = "\\Windows\\error.wav";
            }

            Play();
        }



        // Play the sound.
        public void Play()
        {
            // If a file name has been registered, call WCE_PlaySound, 
            // otherwise call WCE_PlaySoundBytes.
            if (soundFileName != null)
                WCE_PlaySound(soundFileName, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_FILENAME));
            else
                WCE_PlaySoundBytes(soundBuffer, IntPtr.Zero, (int)(Flags.SND_ASYNC | Flags.SND_MEMORY));
        }


    }

}
