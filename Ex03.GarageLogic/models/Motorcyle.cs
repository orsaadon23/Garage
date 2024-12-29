using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eLicenseType
    {
        A = 1,
        A1,
        AA,
        B1
    }

    public class Motorcycle : Vehicle
    {
        public eLicenseType m_LicenseType; 
        public int m_EngineVolume;
        private const int k_NumberOfWheels = 2;
        private const int k_MaxAirPressure = 33;
        private const float k_FuelTankSize = 5.5f;
        private const float k_BatterySize = 2.5f;

        public Motorcycle(eEngineType i_EngineType, String i_ModelName, String i_LicenseNumber, String i_OwnerName, String i_PhoneNumber)
        : base(i_ModelName, i_LicenseNumber, Wheel.CreateWheels(2, 33), i_OwnerName, i_PhoneNumber)
        {
            if (i_EngineType == eEngineType.Fuel)
            {
                m_Engine = new FuelEngine(eFuelType.Octane98, k_FuelTankSize);
            }
            else if (i_EngineType == eEngineType.Electric)
            {
                m_Engine = new ElectricEngine(k_BatterySize);
            }
        }

        public override string ToString()
        {
            string result = String.Format("{0}\n" +
                                          "License type: {1}\n" +
                                          "Engine volume: {2}",
                                          base.ToString(),
                                          m_LicenseType,
                                          m_EngineVolume);

            return result;
        }
    }
}