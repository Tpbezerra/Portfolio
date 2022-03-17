using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3_Form
{
    public class FoodItem
    {
        string name;
        float weight;
        float volume;
        
        public string Name
        {
            get
            {
                return name;
            }
        }
        public float Weight
        {
            get
            {
                return weight;
            }
        }
        public float Volume
        {
            get
            {
                return volume;
            }
        }

        public FoodItem(string name, float weight, float volume)
        {
            this.name = name;
            this.weight = weight;
            this.volume = volume;
        }
    }
}
