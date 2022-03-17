using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment2_Form
{
    public class Writer
    {
        CharacterBuffer buffer;
        string stringToWrite;
        string stringWritten;
        bool synced;

        Random rand;

        public Writer(CharacterBuffer buffer, Random rand)
        {
            this.buffer = buffer;
            this.rand = rand;
            stringWritten = "";
        }

        public void SetStringToWrite(string stringToWrite) { this.stringToWrite = stringToWrite; }
        public void SetSyncMode(bool sync) { synced = sync; }

        public string StringWritten
        {
            get
            {
                return stringWritten;
            }
        }

        public void Run()
        {
            bool success;

            for (int i = 0; i < stringToWrite.Length; i++)
            {
                if (synced)
                {
                    success = false;

                    while (!success)
                    {
                        success = buffer.SyncedWrite(stringToWrite[i]);
                        Thread.Sleep(rand.Next(1, 1000));
                    }
                    stringWritten += buffer.BufferedChar;
                }
                else
                {
                    buffer.NotSyncWrite(stringToWrite[i]);
                    Thread.Sleep(rand.Next(1, 1000));
                    stringWritten += buffer.BufferedChar;
                }
            }
        }
    }
}
