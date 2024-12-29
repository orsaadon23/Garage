using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        public bool m_ContainsDangerousMaterials; 
        public float m_CargoTankVolume;
        private const int k_NumberOfWheels = 12;
        private const int k_MaxAirPressure = 28;
        private const int k_FuelTankSize = 120;

        public Truck(String i_ModelName, String i_LicenseNumber, String i_OwnerName, String i_PhoneNumber)
        : base(i_ModelName, i_LicenseNumber, Wheel.CreateWheels(k_NumberOfWheels, k_MaxAirPressure), i_OwnerName, i_PhoneNumber)
        {
            m_Engine = new FuelEngine(eFuelType.Soler, k_FuelTankSize);
        }

        public override string ToString()
        {
            string result = String.Format("{0}\n" +
                                          "{1} contain dangerous materials\n" +
                                          "Cargo tank volume: {2}",
                                          base.ToString(),
                                          m_ContainsDangerousMaterials ? "Does" : "Does not",
                                          m_CargoTankVolume);

            return result;
        }
    }
}