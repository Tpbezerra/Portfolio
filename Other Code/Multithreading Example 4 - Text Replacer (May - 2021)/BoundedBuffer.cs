using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment4_Form
{
    public class BoundedBuffer
    {
        StringElement[] strings;

        int writePos;
        int readPos;
        int modifyPos;

        string toReplace;
        string replacementString;

        RichTextBox destinationBox;

        object lockObj = new object();

        delegate void UpdateTextBoxDel(string value);

        public BoundedBuffer(RichTextBox destinationBox, int bufferSize, string toReplace, string replacementString)
        {
            strings = new StringElement[bufferSize];

            writePos = readPos = modifyPos = 0;

            this.destinationBox = destinationBox;
            this.toReplace = toReplace;
            this.replacementString = replacementString;
        }

        /// <summary>
        /// Takes in a string to write to a position in the buffer.
        /// </summary>
        /// <param name="toWrite"></param>
        public void Write(string toWrite)
        {
            // Enter the critical section
            lock (lockObj)
            {
                // If the status for the current position is not the correct one, alow another thread to proceed.
                while (strings[writePos].status != StringElement.ElementStatus.Empty)
                {
                    Monitor.Wait(lockObj);
                }

                // Update the string and status at the writePos an then move the writePos one step forward.
                strings[writePos].word = toWrite;
                strings[writePos].status = StringElement.ElementStatus.New;
                writePos = (writePos + 1) % strings.Length;

                // Signal att other threads that a value has changed.
                Monitor.PulseAll(lockObj);
            }
        }

        /// <summary>
        /// Returns the value of the string at the readPos as long as the status at that position is Checked.
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            string toReturn = "";

            // Enter the critical section
            lock (lockObj)
            {
                // If the status for the current position is not the correct one, alow another thread to proceed.
                while (strings[readPos].status != StringElement.ElementStatus.Checked)
                {
                    Monitor.Wait(lockObj);
                }

                // Take the string at readPos and update the status, then move readPos one step forward.
                toReturn = strings[readPos].word;
                strings[readPos].status = StringElement.ElementStatus.Empty;
                readPos = (readPos + 1) % strings.Length;

                destinationBox.Invoke(new UpdateTextBoxDel(UpdateTextBox), new object[] { toReturn + " " });

                // Signal att other threads that a value has changed.
                Monitor.PulseAll(lockObj);
            }

            return toReturn;
        }

        /// <summary>
        /// Changes the string at the current modifyPos to math the replacement string as long as it matches with the find string.
        /// </summary>
        public void Modify()
        {
            // Enter the critical section
            lock (lockObj)
            {
                // If the status for the current position is not the correct one, alow another thread to proceed.
                while (strings[modifyPos].status != StringElement.ElementStatus.New)
                {
                    Monitor.Wait(lockObj);
                }

                // If the string at the modifyPos matches the findString, change it, no matter what update the status and move the modify pos forward.
                if (strings[modifyPos].word == toReplace)
                    strings[modifyPos].word = replacementString;

                strings[modifyPos].status = StringElement.ElementStatus.Checked;
                modifyPos = (modifyPos + 1) % strings.Length;

                // Signal att other threads that a value has changed.
                Monitor.PulseAll(lockObj);
            }
        }

        void UpdateTextBox(string value)
        {
            destinationBox.Text += value;
        }

        /// <summary>
        /// A struct to keep track of the string and the status of that string in each position.
        /// </summary>
        struct StringElement
        {
            public string word;

            public enum ElementStatus
            {
                Empty,
                New,
                Checked
            }
            public ElementStatus status;
        }
    }
}
