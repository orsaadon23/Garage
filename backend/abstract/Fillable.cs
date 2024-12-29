using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Fillable
    {
        public float m_CurrentFillValue; 
        public readonly float r_MaxFillValue;

        public Fillable(float i_MaxFillValue)
        {
            r_MaxFillValue = i_MaxFillValue;
        }

        public void Fill(float i_ToAdd)
        {
            m_CurrentFillValue += i_ToAdd;

            if (m_CurrentFillValue > r_MaxFillValue)
            {
                throw new ValueOutOfRangeException(0, r_MaxFillValue);
            }
        }

        public void FillToMax()
        {
            m_CurrentFillValue = r_MaxFillValue;
        }
    }
}