using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineticForceCalc
{
    public static class Calculations
    {
        public static double KineticEnergy(double mass, double velocity)
        {
            return mass * (velocity * velocity) * 0.5;
        }

        public static double PotentialEnergy(double mass, double height)
        {
            return mass * Constants.gravity * height;
        }
    }
}
