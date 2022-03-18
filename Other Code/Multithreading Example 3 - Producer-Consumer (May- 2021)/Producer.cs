using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3_Form
{
    public class Producer
    {
        FoodItem[] availableFoods;

        Label status;

        Buffer foodBuffer;
        Random rand;

        delegate void UpdateLabelDel(Label toUpdate, string value);

        public Producer(Buffer foodBuffer, Label status, FoodItem[] availableFoods, Random rand)
        {
            this.foodBuffer = foodBuffer;
            this.status = status;
            this.availableFoods = availableFoods;
            this.rand = rand;
        }

        /// <summary>
        /// The method in charge of producing items into the buffer and altering the current status
        /// </summary>
        public void Run()
        {
            FoodItem currentItem = null;
            bool success = false;

            while (!success)
            {
                status.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { status, "Waiting for available space" });

                currentItem = availableFoods[rand.Next(0, availableFoods.Length)];
                success = foodBuffer.Produce(currentItem);
            }

            status.Invoke(new UpdateLabelDel(UpdateLabel), new object[] { status, $"Made {currentItem.Name}" });

            // Wait for a moment before producing another item
            Thread.Sleep(rand.Next(Settings.minProducerSpeed, Settings.maxProducerSpeed));

            Run();
        }

        void UpdateLabel(Label toUpdate, string value)
        {
            toUpdate.Text = value;
        }
    }
}
