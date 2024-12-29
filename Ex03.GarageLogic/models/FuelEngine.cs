using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eFuelType
    {
        Soler = 1,
        Octane95,
        Octane96,
        Octane98
    }

    public class FuelEngine : Fillable
    {
        public eFuelType m_FuelType;

        public FuelEngine(eFuelType i_FuelType, float i_MaxFuel)
            : base(i_MaxFuel)
        {
            m_FuelType = i_FuelType;
        }

        public override string ToString()
        {
            return string.Format("Fuel-based engine\nFuel type: {0}, amount of fuel: {1}/{2} liters", m_FuelType, m_CurrentFillValue, r_MaxFillValue);
        }
    }
}