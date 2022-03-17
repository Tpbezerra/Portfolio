using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Assignment3_Form
{
    public partial class Form1 : Form
    {
        FoodItem[] availableFoods;

        Producer scan;
        Producer arla;
        Producer axFood;

        Consumer ica;
        Consumer coop;
        Consumer city;

        Thread scanThread;
        Thread arlaThread;
        Thread axFoodThread;
        Thread icaThread;
        Thread coopThread;
        Thread cityThread;

        Buffer foodBuffer;
        Random rand;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeFood();

            foodBuffer = new Buffer(Settings.storageCapacity, lblMax, progressItems);
            rand = new Random();

            scan = new Producer(foodBuffer, lblStatusScan, availableFoods, rand);
            arla = new Producer(foodBuffer, lblStatusArla, availableFoods, rand);
            axFood = new Producer(foodBuffer, lblStatusAxfood, availableFoods, rand);
        }

        /// <summary>
        /// Create an array with all available food types for the consumers and producers to use
        /// </summary>
        void InitializeFood()
        {
            availableFoods = new FoodItem[20];
            availableFoods[0] = new FoodItem("Milk", 1.1f, 0.5f);
            availableFoods[1] = new FoodItem("Cream", 0.6f, 0.1f);
            availableFoods[2] = new FoodItem("Youghurt", 1.1f, 0.5f);
            availableFoods[3] = new FoodItem("Butter", 2.24f, 0.66f);
            availableFoods[4] = new FoodItem("Flower", 3.4f, 1.2f);
            availableFoods[5] = new FoodItem("Sugar", 3.7f, 1.8f);
            availableFoods[6] = new FoodItem("Salt", 1.55f, 0.27f);
            availableFoods[7] = new FoodItem("Almonds", 0.6f, 0.19f);
            availableFoods[8] = new FoodItem("Bread", 1.98f, 0.75f);
            availableFoods[9] = new FoodItem("Donuts", 1.4f, 0.5f);
            availableFoods[10] = new FoodItem("Jam", 1.3f, 1.5f);
            availableFoods[11] = new FoodItem("Ham", 4.1f, 2.5f);
            availableFoods[12] = new FoodItem("Chicken", 6.8f, 3.9f);
            availableFoods[13] = new FoodItem("Salat", 0.87f, 0.55f);
            availableFoods[14] = new FoodItem("Orange", 2.46f, 0.29f);
            availableFoods[15] = new FoodItem("Apple", 2.44f, 0.4f);
            availableFoods[16] = new FoodItem("Pear", 1.3f, 0.77f);
            availableFoods[17] = new FoodItem("Soda", 2.98f, 2.0f);
            availableFoods[18] = new FoodItem("Beer", 3.74f, 1.5f);
            availableFoods[19] = new FoodItem("Sausage", 2.0f, 1.38f);
        }

        /// <summary>
        /// Start scan producer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartScan_Click(object sender, EventArgs e)
        {
            btnStartScan.Enabled = false;
            btnStopScan.Enabled = true;

            scanThread = new Thread(new ThreadStart(scan.Run));
            scanThread.Start();
        }

        /// <summary>
        /// Start arla producer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartArla_Click(object sender, EventArgs e)
        {
            btnStartArla.Enabled = false;
            btnStopArla.Enabled = true;

            arlaThread = new Thread(new ThreadStart(arla.Run));
            arlaThread.Start();
        }

        /// <summary>
        /// Start axfood producer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartAxfood_Click(object sender, EventArgs e)
        {
            btnStartAxfood.Enabled = false;
            btnStopAxfood.Enabled = true;

            axFoodThread = new Thread(new ThreadStart(axFood.Run));
            axFoodThread.Start();
        }

        /// <summary>
        /// Stop scan producer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopScan_Click(object sender, EventArgs e)
        {
            btnStartScan.Enabled = true;
            btnStopScan.Enabled = false;

            if (!scanThread.IsAlive)
                return;

            scanThread.Abort();
        }

        /// <summary>
        /// Stop arla producer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopArla_Click(object sender, EventArgs e)
        {
            btnStartArla.Enabled = true;
            btnStopArla.Enabled = false;

            if (!arlaThread.IsAlive)
                return;

            arlaThread.Abort();
        }

        /// <summary>
        /// Stop axfood producer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopAxfood_Click(object sender, EventArgs e)
        {
            btnStartAxfood.Enabled = true;
            btnStopAxfood.Enabled = false;

            if (!axFoodThread.IsAlive)
                return;

            axFoodThread.Abort();
        }

        /// <summary>
        /// Start Ica consumer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartIca_Click(object sender, EventArgs e)
        {
            btnStartIca.Enabled = false;
            btnStopIca.Enabled = true;

            ica = new Consumer(foodBuffer, lblIcaStatus, lblIcaItem, lblIcaWeight, lblIcaVolume, lstIca, rand);
            ica.ContinueRunning = chkIcaCont.Checked;

            icaThread = new Thread(new ThreadStart(ica.Run));
            icaThread.Start();
        }

        /// <summary>
        /// Stop Ica thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopIca_Click(object sender, EventArgs e)
        {
            btnStartIca.Enabled = true;
            btnStopIca.Enabled = false;

            if (!icaThread.IsAlive)
                return;

            icaThread.Abort();
        }

        /// <summary>
        /// Start Coop consumer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartCoop_Click(object sender, EventArgs e)
        {
            btnStartCoop.Enabled = false;
            btnStopCoop.Enabled = true;

            coop = new Consumer(foodBuffer, lblCoopStatus, lblCoopItem, lblCoopWeight, lblCoopVolume, lstCoop, rand);
            coop.ContinueRunning = chkCoopCont.Checked;

            coopThread = new Thread(new ThreadStart(coop.Run));
            coopThread.Start();
        }

        /// <summary>
        /// Stop coop thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopCoop_Click(object sender, EventArgs e)
        {
            btnStartCoop.Enabled = true;
            btnStopCoop.Enabled = false;

            if (!coopThread.IsAlive)
                return;

            coopThread.Abort();
        }

        /// <summary>
        /// Start City Gross consumer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartCity_Click(object sender, EventArgs e)
        {
            btnStartCity.Enabled = false;
            btnStopCity.Enabled = true;

            city = new Consumer(foodBuffer, lblCityStatus, lblCityItem, lblCityWeight, lblCityVolume, lstCity, rand);
            city.ContinueRunning = chkCityCont.Checked;

            cityThread = new Thread(new ThreadStart(city.Run));
            cityThread.Start();
        }

        /// <summary>
        /// Stop city gross thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopCity_Click(object sender, EventArgs e)
        {
            btnStartCity.Enabled = true;
            btnStopCity.Enabled = false;

            if (!cityThread.IsAlive)
                return;

            cityThread.Abort();
        }

        /// <summary>
        /// Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
    }
}
