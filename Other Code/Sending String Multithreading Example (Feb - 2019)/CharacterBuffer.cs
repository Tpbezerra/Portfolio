using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2_Form
{
    public class CharacterBuffer
    {
        char bufferedChar;
        bool hasStoredChar;
        object lockObj = new object();

        ListBox writerListBox;
        ListBox readerListBox;

        public char BufferedChar { get { return bufferedChar; } }

        delegate void displayStringDelegate(ListBox listBox, string toDisplay);

        public CharacterBuffer(ListBox writerListBox, ListBox readerListBox)
        {
            bufferedChar = ' ';
            hasStoredChar = false;
            this.writerListBox = writerListBox;
            this.readerListBox = readerListBox;
        }

        public char NotSyncedRead()
        {
            StringBuilder sb = new StringBuilder("Reading ");
            if (char.IsWhiteSpace(bufferedChar))
                sb.Append("blank");

            readerListBox.Invoke(new displayStringDelegate(DisplayString), new object[] { readerListBox, sb.ToString() + bufferedChar });
            return bufferedChar;
        }

        public void NotSyncWrite(char value)
        {
            StringBuilder sb = new StringBuilder("Writing ");
            if (char.IsWhiteSpace(value))
                sb.Append("blank");

            bufferedChar = value;
            writerListBox.Invoke(new displayStringDelegate(DisplayString), new object[] { writerListBox, sb.ToString() + bufferedChar });
        }

        public char SyncedRead(out bool success)
        {
            success = false;
            char toReturn = '\0';

            lock (lockObj)
            {
                if (hasStoredChar)
                {
                    hasStoredChar = false;

                    StringBuilder sb = new StringBuilder("Reading ");
                    if (char.IsWhiteSpace(bufferedChar))
                        sb.Append("blank");

                    readerListBox.Invoke(new displayStringDelegate(DisplayString), new object[] { readerListBox, sb.ToString() + bufferedChar });
                    toReturn = bufferedChar;
                    success = true;
                }
                else
                    readerListBox.Invoke(new displayStringDelegate(DisplayString), new object[] { readerListBox, "Waiting for writer" });
            }

            return toReturn;
        }

        public bool SyncedWrite(char value)
        {
            bool success = false;

            lock (lockObj)
            {
                if (hasStoredChar)
                    writerListBox.Invoke(new displayStringDelegate(DisplayString), new object[] { writerListBox, "Waiting for reader" });
                else
                {
                    hasStoredChar = true;

                    StringBuilder sb = new StringBuilder("Writing ");
                    if (char.IsWhiteSpace(value))
                        sb.Append("blank");

                    writerListBox.Invoke(new displayStringDelegate(DisplayString), new object[] { writerListBox, sb.ToString() + value });
                    bufferedChar = value;
                    success = true;
                }
            }

            return success;
        }

        void DisplayString(ListBox listBox, string toDisplay)
        {
            listBox.Items.Add(toDisplay);
        }
    }
}
