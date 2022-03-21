using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_Form
{
    public class Writer
    {
        BoundedBuffer buffer;

        List<string> textToWrite;

        public Writer(BoundedBuffer buffer, List<string> textToWrite)
        {
            this.buffer = buffer;
            this.textToWrite = textToWrite;
        }

        public void Run()
        {
            // Write once for every word in the list.
            for (int i = 0; i < textToWrite.Count; i++)
            {
                buffer.Write(textToWrite[i]);
            }
        }
    }
}
