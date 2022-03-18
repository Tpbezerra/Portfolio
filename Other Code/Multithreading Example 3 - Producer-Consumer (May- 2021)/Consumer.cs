using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3_Form
{
    public class Consumer
    {
        public bool ContinueRunning;

        Label status;
        Label items;
        Label weight;
        Label volume;

        ListBox list;

        int maxItems;
        int totalItems;

        float maxWeight;
        float maxVolume;
        float totalWeight;
        float totalVolume;

        Buffer foodBuffer;
        Random rand;

        delegate void UpdateLabelDel(Label toUpdate, string value);
        delegate void UpdateListBoxDel(ListBox toUpdate, string value);
        delegate void ClearListBoxDel();

        public Consumer(Buffer foodBuffer, Label status, Label items, Label weight, Label volume, ListBox list, Random rand)
        {
            this.foodBuffer = foodBuffer;
            this.status = status;
            this.items = items;
            this.weight = weight;
            this.volume = volume;
            this.list = list;
            this.rand = rand;

            maxItems = rand.Next(Settings.minConsumerItemLimit, Settings.maxConsumerItemLimit);
            maxWeight = rand.Next(Settings.minConsumerWeightLimit, Settings.maxConsumerWeightLimit);
            maxVolume = rand.Next(Settings.minConsumerVolumeLimit, Settings.maxConsumerVolumeLimit);
        }

        /// <summary>
        /// The method in charge of consuming items from the buffer and updating labels
        /// </summary>
        public void Run()
        {
            FoodItem consumedItem = null;
            bool success = false;
            
            totalItems = 0;
            totalWeight = 0;
            totalVolume = 0;
            list.Invoke(new ClearListBoxDel(ClearListBox));

            while (totalItems < maxItems && totalWeight < maxWeight && totalVolume < maxVolume)
            {
                success = false;

                while (!success)
                {
                    status.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { status, "Waiting for item..." });

                    consumedItem = foodBuffer.Consume(out success);
                }

                totalItems++;

                // Update labels and listbox
                status.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { status, $"Consumed {consumedItem.Name}" });
                items.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { items, totalItems.ToString() });
                totalWeight += consumedItem.Weight;
                weight.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { weight, totalWeight.ToString() });
                totalVolume += consumedItem.Volume;
                volume.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { volume, totalVolume.ToString() });
                list.Invoke(new UpdateListBoxDel(UpdateListBox), new object[] { list, consumedItem.Name });

                // Wait for a moment before consuming the next item
                Thread.Sleep(rand.Next(Settings.minConsumerSpeed, Settings.maxConsumerSpeed));
            }

            // Change the status depending on the reason for finishing
            if (totalItems >= maxItems)
                status.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { status, "Item limit reached" });
            else if (totalWeight >= maxWeight)
                status.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { status,  "Weight limit reached" });
            else if (totalVolume >= maxVolume)
                status.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { status, "Volume limit reached" });

            // Wait for a moment before a new consumer can run
            Thread.Sleep(rand.Next(Settings.minConsumerSwitchSpeed, Settings.maxConsumerSwitchSpeed));

            if (ContinueRunning)
                Run();
        }

        void UpdateLabel(Label toUpdate, string value)
        {
            toUpdate.Text = value;
        }

        void UpdateListBox(ListBox toUpdate, string value)
        {
            toUpdate.Items.Add(value);
        }

        void ClearListBox()
        {
            list.Items.Clear();
        }
    }
}
