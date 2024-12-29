using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Fillable
    {
        public ElectricEngine(float i_MaxHours) 
            : base(i_MaxHours)
        { 
        }

        public override string ToString()
        {
            return string.Format("Electric engine \nRemaining battery life: {0}/{1} hours", m_CurrentFillValue, r_MaxFillValue);
        }
    }
}