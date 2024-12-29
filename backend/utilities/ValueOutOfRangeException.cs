using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        readonly float r_MaxValue;
        readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue)
            :base(String.Format("Value is out of range - it must be between {0} and {1}", i_MinValue, i_MaxValue))
        { 
        }
    }
}