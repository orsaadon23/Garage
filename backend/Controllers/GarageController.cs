using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class GarageManager
    {
        private static List<Vehicle> m_Vehicles = new List<Vehicle>();

        // New method to get all vehicles
        public static List<Vehicle> GetAllVehicles()
        {
            if (m_Vehicles.Count == 0)
            {
                throw new InvalidOperationException("The garage is currently empty.");
            }
            return new List<Vehicle>(m_Vehicles); // Return a copy to prevent external modifications
        }

        // New method to get vehicles by status
        public static List<Vehicle> GetVehiclesByStatus(eVehicleStatus status)
        {
            var vehicles = m_Vehicles.Where(v => v.m_VehicleStatus == status).ToList();
            if (vehicles.Count == 0)
            {
                throw new InvalidOperationException($"No vehicles found with status: {status}");
            }
            return vehicles;
        }

        public static List<string> GetLicenseList(eVehicleStatus? i_Status = null)
        {
            try
            {
                var result = new List<string>();
                foreach (Vehicle vehicle in m_Vehicles)
                {
                    if (!i_Status.HasValue || vehicle.m_VehicleStatus == i_Status.Value)
                    {
                        result.Add(vehicle.r_LicenseNumber);
                    }
                }

                if (result.Count == 0)
                {
                    throw new InvalidOperationException(
                        i_Status.HasValue 
                            ? $"No vehicles found with status: {i_Status}" 
                            : "No vehicles found in the garage.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving license list: {ex.Message}");
            }
        }

        public static void SetStatus(string i_LicenseNumber, eVehicleStatus i_NewVehicleStatus)
        {
            if (string.IsNullOrWhiteSpace(i_LicenseNumber))
            {
                throw new ArgumentException("License number cannot be empty or whitespace.");
            }

            Vehicle vehicle = GetVehicle(i_LicenseNumber);
            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with license number {i_LicenseNumber} not found.");
            }

            if (!Enum.IsDefined(typeof(eVehicleStatus), i_NewVehicleStatus))
            {
                throw new ArgumentException($"Invalid vehicle status: {i_NewVehicleStatus}");
            }

            vehicle.m_VehicleStatus = i_NewVehicleStatus;
        }

        public static void MaxFillTires(string i_LicenseNumber)
        {
            if (string.IsNullOrWhiteSpace(i_LicenseNumber))
            {
                throw new ArgumentException("License number cannot be empty or whitespace.");
            }

            Vehicle vehicle = GetVehicle(i_LicenseNumber);
            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with license number {i_LicenseNumber} not found.");
            }

            if (vehicle.m_Wheels == null || vehicle.m_Wheels.Count == 0)
            {
                throw new InvalidOperationException("Vehicle has no wheels to fill.");
            }

            try
            {
                foreach (Wheel wheel in vehicle.m_Wheels)
                {
                    wheel.FillToMax();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error filling tires: {ex.Message}");
            }
        }

        public static void Refuel(string i_LicenseNumber, eFuelType i_FuelType, float i_AmountToFill)
        {
            if (string.IsNullOrWhiteSpace(i_LicenseNumber))
            {
                throw new ArgumentException("License number cannot be empty or whitespace.");
            }

            if (i_AmountToFill <= 0)
            {
                throw new ArgumentException("Amount to fill must be greater than zero.");
            }

            if (!Enum.IsDefined(typeof(eFuelType), i_FuelType))
            {
                throw new ArgumentException($"Invalid fuel type: {i_FuelType}");
            }

            Vehicle vehicle = GetVehicle(i_LicenseNumber);
            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with license number {i_LicenseNumber} not found.");
            }

            if (!(vehicle.m_Engine is FuelEngine))
            {
                throw new ArgumentException("Vehicle does not have a fuel-based engine.");
            }

            var fuelEngine = vehicle.m_Engine as FuelEngine;
            if (i_FuelType != fuelEngine.m_FuelType)
            {
                throw new ArgumentException($"Incorrect fuel type. Vehicle requires {fuelEngine.m_FuelType}.");
            }

            try
            {
                vehicle.FillEngine(i_AmountToFill);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error refueling vehicle: {ex.Message}");
            }
        }

        public static void Charge(string i_LicenseNumber, float i_NumOfMinutesToCharge)
        {
            if (string.IsNullOrWhiteSpace(i_LicenseNumber))
            {
                throw new ArgumentException("License number cannot be empty or whitespace.");
            }

            if (i_NumOfMinutesToCharge <= 0)
            {
                throw new ArgumentException("Charging time must be greater than zero minutes.");
            }

            Vehicle vehicle = GetVehicle(i_LicenseNumber);
            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with license number {i_LicenseNumber} not found.");
            }

            if (!(vehicle.m_Engine is ElectricEngine))
            {
                throw new ArgumentException("Vehicle does not have an electric engine.");
            }

            try
            {
                vehicle.FillEngine(i_NumOfMinutesToCharge / 60);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error charging vehicle: {ex.Message}");
            }
        }

        public static string ListAllVehicleDetails(string i_LicenseNumber)
        {
            if (string.IsNullOrWhiteSpace(i_LicenseNumber))
            {
                throw new ArgumentException("License number cannot be empty or whitespace.");
            }

            Vehicle vehicle = GetVehicle(i_LicenseNumber);
            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with license number {i_LicenseNumber} not found.");
            }

            try
            {
                return vehicle.ToString();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving vehicle details: {ex.Message}");
            }
        }

        public static Vehicle GetVehicle(string i_LicenseNumber)
        {
            if (string.IsNullOrWhiteSpace(i_LicenseNumber))
            {
                throw new ArgumentException("License number cannot be empty or whitespace.");
            }

            return m_Vehicles.FirstOrDefault(v => v.r_LicenseNumber == i_LicenseNumber);
        }

        // New helper method to validate if vehicle exists
        private static void ValidateVehicleExists(Vehicle vehicle, string licenseNumber)
        {
            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle with license number {licenseNumber} not found.");
            }
        }
    }
}