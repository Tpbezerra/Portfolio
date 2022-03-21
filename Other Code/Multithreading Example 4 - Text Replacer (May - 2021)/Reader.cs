using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4_Form
{
    public class Reader
    {
        BoundedBuffer buffer;

        int count;

        List<string> stringList;

        public Reader(BoundedBuffer buffer, int count)
        {
            this.buffer = buffer;
            this.count = count;
            stringList = new List<string>();
        }

        public void Run()
        {
            // Read once for every word in the list.
            for (int i = 0; i < count; i++)
            {
                stringList.Add(buffer.Read());
            }
        }
    }
}
