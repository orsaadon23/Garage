using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public enum eColor
    {
        Yellow = 1,
        White,
        Red,
        Gray
    }
    
    public enum eNumOfDoors
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }
    public class Car : Vehicle
    {
        public eColor m_Color; 
        public eNumOfDoors m_NumOfDoors;
        private const int k_NumberOfWheels = 4;
        private const int k_MaxAirPressure = 31;
        private const int k_FuelTankSize = 45;
        private const float k_BatterySize = 3.5f;

        public Car(eEngineType i_EngineType, String i_ModelName, String i_LicenseNumber, String i_OwnerName, String i_PhoneNumber)
            : base(i_ModelName, i_LicenseNumber, Wheel.CreateWheels(k_NumberOfWheels, k_MaxAirPressure), i_OwnerName, i_PhoneNumber)
        {
            if (i_EngineType == eEngineType.Fuel)
            {
                m_Engine = new FuelEngine(eFuelType.Octane95, k_FuelTankSize);
            }
            else if (i_EngineType == eEngineType.Electric)
            {
                m_Engine = new ElectricEngine(k_BatterySize);
            }
        }

        public override string ToString()
        {
            string result = String.Format("{0}\n" +
                                          "Color: {1}\n" +
                                          "Number of doors: {2}\n",
                                          base.ToString(),
                                          m_Color,
                                          m_NumOfDoors);

            return result;
        }
    }
}