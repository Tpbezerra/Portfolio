using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MulProAssignment1
{
    public class PlayMusic
    {
        MusicPlayer player;

        public void CreatePlayer()
        {
            player = new MusicPlayer();
        }

        public void Open(string filePath)
        {
            player.Open(filePath);
        }

        public void Play()
        {
            player.Play();
        }

        public void Pause()
        {
            player.Stop();
        }
    }
}
