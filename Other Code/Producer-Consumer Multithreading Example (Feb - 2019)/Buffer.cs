using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment3_Form
{
    public class Buffer
    {
        List<FoodItem> foodBuffer = new List<FoodItem>();

        Label currentItemCount;

        ProgressBar progressBar;

        int ItemCount
        {
            get
            {
                return foodBuffer.Count;
            }
        }
        int maxItems;

        Semaphore fullSem;
        Semaphore emptySem;
        Mutex mutex;

        delegate void UpdateProgressDel(int value);
        delegate void UpdateItemCountDel(string value);

        public Buffer(int maxStorageQuantity, Label currentItemCount, ProgressBar progressBar)
        {
            maxItems = maxStorageQuantity;

            fullSem = new Semaphore(0, maxItems);
            emptySem = new Semaphore(maxItems, maxItems);
            mutex = new Mutex(false);

            this.currentItemCount = currentItemCount;
            this.progressBar = progressBar;
        }

        public bool Produce(FoodItem toProduce)
        {
            bool toReturn;

            // Request access to the empty semaphore
            emptySem.WaitOne();
            // Lock access for all but one thread
            mutex.WaitOne();

            foodBuffer.Add(toProduce);

            UpdateProgress();

            toReturn = true;

            // Release the lock
            mutex.ReleaseMutex();
            // Add 1 item to the full semaphore
            fullSem.Release();

            return toReturn;
        }

        public FoodItem Consume(out bool success)
        {
            FoodItem toReturn;
            success = false;

            // Request access to the full semaphore
            fullSem.WaitOne();
            // Lock access for all but one thread
            mutex.WaitOne();

            // Safety net, incase the ItemCount and semaphore don't add up
            while (ItemCount == 0) ;

            toReturn = foodBuffer[0];
            foodBuffer.RemoveAt(0);

            UpdateProgress();

            success = true;

            // Release the lock
            mutex.ReleaseMutex();
            // Add 1 item to the empty semaphore
            emptySem.Release();

            return toReturn;
        }

        /// <summary>
        /// Updates the progressbar and the label of how many items there are
        /// </summary>
        void UpdateProgress()
        {
            float value = (float)ItemCount / maxItems;
            progressBar.Invoke(new UpdateProgressDel(UpdateProgress), new object[] { (int)(value * 100) });
            currentItemCount.Invoke(new UpdateItemCountDel(UpdateItemCount), new object[] { $"Current Items: {ItemCount}/{maxItems}" });
        }

        void UpdateProgress(int value)
        {
            progressBar.Value = value;
        }

        void UpdateItemCount(string value)
        {
            currentItemCount.Text = value;
        }
    }
}
