using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using static Ex03.ConsoleUI.GarageUI;
using static Ex03.GarageLogic.GarageFactory;

namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {
        public enum eGarageFunctions
        {
            insertNewVehicle = 1,
            displayLicenseList,
            changeVehicleStatus,
            inflateTiresToMax,
            refuel,
            charge,
            displayVehicleInfo,
            quit
        }

        static void Main(string[] args)
        {
            bool isGarageRunning = true;

            while (isGarageRunning)
            {
                try
                {
                    DisplayMainMenu();
                    int userChoice = GetValidIntInput("Please select an option (1-8): ", 1, 8);

                    switch ((eGarageFunctions)userChoice)
                    {
                        case eGarageFunctions.insertNewVehicle:
                            insertVehicle();
                            break;
                        case eGarageFunctions.displayLicenseList:
                            displayLicenseList();
                            break;
                        case eGarageFunctions.changeVehicleStatus:
                            changeVehicleStatus();
                            break;
                        case eGarageFunctions.inflateTiresToMax:
                            inflateTiresToMax();
                            break;
                        case eGarageFunctions.refuel:
                            refuel();
                            break;
                        case eGarageFunctions.charge:
                            charge();
                            break;
                        case eGarageFunctions.displayVehicleInfo:
                            displayVehicleInfo();
                            break;
                        case eGarageFunctions.quit:
                            isGarageRunning = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Which service would you like?");
            Console.WriteLine(String.Format("\n" +
                                            "1. {0}\n" +
                                            "2. {1}\n" +
                                            "3. {2}\n" +
                                            "4. {3}\n" +
                                            "5. {4}\n" +
                                            "6. {5}\n" +
                                            "7. {6}\n" +
                                            "8. {7}\n",
                                            eGarageFunctions.insertNewVehicle,
                                            eGarageFunctions.displayLicenseList,
                                            eGarageFunctions.changeVehicleStatus,
                                            eGarageFunctions.inflateTiresToMax,
                                            eGarageFunctions.refuel,
                                            eGargeFunctions.charge,
                                            eGarageFunctions.displayVehicleInfo,
                                            eGarageFunctions.quit));
        }

        private static int GetValidIntInput(string prompt, int minValue, int maxValue)
        {
            int result;
            bool isValid = false;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out result) && result >= minValue && result <= maxValue)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine($"\nPlease enter a valid number between {minValue} and {maxValue}.");
                }
            } while (!isValid);

            return result;
        }

        private static float GetValidFloatInput(string prompt, float minValue, float maxValue)
        {
            float result;
            bool isValid = false;

            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (float.TryParse(input, out result) && result >= minValue && result <= maxValue)
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine($"\nPlease enter a valid number between {minValue} and {maxValue}.");
                }
            } while (!isValid);

            return result;
        }

        private static string GetNonEmptyString(string prompt)
        {
            string input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(input));

            return input;
        }

        private static void insertVehicle()
        {
            string licenseNumber = GetNonEmptyString("What's the license number? ");
            Vehicle vehicle = GarageManager.GetVehicle(licenseNumber);

            if(vehicle == null)
            {
                Console.WriteLine("\nChoose the vehicle type:");
                Console.WriteLine(String.Format(
                                    "1. {0}\n" +
                                    "2. {1}\n" +
                                    "3. {2}\n" +
                                    "4. {3}\n" +
                                    "5. {4}\n",
                                    eUserVehicleChoice.ElectricCar,
                                    eUserVehicleChoice.FuelBasedCar,
                                    eUserVehicleChoice.ElectricMotorcycle,
                                    eUserVehicleChoice.FuelBasedMotorcycle,
                                    eUserVehicleChoice.Truck));

                int vehicleChoice = GetValidIntInput("Select vehicle type (1-5): ", 1, 5);
                insertNewVehicle(licenseNumber, (eUserVehicleChoice)vehicleChoice);
            }
            else
            {
                GarageManager.SetStatus(licenseNumber, eVehicleStatus.InRepair);
                Console.WriteLine($"Status of vehicle {licenseNumber} is changed to in repair.");
            }
        }

        private static void insertNewVehicle(String i_LicenseNumber, eUserVehicleChoice vehicleChoice)
        {
            string ownerName = GetNonEmptyString("Please enter your name: ");
            string phoneNumber = GetNonEmptyString("Please enter your phone number: ");
            string modelName = GetNonEmptyString("Please enter the model name: ");

            GarageFactory.CreateVehicle(vehicleChoice, modelName, i_LicenseNumber, ownerName, phoneNumber);

            switch (vehicleChoice)
            {
                case eUserVehicleChoice.ElectricCar:
                    initWheelDetails(4, i_LicenseNumber);
                    initCarDetails(i_LicenseNumber);
                    break;
                case eUserVehicleChoice.FuelBasedCar:
                    initWheelDetails(4, i_LicenseNumber);
                    initCarDetails(i_LicenseNumber);
                    break;
                case eUserVehicleChoice.ElectricMotorcycle:
                    initWheelDetails(2, i_LicenseNumber);
                    initMotorcycleDetails(i_LicenseNumber);
                    break;
                case eUserVehicleChoice.FuelBasedMotorcycle:
                    initWheelDetails(2, i_LicenseNumber);
                    initMotorcycleDetails(i_LicenseNumber);
                    break;
                case eUserVehicleChoice.Truck:
                    initWheelDetails(12, i_LicenseNumber);
                    initTruckDetails(i_LicenseNumber);
                    break;
            }
            initCurrentEnergy(i_LicenseNumber);
        }

        private static void displayLicenseList()
        {
            Console.WriteLine("Enter your preferred filter:");
            Console.WriteLine(String.Format("\n" +
                    "1. {0}\n" +
                    "2. {1}\n" +
                    "3. {2}\n" +
                    "4. NoFilter\n",
                    eVehicleStatus.InRepair,
                    eVehicleStatus.Repaired,
                    eVehicleStatus.PaidFor));

            int userChoice = GetValidIntInput("Select filter (1-4): ", 1, 4);

            if (userChoice == 4)
            {
                Console.WriteLine(string.Join("\n", GarageManager.GetLicenseList(null)));
            }
            else
            {
                Console.WriteLine(string.Join("\n", GarageManager.GetLicenseList((eVehicleStatus)(userChoice))));
            }
        }

        private static void changeVehicleStatus()
        {
            string licenseNumber = GetNonEmptyString("Enter the license number: ");

            Console.WriteLine(String.Format("\n" +
                    "1. {0}\n" +
                    "2. {1}\n" +
                    "3. {2}\n",
                    eVehicleStatus.InRepair,
                    eVehicleStatus.Repaired,
                    eVehicleStatus.PaidFor));

            int userChoice = GetValidIntInput("Select new status (1-3): ", 1, 3);
            GarageManager.SetStatus(licenseNumber, (eVehicleStatus)userChoice);
        }

        private static void inflateTiresToMax()
        {
            string licenseNumber = GetNonEmptyString("Enter license number: ");
            GarageManager.MaxFillTires(licenseNumber);
        }

        private static void refuel()
        {
            string licenseNumber = GetNonEmptyString("Enter license number: ");

            Console.WriteLine("Choose your fuel type:");
            Console.WriteLine(String.Format("\n" +
                    "1. {0}\n" +
                    "2. {1}\n" +
                    "3. {2}\n" +
                    "4. {3}\n",
                    eFuelType.Soler,
                    eFuelType.Octane95,
                    eFuelType.Octane96,
                    eFuelType.Octane98));

            int fuelType = GetValidIntInput("Select fuel type (1-4): ", 1, 4);
            float litersToAdd = GetValidFloatInput("How many liters would you like to fuel? ", 0, float.MaxValue);

            GarageManager.Refuel(licenseNumber, (eFuelType)fuelType, litersToAdd);
        }

        private static void charge()
        {
            string licenseNumber = GetNonEmptyString("Enter license number: ");
            float minutesToAdd = GetValidFloatInput("How many minutes would you like to charge? ", 0, float.MaxValue);

            GarageManager.Charge(licenseNumber, minutesToAdd);
        }

        private static void displayVehicleInfo()
        {
            string licenseNumber = GetNonEmptyString("Please enter vehicle license number: ");
            Console.WriteLine("\n" + GarageManager.ListAllVehicleDetails(licenseNumber));
        }

        private static void initCurrentEnergy(string licenseNumber)
        {
            float energyLevel = GetValidFloatInput("Enter your current energy level (fuel/battery): ", 0, float.MaxValue);
            GarageFactory.InitializeVehicleEnergy(licenseNumber, energyLevel);
        }

        private static void initWheelDetails(int i_NumOfWheels, string i_LicenseNumber)
        {
            string[] manufacturerNames = new string[i_NumOfWheels];
            float[] currentAirPressures = new float[i_NumOfWheels];

            for(int i = 0; i < i_NumOfWheels; i++)
            {
                manufacturerNames[i] = GetNonEmptyString($"Enter the wheel manufacturer for wheel number {i + 1}: ");
                currentAirPressures[i] = GetValidFloatInput($"Enter the current air pressure for wheel number {i + 1}: ", 0, float.MaxValue);
            }

            GarageFactory.InitializeVehicleWheels(i_LicenseNumber, currentAirPressures, manufacturerNames);
        }

        private static void initCarDetails(string i_LicenseNumber)
        {
            Console.WriteLine("Enter vehicle color:");
            Console.WriteLine(String.Format(
                                "1. {0}\n" +
                                "2. {1}\n" +
                                "3. {2}\n" +
                                "4. {3}\n",
                                eColor.Yellow,
                                eColor.White,
                                eColor.Red,
                                eColor.Gray));

            int color = GetValidIntInput("Select color (1-4): ", 1, 4);
            int numOfDoors = GetValidIntInput("Enter a number of doors (2-5): ", 2, 5);

            GarageFactory.InitializeCar(i_LicenseNumber, (eNumOfDoors)numOfDoors, (eColor)color);
        }

        private static void initMotorcycleDetails(string i_LicenseNumber)
        {
            Console.WriteLine("Enter license type:");
            Console.WriteLine(String.Format(
                                "1. {0}\n" +
                                "2. {1}\n" +
                                "3. {2}\n" +
                                "4. {3}\n",
                                eLicenseType.A,
                                eLicenseType.A1,
                                eLicenseType.AA,
                                eLicenseType.B1));

            int licenseType = GetValidIntInput("Select license type (1-4): ", 1, 4);
            int engineVolume = GetValidIntInput("Enter the engine volume: ", 0, int.MaxValue);

            GarageFactory.InitializeMotorcycle(i_LicenseNumber, (eLicenseType)licenseType, engineVolume);
        }

        private static void initTruckDetails(string i_LicenseNumber)
        {
            Console.WriteLine("Does the truck contain dangerous materials?");
            Console.WriteLine("1. Yes\n2. No\n");

            int userChoice = GetValidIntInput("Select option (1-2): ", 1, 2);
            bool containsDangerousMaterials = userChoice == 1;

            float cargoTankVolume = GetValidFloatInput("Enter the cargo tank volume: ", 0, float.MaxValue);
            GarageFactory.InitializeTruck(i_LicenseNumber, containsDangerousMaterials, cargoTankVolume);
        }
    }
}