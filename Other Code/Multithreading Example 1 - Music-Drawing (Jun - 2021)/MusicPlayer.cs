using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MulProAssignment1
{
    public class MusicPlayer
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, int hwndCallback);

        public void Open(string filePath)
        {
            string command = $"open \"{filePath}\" type MPEGVideo alias CurrentMp3";
            mciSendString(command, null, 0, 0);
        }

        public void Play()
        {
            string command = "play CurrentMp3";
            mciSendString(command, null, 0, 0);
        }

        public void Stop()
        {
            string command = "stop CurrentMp3";
            mciSendString(command, null, 0, 0);
        }
    }
}
