using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Wheel : Fillable
    {
        public String m_ManufacturerName;

        public Wheel(float i_MaxAirPressureRecommended)
            : base(i_MaxAirPressureRecommended)
        {
        }

        public static Wheel[] CreateWheels(int i_NumOfWheels, float i_MaxAirPressure)
        {
            Wheel[] wheels = new Wheel[i_NumOfWheels];

            for (int i = 0; i < i_NumOfWheels; i++)
            {
                wheels[i] = new Wheel(i_MaxAirPressure);
            }

            return wheels;
        }

        public override string ToString()
        {
            return string.Format("Manufacturer name: {0}, air pressure: {1}/{2}", m_ManufacturerName, m_CurrentFillValue, r_MaxFillValue);
        }
    }
}