using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Particle_Force_Calc
{
    public partial class Form1 : Form
    {
        double k;

        public Form1()
        {
            InitializeComponent();

            k = 8.988 * Math.Pow(10, 9);
        }

        private void CalcButton_Click(object sender, EventArgs e)
        {
            double Q1 = 0;
            double Q2 = 0;
            double r = 0;

            if (!double.TryParse(Particle1Input.Text, out Q1) || !double.TryParse(Particle1Pow.Text, out Q1))
            {
                Particle1Input.Text = "Invalid Input";
                Particle1Pow.Text = "X";
                return;
            }

            if (!double.TryParse(Particle2Input.Text, out Q2) || !double.TryParse(Particle2Pow.Text, out Q2))
            {
                Particle2Input.Text = "Invalid Input";
                Particle2Pow.Text = "X";
                return;
            }

            if (!double.TryParse(DistanceInput.Text, out r) || !double.TryParse(DistancePow.Text, out r))
            {
                DistanceInput.Text = "Invalid Input";
                DistancePow.Text = "X";
                return;
            }

            Q1 = double.Parse(Particle1Input.Text);
            Q1 *= Math.Pow(10, double.Parse(Particle1Pow.Text));

            Q2 = double.Parse(Particle2Input.Text);
            Q2 *= Math.Pow(10, double.Parse(Particle2Pow.Text));

            r = double.Parse(DistanceInput.Text);
            r *= Math.Pow(10, double.Parse(DistancePow.Text));

            double answer = k * ((Q1 * Q2) / Math.Pow(r, 2));

            ForceText.Text = answer.ToString("D3");
        }
    }
}
