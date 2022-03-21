using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_Form
{
    public class Modifier
    {
        BoundedBuffer buffer;

        int amountWords;

        public Modifier(BoundedBuffer buffer, int amountWords)
        {
            this.buffer = buffer;
            this.amountWords = amountWords;
        }

        public void Run()
        {
            // Run once for every word in the list.
            for (int i = 0; i < amountWords; i++)
            {
                buffer.Modify();
            }
        }
    }
}
