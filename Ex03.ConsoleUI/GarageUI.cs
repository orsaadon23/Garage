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
                int userChoice;

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
                                                eGarageFunctions.charge,
                                                eGarageFunctions.displayVehicleInfo,
                                                eGarageFunctions.quit));

                if (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice > 8 || userChoice < 1)
                {
                    throw new FormatException();
                }

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
        }

        private static void insertVehicle()
        {
            int vehicleChoice;

            Console.WriteLine("What's the license number?");
            string licenseNumber = Console.ReadLine();
            Vehicle vehicle = GarageManager.GetVehicle(licenseNumber);

            if(vehicle == null)
            {
                Console.WriteLine("Choose the vehicle type:");
                Console.WriteLine(String.Format("\n" +
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
                if (!int.TryParse(Console.ReadLine(), out vehicleChoice))
                {
                    throw new FormatException();
                }
                insertNewVehicle(licenseNumber, (eUserVehicleChoice)vehicleChoice);
            }
            else
            {
                GarageManager.SetStatus(licenseNumber, eVehicleStatus.InRepair);
                Console.WriteLine(string.Format("Status of vehicle {0} is changed to in repair.", licenseNumber));
            }
        }

        private static void insertNewVehicle(String i_LicenseNumber, eUserVehicleChoice vehicleChoice)
        {
            String ownerName;
            String phoneNumber;
            String modelName;

            Console.WriteLine("Please enter your name:");
            ownerName = Console.ReadLine();
            Console.WriteLine("Please enter your phone number:");
            phoneNumber = Console.ReadLine();
            Console.WriteLine("Please enter the model name:");
            modelName = Console.ReadLine();

            GarageFactory.CreateVehicle( vehicleChoice, modelName, i_LicenseNumber, ownerName, phoneNumber);

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
            int userChoice;

            Console.WriteLine("Enter your preferred filter:");
            Console.WriteLine(String.Format("\n" +
                    "1. {0}\n" +
                    "2. {1}\n" +
                    "3. {2}\n" +
                    "4. NoFilter\n",
                    eVehicleStatus.InRepair,
                    eVehicleStatus.Repaired,
                    eVehicleStatus.PaidFor));

            if (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 1 || userChoice > 4)        
            {
                throw new FormatException();
            }

            if (userChoice == 4) {
                Console.WriteLine(string.Join("\n", GarageManager.GetLicenseList(null)));
            }
            else
            {
                Console.WriteLine(string.Join("\n", GarageManager.GetLicenseList((eVehicleStatus)(userChoice))));
            }
        }

        private static void changeVehicleStatus()
        {
            Console.WriteLine("Enter the license number");
            string licenseNumber = Console.ReadLine();
            int userChoice;

            Console.WriteLine(String.Format("\n" +
                    "1. {0}\n" +
                    "2. {1}\n" +
                    "3. {2}\n",
                    eVehicleStatus.InRepair,
                    eVehicleStatus.Repaired,
                    eVehicleStatus.PaidFor));

            if (!int.TryParse(Console.ReadLine(), out userChoice))
            {
                throw new FormatException();
            }

            GarageManager.SetStatus(licenseNumber, (eVehicleStatus)userChoice);
        }

        private static void inflateTiresToMax()
        {
            Console.WriteLine("Enter license number");
            GarageManager.MaxFillTires(Console.ReadLine());
        }

        private static void refuel()
        {
            string licenseNumber;
            int fuelType;
            float litersToAdd;

            Console.WriteLine("Enter license number");
            licenseNumber = Console.ReadLine();
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
          
            fuelType = (int)tryParseInput(Console.ReadLine(), 1, 4);
            Console.WriteLine("How many liters would you like to fuel?");
            litersToAdd = tryParseInput(Console.ReadLine(), 0, int.MaxValue);

            GarageManager.Refuel(licenseNumber, (eFuelType)fuelType, litersToAdd);
        }

        private static void charge()
        {
            string licenseNumber;
            float minutesToAdd;
            Console.WriteLine("Enter license number");
            licenseNumber = Console.ReadLine();
            Console.WriteLine("How many minutes would you like to charge?");
            minutesToAdd = tryParseInput(Console.ReadLine(), 0, int.MaxValue);

            GarageManager.Charge(licenseNumber, minutesToAdd);
        }

        private static void displayVehicleInfo()
        {
            Console.WriteLine("Please enter vehicle license number.");
            string licenseNumber = Console.ReadLine();
            Console.WriteLine("\n" + GarageManager.ListAllVehicleDetails(licenseNumber));
        }
        private static void initCurrentEnergy(string licenseNumber)
        {
            Console.WriteLine("Enter your current energy level (fuel/battery)");
            float energyLevel = tryParseInput(Console.ReadLine(), 0, int.MaxValue);
            GarageFactory.InitializeVehicleEnergy(licenseNumber, energyLevel);
        }

        private static void initWheelDetails(int i_NumOfWheels, string i_LicenseNumber)
        {
            string[] manufacturerNames = new string[i_NumOfWheels];
            float[] currentAirPressures = new float[i_NumOfWheels];

            for(int i = 0; i < i_NumOfWheels; i++)
            {
                Console.WriteLine(string.Format("Enter the wheel manufacturer for wheel number {0}", (i + 1)));
                manufacturerNames[i] = Console.ReadLine();
                Console.WriteLine(string.Format("Enter the current air pressure for wheel number {0}", (i + 1)));
                currentAirPressures[i] = tryParseInput(Console.ReadLine(), 0, int.MaxValue);
            }

            GarageFactory.InitializeVehicleWheels(i_LicenseNumber, currentAirPressures, manufacturerNames);
        }

        private static void initCarDetails(string i_LicenseNumber)
        {
            int color;
            int numOfDoors;

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

            color = (int)tryParseInput(Console.ReadLine(), 1, 4);

            Console.WriteLine("Enter a number of doors between 2-5");
            numOfDoors = (int)tryParseInput(Console.ReadLine(), 2, 5);

            GarageFactory.InitializeCar(i_LicenseNumber, (eNumOfDoors)numOfDoors, (eColor)color);
        }

        private static void initMotorcycleDetails(string i_LicenseNumber)
        {
            int licenseType;
            int engineVolume;

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

            licenseType = (int)tryParseInput(Console.ReadLine(), 1, 4);

            Console.WriteLine("Enter the engine volume:");
            engineVolume = (int)tryParseInput(Console.ReadLine(), 0, int.MaxValue);

            GarageFactory.InitializeMotorcycle(i_LicenseNumber, (eLicenseType)licenseType, engineVolume);
        }

        private static void initTruckDetails(string i_LicenseNumber)
        {
            bool containsDangerousMaterials;
            float cargoTankVolume;

            Console.WriteLine("Does the truck contain dangerous materials?");
            Console.WriteLine(  "1. Yes\n" +
                                "2. No\n");

            int userChoice = (int)tryParseInput(Console.ReadLine(), 1, 2);
            containsDangerousMaterials = userChoice == 1 ? true : false;
            Console.WriteLine("Enter the cargo tank volume:");

            cargoTankVolume = tryParseInput(Console.ReadLine(), 0, int.MaxValue);
            GarageFactory.InitializeTruck(i_LicenseNumber, containsDangerousMaterials, cargoTankVolume);
        }

        private static float tryParseInput(string i_Input, int i_Min, int i_Max)
        {
            float result;

            if (!float.TryParse(i_Input, out result) || result < i_Min || result > i_Max)
            {
                throw new FormatException();
            }

            return result;
        }
    }
 
}
