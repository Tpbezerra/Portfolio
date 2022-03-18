using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment2_Form
{
    public class Reader
    {
        CharacterBuffer buffer;
        string stringToRead;
        string stringRead;
        bool synced;
        
        Random rand;

        public Reader(CharacterBuffer buffer, Random rand)
        {
            this.buffer = buffer;
            this.rand = rand;
        }

        public void SetStringToRead(string stringToRead) { this.stringToRead = stringToRead; }
        public void SetSyncMode(bool sync) { synced = sync; }

        public string StringRead
        {
            get
            {
                return stringRead;
            }
        }

        public void Run()
        {
            bool success;
            char temp = '\0';

            for (int i = 0; i < stringToRead.Length; i++)
            {
                if (synced)
                {
                    success = false;

                    while (!success)
                    {
                        temp = buffer.SyncedRead(out success);
                        Thread.Sleep(rand.Next(1, 1000));
                    }
                    stringRead += temp;
                }
                else
                {
                    stringRead += buffer.NotSyncedRead();
                    Thread.Sleep(rand.Next(1, 1000));
                }
            }
        }
    }
}
