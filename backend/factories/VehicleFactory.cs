using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factories
{
    public class GarageFactory
    {
        public enum eUserVehicleChoice
        {
            ElectricCar = 1,
            FuelBasedCar,
            ElectricMotorcycle,
            FuelBasedMotorcycle,
            Truck
        }

        public static void CreateVehicle(eUserVehicleChoice i_UserVehicleChoice, String i_ModelName, String i_LicenseNumber, String i_OwnerName, String i_PhoneNumber)
        {
            Vehicle result = null;

            switch (i_UserVehicleChoice)
            {
                case eUserVehicleChoice.ElectricCar:
                    result = new Car(eEngineType.Electric, i_ModelName, i_LicenseNumber, i_OwnerName, i_PhoneNumber);
                    break;
                case eUserVehicleChoice.FuelBasedCar:
                    result = new Car(eEngineType.Fuel, i_ModelName, i_LicenseNumber, i_OwnerName, i_PhoneNumber);
                    break;
                case eUserVehicleChoice.ElectricMotorcycle:
                    result = new Motorcycle(eEngineType.Electric, i_ModelName, i_LicenseNumber, i_OwnerName, i_PhoneNumber);
                    break;
                case eUserVehicleChoice.FuelBasedMotorcycle:
                    result = new Motorcycle(eEngineType.Fuel, i_ModelName, i_LicenseNumber, i_OwnerName, i_PhoneNumber);
                    break;
                case eUserVehicleChoice.Truck:
                    result = new Truck(i_ModelName, i_LicenseNumber, i_OwnerName, i_PhoneNumber);
                    break;

            }
           
            GarageManager.m_Vehicles.Add(result);
        }

        public static void InitializeVehicleWheels(String i_LicenseNumber, float[] i_CurrentAirPressures, String[] i_ManufacturerName)
        {
            Vehicle vehicleToInit = GarageManager.GetVehicle(i_LicenseNumber);

            if (vehicleToInit == null)
            {
                throw new ArgumentException("Vehicle not found."); 
            }

            for (int i = 0; i < i_CurrentAirPressures.Length; i++)
            {
                if(i_CurrentAirPressures[i] > vehicleToInit.m_Wheels[i].r_MaxFillValue || i_CurrentAirPressures[i] < 0)
                {
                    throw new ValueOutOfRangeException(0f, vehicleToInit.m_Wheels[i].r_MaxFillValue);
                }
                vehicleToInit.m_Wheels[i].m_CurrentFillValue = i_CurrentAirPressures[i];
                vehicleToInit.m_Wheels[i].m_ManufacturerName = string.Copy(i_ManufacturerName[i]);
            }
        }

        public static void InitializeCar(string i_LicenseNumber, eNumOfDoors i_NumOfDoors, eColor i_Color)
        {
            Car vehicleToInit = GarageManager.GetVehicle(i_LicenseNumber) as Car;

            if (vehicleToInit == null)
            {
                throw new ArgumentException("Car not found.");
            }

            vehicleToInit.m_Color = i_Color;
            vehicleToInit.m_NumOfDoors = i_NumOfDoors;
        }

        public static void InitializeMotorcycle(string i_LicenseNumber, eLicenseType i_LicenseType, int i_EngineVolume)
        {
            Motorcycle vehicleToInit = GarageManager.GetVehicle(i_LicenseNumber) as Motorcycle;

            if (vehicleToInit == null)
            {
                throw new ArgumentException("Motorcycle not found.");
            }

            vehicleToInit.m_LicenseType = i_LicenseType;
            vehicleToInit.m_EngineVolume = i_EngineVolume;
        }

        public static void InitializeTruck(string i_LicenseNumber, bool i_ContainsDangerousMaterials, float i_CargoTankVolume)
        {
            Truck vehicleToInit = GarageManager.GetVehicle(i_LicenseNumber) as Truck;

            if (vehicleToInit == null)
            {
                throw new ArgumentException("Truck not found.");
            }

            vehicleToInit.m_ContainsDangerousMaterials = i_ContainsDangerousMaterials;
            vehicleToInit.m_CargoTankVolume = i_CargoTankVolume;
        }
        public static void InitializeVehicleEnergy(string i_LicenseNumber, float i_EnergyLevel)
        {
            Vehicle vehicleToInit = GarageManager.GetVehicle(i_LicenseNumber);

            if (vehicleToInit == null)
            {
                throw new ArgumentException("Vehicle not found");
            }

            if(i_EnergyLevel >= 0 && i_EnergyLevel <= vehicleToInit.m_Engine.r_MaxFillValue)
            {
                vehicleToInit.m_Engine.m_CurrentFillValue = i_EnergyLevel;
                vehicleToInit.m_RemainingEnergyPercentage = (i_EnergyLevel / vehicleToInit.m_Engine.r_MaxFillValue) * 100;
            }
            else
            {
                throw new ValueOutOfRangeException(0f, vehicleToInit.m_Engine.r_MaxFillValue);
            }
        }
    }
}