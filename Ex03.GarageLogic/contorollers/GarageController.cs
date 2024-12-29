using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        public static List<Vehicle> m_Vehicles = new List<Vehicle>();
        
        public static List<string> GetLicenseList(eVehicleStatus? i_Status = null)
        {
            List<string> result = new List<string>();

            foreach (Vehicle vehicle in m_Vehicles)
            {
                if (!i_Status.HasValue || vehicle.m_VehicleStatus == i_Status.Value)
                {
                    result.Add(vehicle.r_LicenseNumber);
                }
            }

            return result;
        }

        public static void SetStatus(string i_LicenseNumber, eVehicleStatus i_NewVehicleStatus)
        {
            Vehicle vehicle = null;
            vehicle = GetVehicle(i_LicenseNumber);

            if (vehicle != null)
            {
                vehicle.m_VehicleStatus = i_NewVehicleStatus;
            }
            else
            {
                throw new ArgumentException("Vehicle not found.");
            }
        }

        public static void MaxFillTires(string i_LicenseNumber)
        {
            Vehicle vehicle = GetVehicle(i_LicenseNumber);

            if (vehicle == null)
            {
                throw new ArgumentException("Vehicle not found.");
            }

            foreach (Wheel wheel in vehicle.m_Wheels)
            {
                wheel.FillToMax();
            }         
        }

        public static void Refuel(string i_LicenseNumber, eFuelType i_FuelType, float i_AmountToFill) 
        {
            Vehicle vehicle = GetVehicle(i_LicenseNumber);

            if(vehicle.m_Engine is FuelEngine && i_FuelType == (vehicle.m_Engine as FuelEngine).m_FuelType)
            {
                vehicle.FillEngine(i_AmountToFill);
            }
            else if(vehicle.m_Engine is FuelEngine)
            {       
                throw new ArgumentException("Wrong fuel type");
            }
            else
            {
                throw new ArgumentException("Engine is not fuel-based.");
            }
        }

        public static void Charge(string i_LicenseNumber, float i_NumOfMinutesToCharge)
        {
            Vehicle vehicle = GetVehicle(i_LicenseNumber);

            if (vehicle.m_Engine is ElectricEngine)
            {
                vehicle.FillEngine(i_NumOfMinutesToCharge / 60);
            }
            else
            {
                throw new ArgumentException("Engine is not electric.");
            }
        }

        public static string ListAllVehicleDetails(string i_LicenseNumber)
        {
            Vehicle vehicle = GetVehicle(i_LicenseNumber);

            if(vehicle == null)
            {
                throw new ArgumentException("Vehicle does not exist");
            }

            return vehicle.ToString();
        }

        public static Vehicle GetVehicle(string i_LicenseNumber)
        {
            Vehicle result = null;

            foreach (Vehicle vehicle in m_Vehicles)
            {
                if (vehicle.r_LicenseNumber == i_LicenseNumber)
                {
                    result = vehicle;
                    break;
                }
            }

            return result;
        }
    } 
}