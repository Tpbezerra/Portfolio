using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_Form
{
    public static class Settings
    {
        public static int storageCapacity = 20;

        public static int minProducerSpeed = 500;
        public static int maxProducerSpeed = 3000;
        
        #region Consumer Settings
        public static int minConsumerItemLimit = 10;
        public static int maxConsumerItemLimit = 11;

        public static int minConsumerWeightLimit = 5;
        public static int maxConsumerWeightLimit = 6;

        public static int minConsumerVolumeLimit = 5;
        public static int maxConsumerVolumeLimit = 6;

        public static int minConsumerSpeed = 500;
        public static int maxConsumerSpeed = 2000;

        public static int minConsumerSwitchSpeed = 2000;
        public static int maxConsumerSwitchSpeed = 4000;
        #endregion
    }
}
