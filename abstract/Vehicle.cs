using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eVehicleStatus
    {
        InRepair = 1,
        Repaired,
        PaidFor
    }
    
    public enum eEngineType
    {
        Electric = 1,
        Fuel
    }
    public abstract class Vehicle
    {
        public readonly string r_ModelName;
        public readonly string r_LicenseNumber;
        public readonly string r_OwnerName;
        public readonly string r_PhoneNumber;
        public float m_RemainingEnergyPercentage; 
        public Wheel[] m_Wheels;
        public eVehicleStatus m_VehicleStatus;
        public Fillable m_Engine;

        public Vehicle(String i_ModelName, String i_LicenseNumber, Wheel[] i_Wheels, String i_OwnerName, String i_PhoneNumber)
        {
            this.r_ModelName = i_ModelName;
            this.r_LicenseNumber = i_LicenseNumber;
            this.m_Wheels = i_Wheels;
            this.r_OwnerName = i_OwnerName;
            this.r_PhoneNumber = i_PhoneNumber;
            this.m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public void FillEngine(float i_AmountToFill)
        {
            m_Engine.Fill(i_AmountToFill);
            m_RemainingEnergyPercentage = (m_Engine.m_CurrentFillValue / m_Engine.r_MaxFillValue) * 100;
        }

        public override string ToString()
        {
            string result = String.Format("License number: {0}\n" +
                              "Vehicle type: {1}\n" +
                              "Model: {2}\n" +
                              "Owner name: {3}\n" +
                              "Vehicle status: {4}\n" +
                              "Wheel status: \n{5}\n" +
                              "Engine information: \n{6}",
                               r_LicenseNumber,
                               this.GetType().Name,
                               r_ModelName,
                               r_OwnerName,
                               m_VehicleStatus,
                               string.Join("\n", m_Wheels.Select(wheel => wheel.ToString())),
                               m_Engine.ToString());

            return result;
        }
    }
}